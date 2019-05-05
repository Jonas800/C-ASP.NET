using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Brugere : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;
            if (bruger.Rolle == 1)
            {
                if (!IsPostBack)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    SelectBrugere(cmd);

                    cmd.CommandText = "SELECT * FROM roller WHERE rolle_id = 1 OR rolle_id = 2";
                    conn.Open();
                    DropDownList_Rolle.DataSource = cmd.ExecuteReader();
                    DropDownList_Rolle.DataBind();
                    conn.Close();
                    DropDownList_Rolle.Items.Insert(0, "Vælg rolle");

                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                    switch (Request.QueryString["action"])
                    {
                        case "ret":
                            RequiredFieldValidator_Kodeord.Enabled = false;

                            cmd.CommandText = "SELECT bruger_navn, bruger_email, bruger_id FROM brugere WHERE bruger_id = @id";

                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                TextBox_Email.Text = reader["bruger_email"].ToString();
                                TextBox_Navn.Text = reader["bruger_navn"].ToString();
                            }
                            conn.Close();
                            break;
                        case "slet":

                            if (Request.QueryString["rolle"] == "1")
                            {

                                cmd.CommandText = "SELECT COUNT (fk_rolle_id) FROM BRUGERE WHERE fk_rolle_id = 1";
                                conn.Open();
                                int antal_admin = Convert.ToInt32(cmd.ExecuteScalar());
                                conn.Close();

                                if (antal_admin > 1)
                                {

                                    Bruger sletBruger = new Bruger();

                                    bool er_bruger_slettet = sletBruger.SletObjektReturnBool(Convert.ToInt32(Request.QueryString["id"]));

                                    if (er_bruger_slettet)
                                    {
                                        Session["besked"] = "Bruger blev slettet.";
                                        Response.Redirect("Brugere.aspx");
                                    }
                                    else
                                    {
                                        Session["besked"] = "Bruger blev ikke slettet. Prøv igen eller kontakt webmaster.";
                                        Response.Redirect("Brugere.aspx");
                                    }
                                }
                                else
                                {
                                    Session["besked"] = "Der skal være mindst én administrator.";
                                    Response.Redirect("Brugere.aspx");
                                }
                            }
                            else
                            {
                                Bruger sletBruger = new Bruger();

                                bool er_bruger_slettet = sletBruger.SletObjektReturnBool(Convert.ToInt32(Request.QueryString["id"]));

                                if (er_bruger_slettet)
                                {
                                    Session["besked"] = "Bruger blev slettet.";
                                    Response.Redirect("Brugere.aspx");
                                }
                                else
                                {
                                    Session["besked"] = "Bruger blev ikke slettet. Prøv igen eller kontakt webmaster.";
                                    Response.Redirect("Brugere.aspx");
                                }
                            }
                            break;
                    }
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

    private void SelectBrugere(SqlCommand cmd)
    {
        cmd.CommandText = "SELECT bruger_navn, bruger_email, bruger_id, rolle_navn, fk_rolle_id FROM brugere INNER JOIN roller ON fk_rolle_id = rolle_id WHERE fk_rolle_id = 1 OR fk_rolle_id = 2 ORDER BY fk_rolle_id, bruger_email";
        conn.Open();
        Repeater_Brugere.DataSource = cmd.ExecuteReader();
        Repeater_Brugere.DataBind();
        conn.Close();
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@navn", TextBox_Navn.Text);
        cmd.Parameters.AddWithValue("@email", TextBox_Email.Text);

        if (Request.QueryString["action"] != null & Request.QueryString["id"] != null)
        {
            Bruger retBruger = new Bruger();
            bool er_bruger_rettet = retBruger.UpdateBrugerReturnBool(TextBox_Email.Text, TextBox_Kodeord.Text, TextBox_Navn.Text, Convert.ToInt32(Request.QueryString["id"]), DropDownList_Rolle.SelectedValue);

            if (er_bruger_rettet)
            {
                Session["besked"] = "Rettelse lykkedes.";
                Response.Redirect("Brugere.aspx");
            }
            else
            {
                Session["besked"] = "Rettelse fejlede. Prøv igen eller kontakt webmaster.";
                Response.Redirect("Brugere.aspx");
            }
        }
        else
        {
            Bruger nyBruger = new Bruger();

            bool er_bruger_oprettet = nyBruger.OpretBrugerReturnBool(TextBox_Email.Text, TextBox_Kodeord.Text, TextBox_Navn.Text, DropDownList_Rolle.SelectedValue);

            if (er_bruger_oprettet)
            {
                Session["besked"] = "Oprettelse lykkedes.";
                Response.Redirect("Brugere.aspx");
            }
            else
            {
                Session["besked"] = "Oprettelse fejlede. Prøv igen eller kontakt webmaster.";
                Response.Redirect("Brugere.aspx");
            }
        }
    }
}