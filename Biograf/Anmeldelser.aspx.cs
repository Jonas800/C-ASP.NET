using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Anmeldelser : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        //Først sørger vi for at brugeren er logget ind og en film er valgt
        if (Request.QueryString["id"] != null)
        {
            if (Session["bruger"] != null)
            {
                //Henter bruger ud af sessionen
                Bruger bruger = (Bruger)Session["bruger"];

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                //Laver et tjek på om brugeren har lavet en anmeldelse for denne film før, hvis brugeren har, forudfyldes tekstboxen
                cmd.CommandText = "SELECT * FROM anmeldelser WHERE fk_bruger_id = @bruger_id AND fk_film_id = @film_id";
                cmd.Parameters.AddWithValue("@film_id", Request.QueryString["id"]);
                cmd.Parameters.AddWithValue("@bruger_id", bruger.ID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (!IsPostBack)
                    {
                        TextBox_Tekst.Text = reader["anmeldelse_tekst"].ToString();
                    }
                    Panel_Slet.Visible = true;
                }
                conn.Close();

                //Sletter anmeldelsen
                if (Request.QueryString["slet"] != null)
                {
                    cmd.CommandText = "DELETE FROM anmeldelser WHERE fk_bruger_id = @bruger_id AND fk_film_id = @film_id";
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Helpers.Return();
                }
            }
            else
            {
                Label_Ikke_Bruger.Visible = true;
                TextBox_Tekst.Visible = false;
                Button_Gem.Visible = false;
            }
        }
        else
        {
            Response.Redirect("Filmliste.aspx");
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        //Henter bruger ud af sessionen
        Bruger bruger = (Bruger)Session["bruger"];

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        //Laver et tjek på om brugeren har lavet en anmeldelse til denne film før, hvis brugeren har, så updateres der når der trykkes gem, hvis ikke så oprettes en ny række i tabellen
        cmd.CommandText = "SELECT * FROM anmeldelser WHERE fk_bruger_id = @bruger_id AND fk_film_id = @film_id";
        cmd.Parameters.AddWithValue("@film_id", Request.QueryString["id"]);
        cmd.Parameters.AddWithValue("@bruger_id", bruger.ID);
        cmd.Parameters.AddWithValue("@tekst", TextBox_Tekst.Text);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            conn.Close();
            cmd.CommandText = "UPDATE anmeldelser SET anmeldelse_tekst = @tekst WHERE fk_bruger_id = @bruger_id AND fk_film_id = @film_id";
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        else
        {
            conn.Close();
            cmd.CommandText = "INSERT INTO anmeldelser (anmeldelse_tekst, fk_bruger_id, fk_film_id) VALUES (@tekst, @bruger_id, @film_id)";
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        Response.Redirect("Anmeldelser.aspx?id=" + Request.QueryString["id"]);
    }
}