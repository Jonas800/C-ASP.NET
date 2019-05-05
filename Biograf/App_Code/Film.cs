using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Film
/// </summary>
public class Film
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    public Film()
    {

    }

    /// <summary>
    /// Gemmer film objektet
    /// </summary>
    public void GemFilm()
    {
        //Hvis filmobjekt ikke er gyldigt endnu, oprettes en film i databasen
        if (this.ID == 0)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO film (film_navn, film_beskrivelse) VALUES(@navn, @beskrivelse); SELECT SCOPE_IDENTITY()", conn);

            cmd.Parameters.AddWithValue("@beskrivelse", this.Beskrivelse);
            cmd.Parameters.AddWithValue("@navn", this.Navn);
            conn.Open();
            this.ID = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
        }
    }
    /// <summary>
    /// Opdaterer filmobjektets navn og beskrivelse ud fra en tilsendt id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="navn"></param>
    /// <param name="beskrivelse"></param>
    public void UpdateFilm(int id, string navn, string beskrivelse)
    {
        //Der opdateres ud fra sendt id med sendte data
        SqlCommand cmd = new SqlCommand("UPDATE film SET film_navn = @navn, film_beskrivelse = @beskrivelse WHERE film_id = @id", conn);
        cmd.Parameters.AddWithValue("@beskrivelse", this.Beskrivelse);
        cmd.Parameters.AddWithValue("@navn", this.Navn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Henter film udfra en tilsendt id
    /// </summary>
    /// <param name="id"></param>
    public void HentFilm(int id)
    {
        //Henter bruger fra databasen
        SqlCommand cmd = new SqlCommand("SELECT * FROM film WHERE film_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        //Hvis brugeren findes knyttes dataerne til objektet
        if (reader.Read())
        {
            this.Navn = reader["film_navn"].ToString();
            this.ID = Convert.ToInt32(reader["film_id"]);
            this.Beskrivelse = reader["film_beskrivelse"].ToString();
        }
        conn.Close();
        //Henter genrer tilknyttet filmen
        this.HentGenreFraDB();
    }
    /// <summary>
    /// Sletter filmen i databasen ud fra en tilsendt id. Sletter også alle relationer
    /// </summary>
    /// <param name="id"></param>
    public void SletFilm(int id)
    {
        //Sletter film
        SqlCommand cmd = new SqlCommand("DELETE FROM film WHERE film_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Henter genrer fra database og tilknytter dem en film
    /// </summary>
    public void HentGenreFraDB()
    {
        //Tomt List<Genre> objekt er lig med this.genre
        this.Genre = new List<Genre>();

        if (this.ID != 0)
        {
            //Finder genrer ud fra genrer/film relationer
            SqlCommand cmd = new SqlCommand("SELECT * FROM film_genrer INNER JOIN genrer ON fk_genre_id = genre_id INNER JOIN film ON fk_film_id = film_id WHERE film_id = @id;", conn);
            cmd.Parameters.AddWithValue("@id", this.ID);
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataReader reader = cmd.ExecuteReader();

            //Tilføjer data til List<Genre> med hver fundne række i databasen
            while (reader.Read())
            {
                this.Genre.Add(new Genre(reader["genre_navn"].ToString(), Convert.ToInt32(reader["genre_id"])));
            }
            conn.Close();
        }
    }
    public int ID { get; set; }
    public string Navn { get; set; }
    public string Beskrivelse { get; set; }
    public List<Genre> Genre { get; set; }
}