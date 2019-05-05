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
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            SelectBrugere(cmd);
            if (Session["besked"] != null)
            {
                Label_Besked.Text = Session["besked"].ToString();
                Session.Remove("besked");
            }

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

                    Bruger sletBruger = new Bruger();

                    bool er_bruger_slettet = sletBruger.SletObjektReturnBool(Convert.ToInt32(Request.QueryString["id"]));

                    SelectBrugere(cmd);

                    if (er_bruger_slettet)
                    {
                        Session["besked"] = "Bruger blev slettet.";
                        Response.Redirect("Brugere.aspx");
                    }
                    else
                    {
                        Label_Besked.Text = "Bruger blev ikke slettet. Prøv igen eller kontakt webmaster.";
                    }
                    break;
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }

    }

    private void SelectBrugere(SqlCommand cmd)
    {
        cmd.CommandText = "SELECT bruger_navn, bruger_email, bruger_id FROM brugere";
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
            //if (TextBox_Kodeord.Text == String.Empty)
            //{
            //    cmd.CommandText = "UPDATE brugere SET bruger_email = @email, bruger_navn = @navn WHERE bruger_id = @id";
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@kodeord", TextBox_Kodeord.Text);
            //    cmd.CommandText = "UPDATE brugere SET bruger_email = @email, bruger_navn = @navn, bruger_kodeord = @kodeord WHERE bruger_id = @id";
            //}
            //cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            //conn.

            Bruger retBruger = new Bruger();
            bool er_bruger_rettet = retBruger.UpdateBrugerReturnBool(TextBox_Email.Text, TextBox_Kodeord.Text, TextBox_Navn.Text, Convert.ToInt32(Request.QueryString["id"]));

            if (er_bruger_rettet)
            {
                Session["besked"] = "Rettelse lykkedes.";
                Response.Redirect("Brugere.aspx");
            }
            else
            {
                Label_Besked.Text = "Rettelse fejlede. Prøv igen eller kontakt webmaster.";
            }
        }
        else
        {
            Bruger nyBruger = new Bruger();

            bool er_bruger_oprettet = nyBruger.OpretBrugerReturnBool(TextBox_Email.Text, TextBox_Kodeord.Text, TextBox_Navn.Text);

            if (er_bruger_oprettet)
            {
                Label_Besked.Text = "Oprettelse lykkedes.";
                SelectBrugere(cmd);
            }
            else
            {
                Label_Besked.Text = "Oprettelse fejlede. Prøv igen eller kontakt webmaster.";
            }
        }
    }
}