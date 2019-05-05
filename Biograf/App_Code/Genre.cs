using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Genre
/// </summary>
public class Genre
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    public Genre()
    {

    }
	public Genre(string navn)
	{
        this.Navn = navn;
	}
    public Genre(string navn, int id)
    {
        this.Navn = navn;
        this.ID = id;
    }
    // Egenskaber
    public int ID { get; set; }
    public string Navn { get; set; }

    /// <summary>
    /// Gemmer genre i databasen
    /// </summary>
    public void GemGenre()
    {
        if (this.ID == 0)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO genrer (genre_navn) VALUES(@navn); SELECT SCOPE_IDENTITY()", conn);
            cmd.Parameters.AddWithValue("@navn", this.Navn);
            conn.Open();
            this.ID = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
        }
    }
    /// <summary>
    /// Opdaterer genrens navn i databasen ud fra et id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="navn"></param>
    public void UpdateGenre(int id, string navn)
    {
        SqlCommand cmd = new SqlCommand("UPDATE genrer SET genre_navn = @navn WHERE genre_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@navn", navn);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Henter genren ud fra et id
    /// </summary>
    /// <param name="id"></param>
    public void HentGenre(int id)
    {
        //Henter en genre ud fra id
        SqlCommand cmd = new SqlCommand("SELECT * FROM genrer WHERE genre_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        //Hvis genren findes sættes objektets navn og id til readerens værdier
        if (reader.Read())
        {
            this.Navn = reader["genre_navn"].ToString();
            this.ID = Convert.ToInt32(reader["genre_id"]);
        }
        conn.Close();
    }
    /// <summary>
    /// Sletter genren ud fra et id
    /// </summary>
    /// <param name="id"></param>
    public void SletGenre(int id)
    {
        SqlCommand cmd = new SqlCommand("DELETE FROM genrer WHERE genre_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}