using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for Billede
/// </summary>
public class Billede
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    public Billede()
    {

    }
    /// <summary>
    /// Billede objekt
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sti"></param>
    /// <param name="prioritet"></param>
    /// <param name="film"></param>
    public Billede(int id, string sti, int prioritet, int film)
    {
        this.ID = id;
        this.Sti = sti;
        this.Prioritet = prioritet;
        this.Film = film;
    }
    public int ID { get; set; }
    public string Sti { get; set; }
    public int Prioritet { get; set; }
    public int Film { get; set; }

    /// <summary>
    /// Gemmer det billede objekt der er blevet oprettet
    /// </summary>
    public void GemBillede()
    {
        //Opretter ny række i billeder og sætter værdierne til objektets værdier
        SqlCommand cmd = new SqlCommand("INSERT INTO billeder (billede_sti, billede_prioritet, fk_film_id) VALUES (@sti, @prioritet, @film_id)", conn);

        cmd.Parameters.AddWithValue("@sti", this.Sti);
        cmd.Parameters.AddWithValue("@prioritet", this.Prioritet);
        cmd.Parameters.AddWithValue("@film_id", this.Film);

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Henter et billede ud fra en billede id
    /// </summary>
    /// <param name="id"></param>
    public void HentBillede(int id)
    {
        //Henter billedet fra billeder ud fra en id
        SqlCommand cmd = new SqlCommand("SELECT * FROM billeder WHERE billede_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            //Knytter udtrækkets værdier til objektets værdier
            this.Film = Convert.ToInt32(reader["fk_film_id"]);
            this.ID = Convert.ToInt32(reader["billede_id"]);
            this.Sti = reader["billede_sti"].ToString();
            this.Prioritet = Convert.ToInt32(reader["billede_prioritet"]);
        }
        conn.Close();
    }
    /// <summary>
    /// Opdaterer billede med nyt billede (eller ny sti) og prioritet
    /// </summary>
    public void UpdateBilledeMedBillede()
    {
        //Opdaterer alle informationer i billeder ud fra en ID
        SqlCommand cmd = new SqlCommand("UPDATE billeder SET billede_sti = @sti, billede_prioritet = @prioritet WHERE billede_id = @id", conn);
        cmd.Parameters.AddWithValue("@sti", this.Sti);
        cmd.Parameters.AddWithValue("@prioritet", this.Prioritet);
        cmd.Parameters.AddWithValue("@id", this.ID);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Opdaterer et billedes billede prioritet
    /// </summary>
    public void UpdateBilledeInfo()
    {
        //Opdaterer kun prioritets kolonnen (ud fra et ID)
        SqlCommand cmd = new SqlCommand("UPDATE billeder SET billede_prioritet = @prioritet WHERE billede_id = @id", conn);
        cmd.Parameters.AddWithValue("@prioritet", this.Prioritet);
        cmd.Parameters.AddWithValue("@id", this.ID);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Sletter et billede ud fra billede id
    /// </summary>
    /// <param name="id"></param>
    public void SletBillede(int id)
    {
        //Sletter billedet fra databasen
        SqlCommand cmd = new SqlCommand("DELETE FROM billeder WHERE billede_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}