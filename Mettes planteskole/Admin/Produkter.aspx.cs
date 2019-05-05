using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Produkter : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //Sessionbeskyttelse
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;

            if (bruger.Er_Admin)
            {
                if (!IsPostBack)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    //Henter alt fra produkter, inner joiner med jordtyper, kategorier og dyrkningstider, og binder det til en repeater
                    cmd.CommandText = "SELECT *, (SELECT COUNT (fk_produkt_id) FROM billeder where fk_produkt_id = produkt_id) as total FROM produkter INNER JOIN jordtyper ON fk_jordtype_id = jordtype_id INNER JOIN kategorier ON fk_kategori_id = kategori_id INNER JOIN dyrkningstider ON fk_dyrkningstid_id = dyrkningstid_id ORDER BY produkt_lager_stand";
                    conn.Open();
                    Repeater_Produkter.DataSource = cmd.ExecuteReader();
                    Repeater_Produkter.DataBind();
                    conn.Close();
                }
            }
        }
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
                return "admin_udsolgt";
            }
        }
        conn2.Close();

        return "";
    }
}