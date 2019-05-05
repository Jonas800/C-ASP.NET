using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Helpers;
/// <summary>
/// Summary description for Bruger
/// </summary>
public class Bruger
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    public Bruger()
    {
        this.ID = 0;
    }
    /// <summary>
    /// Til Login: Henter data fra loginformular og kalder ProfilLogin/metoden, som udfylder Profilobjektet.
    /// </summary>
    /// <param name="navn"></param>
    /// <param name="kodeord"></param>
    public Bruger(string email, string kodeord)
    {
        //Hvis tilsendte email og kodeord IKKE er tomme oprettes et fyldt objekt. Hvis ikke sættes det bare til at være tomt.
        if (email != String.Empty && kodeord != String.Empty)
        {
            this.Email = email;
            this.Kodeord = kodeord;
            this.ProfilLogin();
        }
        else
        {
            this.Email = null;
            this.Kodeord = null;
            this.ID = 0;
        }
    }

    /// <summary>
    /// Til opret: Indsætter objektet bruger i databasen.
    /// </summary>
    public void OpretBruger(string email, string kodeord, string navn, string by, string adresse, int postnummer, int telefon, bool rolle)
    {
        //Hvis man ikke allerede håndterer et gyldigt objekt
        if (this.ID == 0)
        {
            //Bruger oprettes med de sendte data og rolle sættes til false (bruger)
            SqlCommand cmd = new SqlCommand("INSERT INTO brugere (bruger_email, bruger_kodeord, bruger_er_admin) VALUES(@email, @kodeord, @rolle); SELECT SCOPE_IDENTITY()", conn);

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@kodeord", kodeord);
            cmd.Parameters.AddWithValue("@rolle", rolle);
            conn.Open();
            this.ID = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            this.Email = email;
            this.Er_Admin = false;

            //Opretter kunde og kalder en metode til at gemme kunden i databasen
            Kunde kunde = new Kunde(navn, by, adresse, postnummer, telefon, this.ID);
            kunde.OpretKunde();

            //HttpContext.Current.Session["kunde"] = kunde;
        }
    }
    /// <summary>
    /// Til opret: Indsætter objektet bruger i databasen.
    /// </summary>
    public void OpretBrugerForAdmin(string email, string kodeord, string navn, string by, string adresse, int postnummer, int telefon, bool rolle)
    {
        //Hvis man ikke allerede håndterer et gyldigt objekt
        if (this.ID == 0)
        {
            //Bruger oprettes med de sendte data og rolle sættes til false (bruger)
            SqlCommand cmd = new SqlCommand("INSERT INTO brugere (bruger_email, bruger_kodeord, bruger_er_admin) VALUES(@email, @kodeord, @rolle); SELECT SCOPE_IDENTITY()", conn);

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@kodeord", kodeord);
            cmd.Parameters.AddWithValue("@rolle", rolle);
            conn.Open();
            this.ID = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();

            this.Email = email;
            this.Er_Admin = false;

            Kunde kunde = new Kunde(navn, by, adresse, postnummer, telefon, this.ID);
            kunde.OpretKunde();
        }
    }
    /// <summary>
    /// Til login: Spørger om loginnøgle og kodeord eksisterer i databasen; hvis de gør, udfyldes objektet med data. Hvis ikke sættes objektet til null og 0.
    /// </summary>
    public void ProfilLogin()
    {
        //Finder brugeren frem
        SqlCommand cmd = new SqlCommand("SELECT * FROM brugere LEFT OUTER JOIN kunder ON bruger_id = fk_bruger_id WHERE bruger_email = @email AND bruger_er_aktiv = @er_aktiv", conn);
        cmd.Parameters.AddWithValue("@email", this.Email);
        cmd.Parameters.AddWithValue("@er_aktiv", true);
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        //Hvis den findes
        if (reader.Read())
        {
            //Læser det krypterede kodeord
            var doesPasswordMatch = Crypto.VerifyHashedPassword(reader["bruger_kodeord"].ToString(), this.Kodeord);

            if (doesPasswordMatch)
            {

                //Knyttes readerens værdier til objektets egenskaber
                this.Email = reader["bruger_email"].ToString();
                this.ID = Convert.ToInt32(reader["bruger_id"]);
                this.Er_Admin = Convert.ToBoolean(reader["bruger_er_admin"]);

                if (this.Er_Admin == false)
                {
                    //Laver en kunde til sessionen ved at komme informationerne fra readeren i et objekt af typen Kunde
                    Kunde kunde = new Kunde(Convert.ToInt32(reader["kunde_id"]), reader["kunde_navn"].ToString(), reader["kunde_by"].ToString(), reader["kunde_adresse"].ToString(), Convert.ToInt32(reader["kunde_postnummer"]), Convert.ToInt32(reader["kunde_telefon"]), Convert.ToInt32(reader["fk_bruger_id"]));

                    //Gemmer kunde i sessionen kunde
                    HttpContext.Current.Session["kunde"] = kunde;
                }
            }
            else
            {
                //Ellers sættes objektet til tomt
                this.Email = null;
                this.Kodeord = null;
                this.Er_Admin = false;
                this.ID = 0;
            }
        }
        else
        {
            //Ellers sættes objektet til tomt
            this.Email = null;
            this.Kodeord = null;
            this.Er_Admin = false;
            this.ID = 0;
        }
        conn.Close();
    }
    /// <summary>
    /// Opdaterer databasen med objektet enten med kodeord eller uden kodeord, ud fra objektets id
    /// </summary>
    public void UpdateBruger(string email, string kodeord)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        //Opdaterer kodeord hvis kodeord er længere end nul, ellers undlades kodeord
        if (kodeord.Length != 0)
        {
            cmd.CommandText = "UPDATE brugere SET bruger_kodeord = @kodeord, bruger_email = @email WHERE bruger_id = @id";
        }
        else
        {
            cmd.CommandText = "UPDATE brugere SET bruger_email = @email WHERE bruger_id = @id";
        }
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@kodeord", kodeord);
        cmd.Parameters.AddWithValue("@id", this.ID);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Opdaterer databasen med objektet enten med kodeord eller uden kodeord, ud fra sendt id
    /// </summary>
    /// <param name="id"></param>
    public void UpdateBruger(int id, string email, string kodeord)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        //Opdaterer kodeord hvis kodeord er længere end nul, ellers undlades kodeord
        if (kodeord.Length != 0)
        {
            cmd.CommandText = "UPDATE brugere SET bruger_kodeord = @kodeord, bruger_email = @email WHERE bruger_id = @id";
        }
        else
        {
            cmd.CommandText = "UPDATE brugere SET bruger_email = @email WHERE bruger_id = @id";
        }
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@kodeord", kodeord);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Sletter bruger og bruger/genrer-relationer fra databasen ud fra sendt id
    /// </summary>
    /// <param name="id"></param>
    public void SletObjekt(int id)
    {
        //Sletter fra databasen og alle dets relationer
        SqlCommand cmd = new SqlCommand("DELETE FROM brugere WHERE bruger_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// Henter en bruger ud af databasen
    /// </summary>
    /// <param name="id"></param>
    public void HentBruger(int id)
    {
        //Henter en bruger fra databasen og udfylder objektet hvis brugeren findes
        SqlCommand cmd = new SqlCommand("SELECT * FROM brugere WHERE bruger_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            this.Email = reader["bruger_email"].ToString();
            this.ID = Convert.ToInt32(reader["bruger_id"]);
        }
        conn.Close();
    }

    /// <summary>
    /// Egenskaber
    /// </summary>
    public string Kodeord { private get; set; }
    public string Email { get; set; }
    public int ID { get; set; }
    public bool Er_Admin { get; set; }

}
