using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class AdminAnmeldelser : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //Henter alle relevante data tilhørende anmeldelser ud
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT film_navn, bruger_navn, anmeldelse_tekst, anmeldelse_id FROM anmeldelser INNER JOIN film ON fk_film_id = film_id INNER JOIN brugere ON fk_bruger_id = bruger_id";
        conn.Open();
        Repeater_Anmeldelser.DataSource = cmd.ExecuteReader();
        Repeater_Anmeldelser.DataBind();
        conn.Close();

        //Sørger for at kun en admin kan slette
        if (Session["bruger"] != null)
        {
            Bruger bruger = (Bruger)Session["bruger"];
            if (bruger.Rolle_Navn == rolle.Administrator)
            {
                if (Request.QueryString["slet"] != null)
                {
                    db.DeleteFromTable("anmeldelser", "anmeldelse_id", Convert.ToInt32(Request.QueryString["id"]));
                    Response.Redirect("AdminAnmeldelser.aspx");
                }
            }
        }
    }

}