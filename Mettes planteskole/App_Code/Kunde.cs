using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Kunde
/// </summary>
public class Kunde
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    public string Navn { get; set; }
    public int ID { get; set; }
    public string By { get; set; }
    public string Adresse { get; set; }
    public int Postnummer { get; set; }
    public int Telefon { get; set; }
    public int Bruger_ID { get; set; }
    /// <summary>
    /// Bruges når der logges ind
    /// </summary>
    /// <param name="id"></param>
    /// <param name="navn"></param>
    /// <param name="by"></param>
    /// <param name="adresse"></param>
    /// <param name="postnummer"></param>
    /// <param name="telefon"></param>
    /// <param name="bruger_id"></param>
    public Kunde(int id, string navn, string by, string adresse, int postnummer, int telefon, int bruger_id)
	{
        this.ID = id;
        this.Navn = navn;
        this.By = by;
        this.Adresse = adresse;
        this.Postnummer = postnummer;
        this.Telefon = telefon;
        this.Bruger_ID = bruger_id;
	}
    /// <summary>
    /// Bruges når der oprettes en kunde
    /// </summary>
    /// <param name="navn"></param>
    /// <param name="by"></param>
    /// <param name="adresse"></param>
    /// <param name="postnummer"></param>
    /// <param name="telefon"></param>
    /// <param name="bruger_id"></param>
    public Kunde(string navn, string by, string adresse, int postnummer, int telefon, int bruger_id)
    {
        this.Navn = navn;
        this.By = by;
        this.Adresse = adresse;
        this.Postnummer = postnummer;
        this.Telefon = telefon;
        this.Bruger_ID = bruger_id;
    }
    /// <summary>
    /// Opretter en kunde i databasen
    /// </summary>
    public void OpretKunde()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@navn", this.Navn);
        cmd.Parameters.AddWithValue("@by", this.By);
        cmd.Parameters.AddWithValue("@adresse", this.Adresse);
        cmd.Parameters.AddWithValue("@postnummer", this.Postnummer);
        cmd.Parameters.AddWithValue("@telefon", this.Telefon);
        cmd.Parameters.AddWithValue("@bruger_id", this.Bruger_ID);

        //Opretter kunde og finder id'en for kunden
        cmd.CommandText = "INSERT INTO kunder (kunde_navn, kunde_by, kunde_adresse, kunde_postnummer, kunde_telefon, fk_bruger_id) VALUES(@navn, @by, @adresse, @postnummer, @telefon, @bruger_id); SELECT SCOPE_IDENTITY()";

        conn.Open();
        object kunde_id = cmd.ExecuteScalar();
        conn.Close();

        //Laver et fuldt kundeobjekt ud fra den id der kom fra databaseforespørgslen
        Kunde kunde = new Kunde(Convert.ToInt32(kunde_id), this.Navn, this.By, this.Adresse, this.Postnummer, this.Telefon, this.Bruger_ID);

        //Gemmer objektet i sessionen
        HttpContext.Current.Session["kunde"] = kunde;
    }
}