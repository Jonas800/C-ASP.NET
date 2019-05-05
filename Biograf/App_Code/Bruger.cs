using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// Summary description for Bruger
/// </summary>
public class Bruger
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    public Bruger()
    {
        //
        // TODO: Add constructor logic here
        //
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
            this.HentGenreFraDB();
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
    public void GemProfil()
    {
        //Hvis man ikke allerede håndterer et gyldigt objekt
        if (this.ID == 0)
        {
            //Bruger oprettes med de sendte data og rolle sættes til 1 (bruger)
            SqlCommand cmd = new SqlCommand("INSERT INTO brugere (bruger_email, bruger_kodeord, bruger_navn, bruger_nyhedsbrev, fk_rolle_id) VALUES(@email, @kodeord, @navn, @nyhedsbrev, @rolle); SELECT SCOPE_IDENTITY()", conn);

            cmd.Parameters.AddWithValue("@email", this.Email);
            cmd.Parameters.AddWithValue("@kodeord", this.Kodeord);
            cmd.Parameters.AddWithValue("@nyhedsbrev", this.Nyhedsbrev);
            cmd.Parameters.AddWithValue("@navn", this.Navn);
            cmd.Parameters.AddWithValue("@rolle", 1);
            conn.Open();
            this.ID = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
        }
    }
    public void GemProfilMedRolle()
    {
        //Hvis man ikke allerede håndterer et gyldigt objekt
        if (this.ID == 0)
        {
            //Bruger oprettes med de sendte data og rolle sættes til 1 (bruger)
            SqlCommand cmd = new SqlCommand("INSERT INTO brugere (bruger_email, bruger_kodeord, bruger_navn, bruger_nyhedsbrev, fk_rolle_id) VALUES(@email, @kodeord, @navn, @nyhedsbrev, @rolle); SELECT SCOPE_IDENTITY()", conn);

            cmd.Parameters.AddWithValue("@email", this.Email);
            cmd.Parameters.AddWithValue("@kodeord", this.Kodeord);
            cmd.Parameters.AddWithValue("@nyhedsbrev", this.Nyhedsbrev);
            cmd.Parameters.AddWithValue("@navn", this.Navn);
            //rolle_id sættes til tilsvarende rolle_navn
            if (this.Rolle_Navn == rolle.Administrator)
            {
                this.Rolle_ID = 3;
            }
            else if (this.Rolle_Navn == rolle.Premium)
            {
                this.Rolle_ID = 2;
            }
            else
            {
                this.Rolle_ID = 1;
            }
            cmd.Parameters.AddWithValue("@rolle", this.Rolle_ID);
            conn.Open();
            this.ID = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
        }
    }

    /// <summary>
    /// Til login: Spørger om loginnøgle og kodeord eksisterer i databasen; hvis de gør, udfyldes objektet med data. Hvis ikke sættes objektet til null og 0.
    /// </summary>
    public void ProfilLogin()
    {
        //Finder brugeren frem
        SqlCommand cmd = new SqlCommand("SELECT * FROM brugere WHERE bruger_email = @email AND bruger_kodeord = @kodeord", conn);
        cmd.Parameters.AddWithValue("@email", this.Email);
        cmd.Parameters.AddWithValue("@kodeord", this.Kodeord);
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        //Hvis den findes
        if (reader.Read())
        {
            //Knyttes readerens værdier til objektets egenskaber
            this.Point = Convert.ToInt32(reader["bruger_point"]);
            this.Nyhedsbrev = Convert.ToBoolean(reader["bruger_nyhedsbrev"]);
            this.Email = reader["bruger_email"].ToString();
            this.ID = Convert.ToInt32(reader["bruger_id"]);
            this.Navn = reader["bruger_navn"].ToString();
            this.Rolle_ID = Convert.ToInt16(reader["fk_rolle_id"]);

            //Sørger for at rolle_id og rolle_navn matcher
            if (this.Rolle_ID == 3)
            {
                this.Rolle_Navn = rolle.Administrator;
            }
            else if (this.Rolle_ID == 2)
            {
                this.Rolle_Navn = rolle.Premium;
            }
            else
            {
                this.Rolle_Navn = rolle.Standard;
            }
        }
        else
        {
            //Ellers sættes objektet til tomt
            this.Email = null;
            this.Kodeord = null;
            this.ID = 0;
        }
        conn.Close();
        //Henter genrer hvis brugeren endte med at være gyldig
        if (this.ID > 0)
        {
            this.HentGenreFraDB();
        }
    }
    /// <summary>
    /// Opdaterer databasen med objektet enten med kodeord eller uden kodeord, ud fra objektets id
    /// </summary>
    public void UpdateObjekt()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        //Opdaterer kodeord hvis kodeord er længere end nul, ellers undlades kodeord
        if (this.Kodeord.Length != 0)
        {
            cmd.CommandText = "UPDATE brugere SET bruger_navn = @navn, bruger_kodeord = @kodeord, bruger_email = @email, bruger_nyhedsbrev = @nyhedsbrev WHERE bruger_id = @id";
        }
        else
        {
            cmd.CommandText = "UPDATE brugere SET bruger_navn = @navn, bruger_email = @email, bruger_nyhedsbrev = @nyhedsbrev WHERE bruger_id = @id";
        }
        cmd.Parameters.AddWithValue("@email", this.Email);
        cmd.Parameters.AddWithValue("@kodeord", this.Kodeord);
        cmd.Parameters.AddWithValue("@nyhedsbrev", this.Nyhedsbrev);
        cmd.Parameters.AddWithValue("@navn", this.Navn);
        cmd.Parameters.AddWithValue("@id", this.ID);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Opdaterer databasen med objektet enten med kodeord eller uden kodeord, ud fra sendt id
    /// </summary>
    /// <param name="id"></param>
    public void UpdateObjekt(int id)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        //Opdaterer kodeord hvis kodeord er længere end nul, ellers undlades kodeord
        if (this.Kodeord.Length != 0)
        {
            cmd.CommandText = "UPDATE brugere SET bruger_navn = @navn, bruger_kodeord = @kodeord, bruger_email = @email, fk_rolle_id = @rolle WHERE bruger_id = @id";
        }
        else
        {
            cmd.CommandText = "UPDATE brugere SET bruger_navn = @navn, bruger_email = @email, fk_rolle_id = @rolle WHERE bruger_id = @id";
        }
        cmd.Parameters.AddWithValue("@email", this.Email);
        cmd.Parameters.AddWithValue("@kodeord", this.Kodeord);
        //cmd.Parameters.AddWithValue("@nyhedsbrev", this.Nyhedsbrev); , bruger_nyhedsbrev = @nyhedsbrev
        cmd.Parameters.AddWithValue("@navn", this.Navn);
        cmd.Parameters.AddWithValue("@id", id);
        //Sørger for at rolle_navn og rolle_id matcher
        if (this.Rolle_Navn == rolle.Administrator)
        {
            this.Rolle_ID = 3;
        }
        else if (this.Rolle_Navn == rolle.Premium)
        {
            this.Rolle_ID = 2;
        }
        else
        {
            this.Rolle_ID = 1;
        }

        cmd.Parameters.AddWithValue("@rolle", this.Rolle_ID);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Sletter bruger og bruger/genrer-relationer fra databasen ud fra objektets id
    /// </summary>
    public void SletObjekt()
    {
        //Sletter fra databasen og alle dets relationer
        SqlCommand cmd = new SqlCommand("DELETE FROM brugere WHERE bruger_id = @id; DELETE FROM brugere_genrer WHERE fk_bruger_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", this.ID);
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
        SqlCommand cmd = new SqlCommand("DELETE FROM brugere_genrer WHERE fk_bruger_id = @id; DELETE FROM anmeldelser WHERE fk_bruger_id = @id; DELETE FROM rating WHERE fk_bruger_id = @id; DELETE FROM brugere WHERE bruger_id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    /// <summary>
    /// Opdaterer bruger point.
    /// </summary>
    public void PointUpdate()
    {
        //Opdaterer bruger point
        SqlCommand cmd = new SqlCommand("UPDATE brugere SET bruger_point = @point WHERE bruger_id = @id", conn);
        cmd.Parameters.AddWithValue("@point", this.Point);
        cmd.Parameters.AddWithValue("@id", this.ID);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Henter genrer fra database og ligger dem over i objektets egenskab Genre
    /// </summary>
    public void HentGenreFraDB()
    {
        this.Genre = new List<Genre>();

        //Hvis brugeren er gyldig
        if (this.ID != 0)
        {
            //Finder alle genrer tilhørende denne bruger ud fra en mellem tabel
            SqlCommand cmd = new SqlCommand("SELECT * FROM brugere_genrer INNER JOIN genrer ON fk_genre_id = genre_id INNER JOIN brugere ON fk_bruger_id = bruger_id WHERE bruger_id = @id;", conn);
            cmd.Parameters.AddWithValue("@id", this.ID);
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataReader reader = cmd.ExecuteReader();

            //Udfylder listen med navn og id hver gang reader læser
            while (reader.Read())
            {
                this.Genre.Add(new Genre(reader["genre_navn"].ToString(), Convert.ToInt32(reader["genre_id"])));
            }
            conn.Close();
        }
    }
    /// <summary>
    /// Opretter genre/bruger relationer i databasen ud fra et sendt id og objektets id
    /// </summary>
    /// <param name="genreId"></param>
    public void InsertGenre(int genreId)
    {
        //Indsætter værdier til genre/bruger mellemtabellen
        SqlCommand cmd = new SqlCommand("INSERT INTO brugere_genrer (fk_bruger_id, fk_genre_id) VALUES(@bruger_id, @genre_id)", conn);
        cmd.Parameters.AddWithValue("@genre_id", genreId);
        cmd.Parameters.AddWithValue("@bruger_id", this.ID);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
    /// Sletter den specifikke genre/bruger relation ud fra et sendt id og objektets id
    /// </summary>
    /// <param name="genreId"></param>
    public void SletGenre(int genreId)
    {
        //Sletter fra genre/bruger mellemtabellen
        SqlCommand cmd = new SqlCommand("DELETE FROM brugere_genrer WHERE fk_genre_id = @genre_id AND fk_bruger_id = @bruger_id", conn);
        cmd.Parameters.AddWithValue("@genre_id", genreId);
        cmd.Parameters.AddWithValue("@bruger_id", this.ID);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
    }
    /// <summary>
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
            this.Nyhedsbrev = Convert.ToBoolean(reader["bruger_nyhedsbrev"]);
            this.Rolle_ID = Convert.ToInt32(reader["fk_rolle_id"]);
            this.Point = Convert.ToInt32(reader["bruger_point"]);
            //Sørger for at rolle_id og rolle_navn matcher
            if (this.Rolle_ID == 3)
            {
                this.Rolle_Navn = rolle.Administrator;
            }
            else if (this.Rolle_ID == 2)
            {
                this.Rolle_Navn = rolle.Premium;
            }
            else
            {
                this.Rolle_Navn = rolle.Standard;
            }
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
    public int Point { get; set; }
    public rolle Rolle_Navn { get; set; }
    public int Rolle_ID { get; set; }
    public bool Nyhedsbrev { get; set; }
    public List<Genre> Genre { get; set; }
}
public enum rolle
{
    Standard, Premium, Administrator
}