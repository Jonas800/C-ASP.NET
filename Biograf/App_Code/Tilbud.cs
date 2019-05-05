using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for Tilbud
/// </summary>
public class Tilbud
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    public int ID { get; set; }
    public string Billede { get; set; }
    public rolle Rolle { get; set; }
    public Tilbud()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// Hent alle tilbud
    /// </summary>
    public void HentTilbud()
    {
        //Henter alle tilbud fra databasen
        SqlCommand cmd = new SqlCommand("SELECT * FROM tilbud", conn);
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        //Hvis tilbuddet findes, sættes readerens værdier til objektets egenskaber
        if (reader.Read())
        {
            this.ID = Convert.ToInt32(reader["tilbud_id"]);
            this.Billede = reader["tilbud_billede"].ToString();
            this.Rolle = (rolle)Enum.Parse(typeof(rolle), reader["tilbud_rolle"].ToString());

        }
        conn.Close();
    }
    /// <summary>
    /// Hent tilbud fra databasen ud fra id
    /// </summary>
    /// <param name="id"></param>
    public void HentTilbud(int id)
    {
        //Henter alle tilbud fra databasen
        SqlCommand cmd = new SqlCommand("SELECT * FROM tilbud WHERE tilbud_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        //Hvis tilbuddet findes, sættes readerens værdier til objektets egenskaber
        if (reader.Read())
        {
            this.ID = Convert.ToInt32(reader["tilbud_id"]);
            this.Billede = reader["tilbud_billede"].ToString();
            this.Rolle = (rolle)Enum.Parse(typeof(rolle), reader["tilbud_rolle"].ToString());
        }
        conn.Close();
    }
    /// <summary>
    /// Insert tilbud i databasen
    /// </summary>
    public void GemTilbud()
    {
        SqlCommand cmd = new SqlCommand("INSERT INTO tilbud (tilbud_billede, tilbud_rolle) VALUES (@billede, @rolle)", conn);
        cmd.Parameters.AddWithValue("@billede", this.Billede);
        cmd.Parameters.AddWithValue("@rolle", this.Rolle.ToString());
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Slet tilbud fra databasen ud fra en id
    /// </summary>
    /// <param name="id"></param>
    public void SletTilbud(int id)
    {
        SqlCommand cmd = new SqlCommand("DELETE FROM tilbud WHERE tilbud_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Opdater tilbud i databasen ud fra en id
    /// </summary>
    /// <param name="id"></param>
    public void UpdateTilbud(int id)
    {
        SqlCommand cmd = new SqlCommand("UPDATE tilbud SET tilbud_billede = @billede, tilbud_rolle = @rolle WHERE tilbud_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@billede", this.Billede);
        cmd.Parameters.AddWithValue("@rolle", this.Rolle.ToString());
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Opdaterer kun tilbuds tabellens rolle ud fra en id
    /// </summary>
    public void UpdateTilbudRolle(int id)
    {
        SqlCommand cmd = new SqlCommand("UPDATE tilbud SET tilbud_rolle = @rolle WHERE tilbud_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@rolle", this.Rolle.ToString());
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}