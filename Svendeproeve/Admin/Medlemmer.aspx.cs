using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Medlemmer : System.Web.UI.Page
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

                    cmd.CommandText = "SELECT *, (SELECT SUM(hold_brugere_point) FROM hold_brugere WHERE fk_bruger_id = bruger_id AND hold_brugere_godkendt = 1) AS total_point FROM brugere INNER JOIN medlemmer ON fk_bruger_id = bruger_id WHERE fk_rolle_id = 3";

                    conn.Open();
                    Repeater_Brugere.DataSource = cmd.ExecuteReader();
                    Repeater_Brugere.DataBind();
                    conn.Close();

                    int id = 0;
                    if (Request.QueryString["id"] != null)
                    {
                        if (int.TryParse(Request.QueryString["id"], out id))
                        {

                            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                            switch (Request.QueryString["action"])
                            {
                                case "ret":
                                    RequiredFieldValidator_Kodeord.Enabled = false;

                                    cmd.CommandText = "SELECT bruger_navn, bruger_email, bruger_id, medlem_adresse FROM brugere INNER JOIN medlemmer ON fk_bruger_id = bruger_id WHERE bruger_id = @id";

                                    conn.Open();
                                    SqlDataReader reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        TextBox_Email.Text = reader["bruger_email"].ToString();
                                        TextBox_Navn.Text = reader["bruger_navn"].ToString();
                                        TextBox_Adresse.Text = reader["medlem_adresse"].ToString();
                                    }
                                    conn.Close();
                                    break;
                                case "slet":

                                    Bruger sletBruger = new Bruger();

                                    bool er_bruger_slettet = sletBruger.SletObjektReturnBool(Convert.ToInt32(Request.QueryString["id"]));

                                    if (er_bruger_slettet)
                                    {
                                        Session["besked"] = "Bruger blev slettet.";
                                        Response.Redirect("Medlemmer.aspx");
                                    }
                                    else
                                    {
                                        Session["besked"] = "Bruger blev ikke slettet. Prøv igen eller kontakt webmaster.";
                                        Response.Redirect("Medlemmer.aspx");
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Response.Redirect("Medlemmer.aspx");
                        }
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
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@adresse", TextBox_Adresse.Text);

        if (Request.QueryString["action"] != null & Request.QueryString["id"] != null)
        {

            Bruger retBruger = new Bruger();
            bool er_bruger_rettet = retBruger.UpdateBrugerReturnBool(TextBox_Email.Text, TextBox_Kodeord.Text, TextBox_Navn.Text, Convert.ToInt32(Request.QueryString["id"]), "3");

            if (er_bruger_rettet)
            {
                cmd.Parameters.AddWithValue("@bruger_id", Request.QueryString["id"]);
                cmd.CommandText = "UPDATE medlemmer SET medlem_adresse = @adresse WHERE fk_bruger_id = @bruger_id";

                int opdaterede_rækker = 0;
                conn.Open();
                opdaterede_rækker = cmd.ExecuteNonQuery();
                conn.Close();

                if (opdaterede_rækker > 0)
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
                Session["besked"] = "Rettelse fejlede. Prøv igen eller kontakt webmaster.";
                Response.Redirect("Brugere.aspx");
            }
        }
        else
        {
            Bruger nyBruger = new Bruger();

            int bruger_id = nyBruger.OpretBrugerReturnID(TextBox_Email.Text, TextBox_Kodeord.Text, TextBox_Navn.Text, "3");

            if (bruger_id > 0)
            {
                int er_medlem_oprettet = 0;
                cmd.CommandText = "INSERT INTO medlemmer (medlem_adresse, fk_bruger_id) VALUES (@adresse, @bruger_id)";
                cmd.Parameters.AddWithValue("@bruger_id", bruger_id);

                conn.Open();
                er_medlem_oprettet = cmd.ExecuteNonQuery();
                conn.Close();

                if (er_medlem_oprettet > 0)
                {
                    Session["besked"] = "Tillykke, du er nu oprettet!";
                }
                else
                {
                    Session["besked"] = "Oprettelse fejlede. Prøv med en anden email eller kontakt os.";
                    cmd.CommandText = "DELETE FROM brugere WHERE bruger_id = @bruger_id";
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                Response.Redirect("Medlemmer.aspx");

            }
            else
            {
                Session["besked"] = "Oprettelse fejlede. Prøv med en anden email eller kontakt os.";
                Response.Redirect("Medlemmer.aspx");
            }
        }
    }
}