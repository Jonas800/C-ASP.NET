using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Tilbude : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            //Opretter Bruger objekt via Session
            Bruger bruger = (Bruger)Session["bruger"];

            //Henter alle tilbud som passer til den rolle brugeren er
            SqlCommand cmd = new SqlCommand("SELECT * FROM tilbud WHERE tilbud_rolle = @rolle", conn);
            cmd.Parameters.AddWithValue("@rolle", bruger.Rolle_Navn.ToString());

            Label_Tilbud.Text = "Du har rettigheder til disse tilbud!";

            conn.Open();
            Repeater_Tilbud.DataSource = cmd.ExecuteReader();
            Repeater_Tilbud.DataBind();
            conn.Close();
        }
        else
        {
            //Viser et par tilbud tilfældigt - ment til at give en teaser på hvad man kan få hvis man opretter sig som bruger :) Andre alternativer er ingen eller alle tilbud vises.
            SqlCommand cmd = new SqlCommand("SELECT TOP 2 * FROM tilbud ORDER BY newid()", conn);

            conn.Open();
            Repeater_Tilbud.DataSource = cmd.ExecuteReader();
            Repeater_Tilbud.DataBind();
            conn.Close();
        }
    }
}