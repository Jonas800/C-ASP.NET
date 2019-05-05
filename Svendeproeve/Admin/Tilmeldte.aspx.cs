using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Tilmeldte : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;
            if (bruger.Rolle == 1)
            {
                int hold_id = 0;
                if (int.TryParse(Request.QueryString["hold_id"], out hold_id))
                {
                    if (!IsPostBack)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@hold_id", hold_id);


                        cmd.CommandText = "SELECT * FROM hold INNER JOIN aktiviteter ON fk_aktivitet_id = aktivitet_id INNER JOIN brugere ON fk_bruger_id = bruger_id WHERE hold_id = @hold_id ORDER BY hold_tidspunkt ASC, aktivitet_navn ASC, bruger_navn ASC";

                        conn.Open();
                        Repeater_Hold.DataSource = cmd.ExecuteReader();
                        Repeater_Hold.DataBind();
                        conn.Close();


                        cmd.CommandText = "SELECT * FROM hold_brugere INNER JOIN brugere ON hold_brugere.fk_bruger_id = bruger_id INNER JOIN hold ON fk_hold_id = hold_id INNER JOIN medlemmer ON medlemmer.fk_bruger_id = bruger_id WHERE hold_id = @hold_id";

                        conn.Open();
                        Repeater_Tilmeldte.DataSource = cmd.ExecuteReader();
                        Repeater_Tilmeldte.DataBind();
                        conn.Close();

                        if (Request.QueryString["action"] == "slet")
                        {
                            cmd.Parameters.AddWithValue("@bruger_id", Request.QueryString["id"]);
                            cmd.CommandText = "DELETE FROM hold_brugere WHERE fk_bruger_id = @bruger_id AND fk_hold_id = @hold_id";

                            conn.Open();
                            int slettede_rækker = cmd.ExecuteNonQuery();
                            conn.Close();

                            if (slettede_rækker > 0)
                            {
                                Session["besked"] = "Medlem blev fjernet fra hold.";
                            }
                            else
                            {
                                Session["besked"] = "Medlem blev ikke fjernet fra hold.";
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("Hold.aspx");
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
    protected void Repeater_Tilmeldte_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "changePoint")
        {
            int id = 0;
            int point = 0;

            int.TryParse((e.Item.FindControl("HiddenField_HoldBrugereID") as HiddenField).Value, out id);
            int.TryParse((e.Item.FindControl("TextBox_Point") as TextBox).Text, out point);
            bool tilstede = (e.Item.FindControl("CheckBox_Tilstede") as CheckBox).Checked;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "UPDATE hold_brugere SET hold_brugere_point = @point, hold_brugere_godkendt = @godkendt WHERE hold_brugere_id = @id";

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@point", point);
            cmd.Parameters.AddWithValue("@godkendt", tilstede);

            conn.Open();
            int rettede_rækker = cmd.ExecuteNonQuery();
            conn.Close();
            if (rettede_rækker > 0)
            {
                Session["besked"] = "Rettelse lykkedes.";
            }
            else
            {
                Session["besked"] = "Rettelse fejlede. Prøv igen eller kontakt webmaster.";
            }
        }
    }
}