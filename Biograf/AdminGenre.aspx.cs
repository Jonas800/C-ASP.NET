using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class AdminGenre : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        //Genrer objekt oprettes
        Genre genre = new Genre();

        switch (Request.QueryString["handling"])
        {
            case "ret":
                //Henter den valgte genres data og forudfylder textboxen
                genre.HentGenre(Convert.ToInt32(Request.QueryString["id"]));

                if (!IsPostBack)
                {
                    TextBox_Genre.Text = genre.Navn;
                } 
                break;
            case "slet":
                //Sletter genren ud fra en sendt id
                genre.SletGenre(Convert.ToInt32(Request.QueryString["id"]));
                Response.Redirect("AdminGenre.aspx");
                break;
            default:
                //Finder alle genrer og ligger dem i en repeater
                SqlCommand cmd = new SqlCommand("SELECT * FROM genrer", conn);
                conn.Open();
                Repeater_Genre.DataSource = cmd.ExecuteReader();
                Repeater_Genre.DataBind();
                conn.Close();
                break;
        }

    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        //Et genre objekt oprettes
        Genre genre = new Genre();

        switch (Request.QueryString["handling"])
        {
            case "ret":
                //Opdaterer genrens navn i databasen ud fra en sendt id
                genre.UpdateGenre(Convert.ToInt32(Request.QueryString["id"]), TextBox_Genre.Text);

                break;
            default:
                //Sætter genres navn til den indtastede string
                genre.Navn = TextBox_Genre.Text;

                //Gemmer genre i databasen
                genre.GemGenre();

                Response.Redirect("AdminGenre.aspx");

                break;
        }
    }
}