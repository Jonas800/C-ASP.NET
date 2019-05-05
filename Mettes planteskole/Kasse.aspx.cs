using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Kasse : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //Sikrer at repeateren stadig eksisterer ved postbavk/knaptryk
        if (!IsPostBack)
        {
            Repeater_Kurv_Checkout.DataSource = Kurv.HentKurven();
            Repeater_Kurv_Checkout.DataBind();
        }
    }
    protected void Button_Bestil_Click(object sender, EventArgs e)
    {
        if (Session["kunde"] != null)
        {
            //Henter kundeinfo fra session for at finde ID'et
            Kunde kunde = Session["kunde"] as Kunde;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            //Opret Ordre
            cmd.CommandText = "INSERT INTO ordrer (ordre_datetime, fk_kunde_id, fk_status_id) VALUES (getdate(), @kunde_id, @status_id); SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@kunde_id", kunde.ID);
            cmd.Parameters.AddWithValue("@status_id", 1);

            conn.Open();
            object ordreID = cmd.ExecuteScalar();
            conn.Close();

            //Opret Ordrelinjer
            cmd.CommandText = "INSERT INTO ordre_linjer (fk_ordre_id, fk_produkt_id, ordre_linje_antal, ordre_linje_pris) VALUES(@ordre_id, @produkt_id, @antal, @pris)";
            cmd.Parameters.AddWithValue("@ordre_id", ordreID);
            cmd.Parameters.Add("@antal", SqlDbType.Int);
            cmd.Parameters.Add("@pris", SqlDbType.Int);
            cmd.Parameters.Add("@produkt_id", SqlDbType.Int);

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

            Button_Bestil.Visible = false;
            Label_Tak.Visible = true;
        }
        else
        {
            Panel_Bestil.Visible = false;
            Panel_Opret_Or_Login.Visible = true;
        }
    }
}