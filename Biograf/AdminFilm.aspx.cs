using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

public partial class AdminFilm : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            Bruger bruger = (Bruger)Session["bruger"];
            if (bruger.Rolle_Navn == rolle.Administrator)
            {
                //Sikrer os at alt indtastet info ikke bliver genlæst når der trykkes gem
                if (!IsPostBack)
                {
                    //Udfylder checkboxlisten med alle genrer
                    CheckBoxList_Genre.DataSource = db.SelectAllFrom("genrer");
                    CheckBoxList_Genre.DataBind();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    switch (Request.QueryString["handling"])
                    {
                        #region ret
                        case "ret":
                            //Opretter et film objekt og udfylder det
                            Film film = new Film();
                            film.HentFilm(Convert.ToInt32(Request.QueryString["id"]));

                            TextBox_Navn.Text = film.Navn;
                            TextBox_Beskrivelse.Text = film.Beskrivelse;

                            //Forudfylder checkboxlisten med det data der ligger i databasen
                            foreach (var item in film.Genre)
                            {
                                ListItem li = CheckBoxList_Genre.Items.FindByText(item.Navn);

                                if (li != null)
                                {
                                    li.Selected = true;
                                }
                            }
                            Panel_Billede.Visible = false;
                            break;
                        #endregion
                        #region slet
                        case "slet":
                            //Finder alle billede stierne og sletter dem derefter
                            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                            cmd.CommandText = "SELECT * FROM billeder WHERE fk_film_id = @id";
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                Helpers.DeletePictures(reader["billede_sti"]);
                            }
                            conn.Close();
                            //Sletter alle film og alle relationer
                            cmd.CommandText = "DELETE FROM film_genrer WHERE film_genrer.fk_film_id = @id; DELETE FROM billeder WHERE billeder.fk_film_id = @id; DELETE FROM anmeldelser WHERE fk_film_id = @id; DELETE FROM rating WHERE fk_film_id = @id; DELETE FROM film WHERE film_id = @id";
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            Response.Redirect("AdminFilm.aspx");
                            break;
                        #endregion
                        #region default
                        default:
                            //Finder alle film frem
                            cmd.CommandText = "SELECT * FROM film";
                            Repeater_Film.DataSource = db.SelectTable(cmd);
                            Repeater_Film.DataBind();
                            break;
                        #endregion
                    }
                }
            }
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        //Opretter et filmobjekt og fylder det ud sendt data
        Film film = new Film();
        film.Navn = TextBox_Navn.Text;
        film.Beskrivelse = TextBox_Beskrivelse.Text;

        //Opretter et billedeobjekt, udfyldning sker længere nede
        Billede billede = new Billede();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        #region genre objekt

        //Opretter en List af typen Genre og ligger den over i film objektets egenskab .genre med de data der er blevet sendt
        List<Genre> genre = new List<Genre>();

        film.Genre = genre;

        //Udfylder listen genre med sendt data
        foreach (ListItem item in CheckBoxList_Genre.Items)
        {
            if (item.Selected)
            {
                genre.Add(new Genre(item.Text, Convert.ToInt32(item.Value)));
            }
        }
        film.Genre = genre;
        #endregion
        switch (Request.QueryString["handling"])
        {
            #region ret
            case "ret":
                //Opdaterer film med sendt data
                film.UpdateFilm(Convert.ToInt32(Request.QueryString["id"]), film.Navn, film.Beskrivelse);

                //Sletter alle tilhørende genrer for derefter at ligge nye ind. Update kan ikke bruges da man jo ligger ændrer på en masse rækker, tilføjer nye og fjerner andre. Ellers ville man kun kunne ændre på de få man valgte til at starte med.
                cmd.CommandText = "DELETE FROM film_genrer WHERE fk_film_id = @film_id; ";
                cmd.Parameters.AddWithValue("@film_id", Request.QueryString["id"]);

                //Denne foreach tilføjer en CommandText hver gang den finder en værdi i listen genre, som vi lavede tidligere.
                foreach (Genre item in genre)
                {
                    cmd.CommandText += "INSERT INTO film_genrer (fk_film_id, fk_genre_id) VALUES(@film_id" + item.ID.ToString() + ", @genre_id" + item.ID.ToString() + "); ";
                    cmd.Parameters.AddWithValue("@genre_id" + item.ID.ToString(), item.ID);
                    cmd.Parameters.AddWithValue("@film_id" + item.ID.ToString(), Request.QueryString["id"]);
                }
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                Response.Redirect("AdminFilm.aspx");

                break;
            #endregion
            #region opret
            default:
                //Gemmer film objektet vi lavede i toppen.
                film.GemFilm();

                //Sender et billede med og ligger dataen dertil ind i billedetabellen. Dette kan også gøres senere.
                if (FileUpload_Billede.HasFile)
                {
                    //Der skal kun tjekkes for om prioritet er udfyldt korrekt hvis der er et billede
                    RequiredFieldValidator_Prioritet.Enabled = true;
                    RegularExpressionValidator_Prioritet.Enabled = true;

                    string NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Billede.FileName);
                    FileUpload_Billede.SaveAs(Server.MapPath("~/billeder/") + NewFileName);
                    Helpers.Resizing(NewFileName);
                    billede.Film = film.ID;
                    billede.Prioritet = Convert.ToInt32(TextBox_Prioritet.Text);
                    billede.Sti = NewFileName;
                    billede.GemBillede();
                }

                if (CheckBoxList_Genre.SelectedItem != null)
                {
                    //Denne foreach tilføjer en CommandText hver gang den finder en værdi i listen genre, som vi lavede tidligere.
                    foreach (Genre item in genre)
                    {
                        cmd.CommandText += "INSERT INTO film_genrer (fk_film_id, fk_genre_id) VALUES(@film_id" + item.ID.ToString() + ", @genre_id" + item.ID.ToString() + "); ";
                        cmd.Parameters.AddWithValue("@genre_id" + item.ID.ToString(), item.ID);
                        cmd.Parameters.AddWithValue("@film_id" + item.ID.ToString(), film.ID);
                    }
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("AdminFilm.aspx");
                }
                break;
            #endregion
        }
    }
    protected void Repeater_Film_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Her er to nested repeaters, som viser genrer og et billede der tilhører de film, vi tidligere sendte ind i en repeater i pageload.

        // det er kun Item og AlternatingItem der skal håndteres
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            Repeater nested = e.Item.FindControl("Repeater_nested") as Repeater;

            SqlCommand cmd = new SqlCommand("SELECT * FROM film INNER JOIN film_genrer ON film_genrer.fk_film_id = film_id INNER JOIN genrer ON fk_genre_id = genre_id WHERE film_id = @id", conn);
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
            Repeater nested = e.Item.FindControl("Repeater_billede") as Repeater;

            SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM film INNER JOIN billeder ON fk_film_id = film_id WHERE film_id = @id ORDER BY billede_prioritet ASC", conn);
            cmd.Parameters.AddWithValue("@id", row["film_id"]);
            conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            conn.Close();
        }
    }
}