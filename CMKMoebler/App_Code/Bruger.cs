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
    /// Opretter bruger og tilbagemelder om det lykkedes
    /// </summary>
    /// <param name="email"></param>
    /// <param name="kodeord"></param>
    /// <param name="navn"></param>
    /// <returns></returns>
    public bool OpretBrugerReturnBool(string email, string kodeord, string navn)
    {

        //Bruger oprettes med de sendte data og rolle sættes til false (bruger)
        SqlCommand cmd = new SqlCommand("INSERT INTO brugere (bruger_email, bruger_kodeord, bruger_navn) VALUES(@email, @kodeord, @navn)", conn);

        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@kodeord", Crypto.HashPassword(kodeord)); 
        cmd.Parameters.AddWithValue("@navn", navn);
        conn.Open();
        int oprettede_rækker = cmd.ExecuteNonQuery();
        conn.Close();

        if (oprettede_rækker > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    /// <summary>
    /// Til login: Spørger om loginnøgle og kodeord eksisterer i databasen; hvis de gør, udfyldes objektet med data. Hvis ikke sættes objektet til null og 0.
    /// </summary>
    public void ProfilLogin()
    {
        //Finder brugeren frem
        SqlCommand cmd = new SqlCommand("SELECT * FROM brugere WHERE bruger_email = @email", conn);
        cmd.Parameters.AddWithValue("@email", this.Email);
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
                this.Navn = reader["bruger_navn"].ToString();
            }
            else
            {
                //Ellers sættes objektet til tomt
                this.Email = null;
                this.Kodeord = null;
                this.Navn = null;
                this.ID = 0;
            }
        }
        else
        {
            //Ellers sættes objektet til tomt
            this.Email = null;
            this.Kodeord = null;
            this.Navn = null;
            this.ID = 0;
        }
        conn.Close();
    }
    
    /// <summary>
    /// Opdaterer bruger og returnerer om det lykkedes.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="kodeord"></param>
    /// <param name="navn"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool UpdateBrugerReturnBool(string email, string kodeord, string navn, int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        //Opdaterer kodeord hvis kodeord er længere end nul, ellers undlades kodeord
        if (kodeord.Length != 0)
        {
            cmd.CommandText = "UPDATE brugere SET bruger_kodeord = @kodeord, bruger_email = @email, bruger_navn = @navn WHERE bruger_id = @id";
        }
        else
        {
            cmd.CommandText = "UPDATE brugere SET bruger_email = @email, bruger_navn = @navn WHERE bruger_id = @id";
        }
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@kodeord", Crypto.HashPassword(kodeord));
        cmd.Parameters.AddWithValue("@navn", navn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        int opdaterede_rækker = cmd.ExecuteNonQuery();
        conn.Close();

        if (opdaterede_rækker > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Sletter bruger og returnerer om det lykkedes.
    /// </summary>
    /// <param name="id"></param>
    public bool SletObjektReturnBool(int id)
    {
        //Sletter fra databasen og alle dets relationer
        SqlCommand cmd = new SqlCommand("DELETE FROM brugere WHERE bruger_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        int slettede_rækker = cmd.ExecuteNonQuery();
        conn.Close();

        if (slettede_rækker > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
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
            this.Navn = reader["bruger_navn"].ToString();
            this.ID = Convert.ToInt32(reader["bruger_id"]);
        }
        conn.Close();
    }

    /// <summary>
    /// Egenskaber
    /// </summary>
    public string Kodeord { private get; set; }
    public string Email { get; set; }
    public string Navn { get; set; }
    public int ID { get; set; }
}
