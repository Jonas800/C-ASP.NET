using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Hold : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;
            if (bruger.Rolle == 1)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT *, (SELECT COUNT (fk_hold_id) FROM hold_brugere WHERE fk_hold_id = hold_id) as total FROM hold INNER JOIN aktiviteter ON fk_aktivitet_id = aktivitet_id INNER JOIN brugere ON fk_bruger_id = bruger_id ORDER BY hold_tidspunkt ASC, aktivitet_navn ASC, bruger_navn ASC";

                conn.Open();
                Repeater_Hold.DataSource = cmd.ExecuteReader();
                Repeater_Hold.DataBind();
                conn.Close();

                if (!IsPostBack)
                {
                    Calendar_Dato.SelectedDate = DateTime.Now;

                    cmd.CommandText = "SELECT * FROM aktiviteter";
                    conn.Open();
                    DropDownList_Aktivitet.DataSource = cmd.ExecuteReader();
                    DropDownList_Aktivitet.DataBind();
                    conn.Close();
                    DropDownList_Aktivitet.Items.Insert(0, "Vælg aktivitet");

                    cmd.CommandText = "SELECT * FROM brugere WHERE fk_rolle_id = 2";
                    conn.Open();
                    DropDownList_Instruktør.DataSource = cmd.ExecuteReader();
                    DropDownList_Instruktør.DataBind();
                    conn.Close();
                    DropDownList_Instruktør.Items.Insert(0, "Vælg instruktør");

                }


                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                switch (Request.QueryString["action"])
                {
                    case "ret":
                        if (!IsPostBack)
                        {
                            cmd.CommandText = "SELECT * FROM hold WHERE hold_id = @id";
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();

                            if (reader.Read())
                            {
                                DropDownList_Aktivitet.SelectedValue = reader["fk_aktivitet_id"].ToString();
                                DropDownList_Instruktør.SelectedValue = reader["fk_bruger_id"].ToString();
                                Calendar_Dato.SelectedDate = Convert.ToDateTime(reader["hold_tidspunkt"]);
                                TextBox_Point.Text = reader["hold_point"].ToString();
                                TextBox_Tidspunkt.Text = Convert.ToDateTime(reader["hold_tidspunkt"]).ToString("HH:mm");
                                TextBox_Antal.Text = reader["hold_max_antal"].ToString();
                            }
                            else
                            {
                                Response.Redirect("Hold.aspx");
                            }
                            conn.Close();
                        }
                        break;
                    case "slet":
                        int slettede_rækker = 0;
                        cmd.CommandText = "DELETE FROM hold WHERE hold_id = @id";
                        conn.Open();
                        slettede_rækker = cmd.ExecuteNonQuery();
                        conn.Close();
                        if (slettede_rækker > 0)
                        {
                            Session["besked"] = "Sletning lykkedes.";
                        }
                        else
                        {
                            Session["besked"] = "Sletning fejlede. Prøv igen eller kontakt webmaster.";
                        }

                        Response.Redirect("Hold.aspx");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        if (Calendar_Dato.SelectedDate == null || Calendar_Dato.SelectedDate == new DateTime(0001, 1, 1, 0, 0, 0) || Calendar_Dato.SelectedDate > DateTime.UtcNow.AddDays(-1))
        {
            DateTime tid = DateTime.Now;
            if (DateTime.TryParse(Calendar_Dato.SelectedDate.ToString("dd-MM-yyyy") + "-" + TextBox_Tidspunkt.Text, out tid))
            {
                tid = tid.ToUniversalTime();

                //Response.Write("tilbage" + tid.Subtract(DateTime.UtcNow).TotalHours);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("@tidspunkt", tid);
                cmd.Parameters.AddWithValue("@point", TextBox_Point.Text);
                cmd.Parameters.AddWithValue("@bruger_id", DropDownList_Instruktør.SelectedValue);
                cmd.Parameters.AddWithValue("@aktivitet_id", DropDownList_Aktivitet.SelectedValue);
                cmd.Parameters.AddWithValue("@antal", TextBox_Antal.Text);

                if (Request.QueryString["action"] == "ret" && Request.QueryString["id"] != null)
                {
                    //ret
                    int indsatte_rækker = 0;
                    cmd.Parameters.AddWithValue("@hold_id", Request.QueryString["id"]);
                    cmd.CommandText = "UPDATE HOLD SET hold_tidspunkt = @tidspunkt, hold_point = @point, hold_max_antal = @antal, fk_bruger_id = @bruger_id, fk_aktivitet_id = @aktivitet_id WHERE hold_id = @hold_id";
                    conn.Open();
                    indsatte_rækker = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (indsatte_rækker > 0)
                    {
                        Session["besked"] = "Rettelse lykkedes.";
                    }
                    else
                    {
                        Session["besked"] = "Rettelse fejlede. Prøv igen eller kontakt webmaster";
                    }
                    Response.Redirect("Hold.aspx");
                }
                else
                {
                    //opret
                    int indsatte_rækker = 0;
                    cmd.CommandText = "INSERT INTO hold (hold_tidspunkt, hold_point, fk_bruger_id, fk_aktivitet_id, hold_max_antal) VALUES (@tidspunkt, @point, @bruger_id, @aktivitet_id, @antal)";
                    conn.Open();
                    indsatte_rækker = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (indsatte_rækker > 0)
                    {
                        Session["besked"] = "Oprettelse lykkedes.";
                    }
                    else
                    {
                        Session["besked"] = "Oprettelse fejlede. Prøv igen eller kontakt webmaster";
                    }
                    Response.Redirect("Hold.aspx");
                }
            }
            else
            {
                Label_Calendar_Error.Text = "Ugyldig dato.";
            }
        }
        else
        {
            Label_Calendar_Error.Text = "Vælg en kommende dato.";
        }
    }
}