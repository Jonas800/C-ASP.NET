using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Møbel : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@produkt_id", Request.QueryString["id"]);
        cmd.CommandText = "SELECT * FROM billeder WHERE fk_produkt_id = @produkt_id ORDER BY billede_prioritet DESC";

        conn.Open();
        Repeater_Carousel.DataSource = cmd.ExecuteReader();
        Repeater_Carousel.DataBind();
        conn.Close();

        conn.Open();
        Repeater_Thumbs.DataSource = cmd.ExecuteReader();
        Repeater_Thumbs.DataBind();
        conn.Close();

        cmd.CommandText = "SELECT * FROM produkter INNER JOIN designere ON fk_designer_id = designer_id WHERE produkt_id = @produkt_id";
        conn.Open();
        Repeater_Møbel.DataSource = cmd.ExecuteReader();
        Repeater_Møbel.DataBind();
        conn.Close();
    }
}