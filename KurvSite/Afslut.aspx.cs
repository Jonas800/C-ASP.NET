using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Afslut : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button_Afslut_Click(object sender, EventArgs e)
    {
        bool godkendKøb = true;

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.Parameters.Add("@produkt_id", SqlDbType.Int);

        cmd.CommandText = "SELECT produkt_lager_stand FROM produkter WHERE produkt_id = @produkt_id";
        conn.Open();
        foreach (Produkt produktIKurven in Kurv.HentKurven())
        {
            cmd.Parameters["@produkt_id"].Value = produktIKurven.Id;
            object antal = cmd.ExecuteScalar();

            if (Convert.ToInt32(antal) < produktIKurven.Antal)
            {
                godkendKøb = false;
            }
        }
        conn.Close();

        if (godkendKøb)
        {
            //Opret Kunde
            cmd.CommandText = "INSERT INTO kunder (kunde_navn, kunde_adresse, kunde_by, kunde_email) VALUES (@navn, @adresse, @by, @email); SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@navn", TextBox_Navn.Text);
            cmd.Parameters.AddWithValue("@adresse", TextBox_Adresse.Text);
            cmd.Parameters.AddWithValue("@by", TextBox_By.Text);
            cmd.Parameters.AddWithValue("@email", TextBox_Email.Text);

            conn.Open();
            object kundeID = cmd.ExecuteScalar();
            conn.Close();

            //Opret Ordre
            cmd.CommandText = "INSERT INTO ordrer (ordre_dato, fk_kunde_id) VALUES (getdate(), @kunde_id); SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@kunde_id", kundeID);

            conn.Open();
            object ordreID = cmd.ExecuteScalar();
            conn.Close();

            //Opret Ordrelinjer
            cmd.CommandText = "INSERT INTO ordre_linjer (fk_ordre_id, fk_produkt_id, ordre_linje_antal, ordre_linje_pris) VALUES(@ordre_id, @produkt_id, @antal, @pris)";
            cmd.Parameters.AddWithValue("@ordre_id", ordreID);
            cmd.Parameters.Add("@antal", SqlDbType.Int);
            cmd.Parameters.Add("@pris", SqlDbType.Int);

            conn.Open();
            foreach (Produkt produktIKurven in Kurv.HentKurven())
            {
                cmd.Parameters["@produkt_id"].Value = produktIKurven.Id;
                cmd.Parameters["@antal"].Value = produktIKurven.Antal;
                cmd.Parameters["@pris"].Value = produktIKurven.Pris;
                cmd.ExecuteNonQuery();

                cmd.CommandText = "UPDATE produkter SET produkt_lager_stand = produkt_lager_stand - @antal WHERE produkt_id = @produkt_id";
                cmd.ExecuteNonQuery();
            }
            conn.Close();

            Kurv.FjernKurven();

            Panel_Kunde.Visible = false;
            Panel_Tak.Visible = true;
        }
        else
        {
            Response.Write("Der er desværre blevet udsolgt af en eller flere af dine varer og vi kan derfor ikke godkende dit køb");
        }
    }
}