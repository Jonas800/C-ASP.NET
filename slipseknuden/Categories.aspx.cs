using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class Categories : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;


        if (Request.QueryString["id"] == null)
        {
            cmd.CommandText = "SELECT * FROM categories ORDER BY category_name";
            conn.Open();
            Repeater_List.DataSource = cmd.ExecuteReader();
            Repeater_List.DataBind();
            conn.Close();
            Panel_List.Visible = true;
        }
        else { 
        cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

        cmd.CommandText = "SELECT * FROM products WHERE fk_category_id = @id";
        conn.Open();
        Repeater_Categories_Products.DataSource = cmd.ExecuteReader();
        Repeater_Categories_Products.DataBind();
        conn.Close();

        cmd.CommandText = "SELECT * FROM categories WHERE category_id = @id";
        conn.Open();
        Repeater_Categories_Title.DataSource = cmd.ExecuteReader();
        Repeater_Categories_Title.DataBind();
        conn.Close();
        }
    }
}