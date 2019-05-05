using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Filmbeskrivelse : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        ////Start rating metoden
        //if (Request.QueryString["rating"] != null && Request.QueryString["id"] != null)
        //{
        //    Rating(Request.QueryString["id"], Request.QueryString["rating"]);
        //}

        //Tjekker om en film er valgt
        if (Request.QueryString["id"] != null)
        {
            //Der oprettes en datatable som finder navn, beskrivelse og id på filmen, samt udregner et gennemsnit af de ratings filmen har fået via rating-tabellen.
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "SELECT film_id, film_navn, film_beskrivelse, ISNULL((SELECT CAST(SUM(rating_tal) as decimal(10,2)) / COUNT(rating_tal) FROM rating WHERE fk_film_id = film_id), 0) as vurderinger FROM film WHERE film_id = @film_id ORDER BY film_navn ASC";
            cmd.Parameters.AddWithValue("@film_id", Request.QueryString["id"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            Repeater_Film.DataSource = dt;
            Repeater_Film.DataBind();

            //Finder alle anmeldelser frem til denne film, med brugernavn, ratings og anmeldelses tekst via inner joins mellem de tre tabeller
            cmd.CommandText = "SELECT bruger_navn, rating_tal, anmeldelse_tekst FROM anmeldelser INNER JOIN brugere ON anmeldelser.fk_bruger_id = bruger_id INNER JOIN rating ON rating.fk_bruger_id = bruger_id INNER JOIN film ON anmeldelser.fk_film_id = film_id WHERE film_id = @film_id AND rating.fk_film_id = @film_id";
            conn.Open();
            Repeater_Anmeldelser.DataSource = cmd.ExecuteReader();
            Repeater_Anmeldelser.DataBind();
            conn.Close();

            //Forudfylder rating controllen
            if (Session["bruger"] != null)
            {
                if (!IsPostBack)
                {
                    Bruger bruger = (Bruger)Session["bruger"];
                    cmd.Parameters.AddWithValue("@bruger_id", bruger.ID);
                    cmd.CommandText = "SELECT rating_tal FROM rating WHERE fk_bruger_id = @bruger_id AND fk_film_id = @film_id";
                    conn.Open();
                    object currentRating = cmd.ExecuteScalar();
                    conn.Close();
                    Rating_Stjerner.CurrentRating = Convert.ToInt32(currentRating);
                }
            }
        }
        else
        {
            Response.Redirect("Filmliste.aspx");
        }


    }
    protected void Repeater_Film_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Nested repeaters der tilknytter billeder til film i repeater i pageload

        //Posterbillede
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            Repeater nested = e.Item.FindControl("Repeater_nested") as Repeater;

            //Finder billedet med første prioritet
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM billeder INNER JOIN film ON fk_film_id = film_id WHERE film_id = @id ORDER BY billede_prioritet ASC", conn);
            cmd.Parameters.AddWithValue("@id", row["film_id"]);
            conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            conn.Close();
        }

        //Stemning
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            Repeater nested = e.Item.FindControl("Repeater_Stemning") as Repeater;

            //Offset gør at vi ser væk fra første række, som vi fandt ovenover.
            SqlCommand cmd = new SqlCommand("SELECT * FROM billeder INNER JOIN film ON fk_film_id = film_id WHERE film_id = @id ORDER BY billede_prioritet ASC OFFSET 1 ROWS", conn);
            cmd.Parameters.AddWithValue("@id", row["film_id"]);
            conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            conn.Close();
        }
        // det er kun Item og AlternatingItem der skal håndteres
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            Repeater nested = e.Item.FindControl("Repeater_Genrer") as Repeater;

            SqlCommand cmd = new SqlCommand("SELECT * FROM film INNER JOIN film_genrer ON film_genrer.fk_film_id = film_id INNER JOIN genrer ON fk_genre_id = genre_id WHERE film_id = @id", conn);
            cmd.Parameters.AddWithValue("@id", row["film_id"]);
            conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            conn.Close();
        }

        if (Session["bruger"] != null)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = e.Item.DataItem as DataRowView;
                Repeater nested = e.Item.FindControl("Repeater_Rating") as Repeater;

                //Offset gør at vi ser væk fra første række, som vi fandt ovenover.
                SqlCommand cmd = new SqlCommand("SELECT * FROM rating INNER JOIN film ON fk_film_id = film_id INNER JOIN brugere ON fk_bruger_id = bruger_id WHERE film_id = @id AND fk_bruger_id = @bruger_id", conn);
                cmd.Parameters.AddWithValue("@id", row["film_id"]);

                Bruger bruger = (Bruger)Session["bruger"];

                cmd.Parameters.AddWithValue("@bruger_id", bruger.ID);
                conn.Open();
                nested.DataSource = cmd.ExecuteReader();
                nested.DataBind();
                conn.Close();
            }
        }
    }

    /// <summary>
    /// Rating funktion: Indsætter eller opdaterer rating tabellen
    /// </summary>
    /// <param name="film_id"></param>
    /// <param name="rating"></param>
    private void Rating(object film_id, object rating)
    {
        //Tjekker om brugeren er logget ind
        if (Session["bruger"] != null)
        {
            //Henter bruger fra sessionen
            Bruger bruger = (Bruger)Session["bruger"];

            //Laver et tjek på om brugeren har stemt på denne film før
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@bruger_id", bruger.ID);
            cmd.Parameters.AddWithValue("@film_id", Request.QueryString["id"]);
            cmd.Parameters.AddWithValue("@rating", Rating_Stjerner.CurrentRating);

            cmd.CommandText = "SELECT rating_id FROM rating WHERE fk_bruger_id = @bruger_id AND fk_film_id = @film_id";

            conn.Open();
            object rating_id = cmd.ExecuteScalar();
            conn.Close();

            if (rating_id == null)
            {
                //Hvis brugeren ikke har stemt før, indsættes scoren med relationer til bruger og film
                cmd.CommandText = "INSERT INTO rating (fk_bruger_id, fk_film_id, rating_tal) VALUES (@bruger_id, @film_id, @rating)";
            }
            else
            {
                //ellers opdateres scoren
                cmd.CommandText = "UPDATE rating SET rating_tal = @rating WHERE fk_bruger_id = @bruger_id AND fk_film_id = @film_id";
            }
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        else
        {
            //Fejlbesked hvis man ikke er logget på
            Label_Rating_Error.Text = "Du skal være logget på for at stemme!";
        }
    }

    protected void Rating_Stjerner_Click(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            Rating(Request.QueryString["id"], Rating_Stjerner.CurrentRating);
            if (Session["bruger"] != null)
            {
                Helpers.Return();
            }
        }

    }
}