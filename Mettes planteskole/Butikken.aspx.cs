using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Butikken : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        //Til undermenu
        cmd.CommandText = "SELECT * FROM kategorier WHERE kategori_er_aktiv = 1";
        conn.Open();
        Repeater_UnderMenu.DataSource = cmd.ExecuteReader();
        Repeater_UnderMenu.DataBind();
        conn.Close();

        if (!IsPostBack)
        {
            cmd.Parameters.AddWithValue("@er_aktiv", true);

            if (Request.QueryString["Kategori"] != null)
            {
                cmd.Parameters.AddWithValue("@id", Request.QueryString["Kategori"]);
                cmd.CommandText = "SELECT * FROM produkter WHERE fk_kategori_id = @id AND produkt_er_aktiv = @er_aktiv";
                conn.Open();
                Repeater_Produkter.DataSource = cmd.ExecuteReader();
                Repeater_Produkter.DataBind();
                conn.Close();
            }
            else
            {
                //Viser produkter fra første kategori hvis ingen kategori er valgt
                cmd.CommandText = "SELECT * FROM produkter WHERE produkt_er_aktiv = @er_aktiv AND fk_kategori_id IN (SELECT TOP 1 kategori_id FROM kategorier ORDER BY kategori_id)";
                conn.Open();
                Repeater_Produkter.DataSource = cmd.ExecuteReader();
                Repeater_Produkter.DataBind();
                conn.Close();
            }
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in Repeater_Produkter.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                int id = 0;
                int antal = 0;
                decimal pris = 0;

                int.TryParse((item.FindControl("HiddenField_ID") as HiddenField).Value, out id);
                string navn = (item.FindControl("Literal_Navn") as Literal).Text;
                int.TryParse((item.FindControl("TextBox_Antal") as TextBox).Text, out antal);
                decimal.TryParse((item.FindControl("Literal_Pris") as Literal).Text, out pris);

                if (antal > 0)
                {
                    Kurv.PutFlereVareIKurv(id, navn, pris, antal);
                }
            }
        }
        Response.Redirect(Request.RawUrl);
    }

    public string HighlightUnderMenu(string kategori_id)
    {
        if (Request.QueryString["Kategori"] != null)
        {
            if (Request.QueryString["Kategori"] == kategori_id)
            {
                return "HighlightUnderMenu";
            }
        }
        else
        {
            SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 kategori_id FROM kategorier ORDER by kategori_id", conn2);
            conn2.Open();
            object firstID = cmd.ExecuteScalar();
            conn2.Close();

            if (kategori_id == firstID.ToString())
            {
                return "HighlightUnderMenu";
            }
        }
        return "";
    }
    public string Udsolgt(int id)
    {
        SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("SELECT * FROM produkter WHERE produkt_id = @id", conn2);
        cmd.Parameters.AddWithValue("@id", id);
        conn2.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            if (Convert.ToInt32(reader["produkt_lager_min"]) > Convert.ToInt32(reader["produkt_lager_stand"]))
            {
                return " (udsolgt)";
            }
        }
        conn2.Close();

        return "";
    }
    public string UdsolgtClass(int id)
    {
        SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand("SELECT * FROM produkter WHERE produkt_id = @id", conn2);
        cmd.Parameters.AddWithValue("@id", id);
        conn2.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            if (Convert.ToInt32(reader["produkt_lager_min"]) > Convert.ToInt32(reader["produkt_lager_stand"]))
            {
                return "udsolgt";
            }
        }
        conn2.Close();

        return "";
    }
}