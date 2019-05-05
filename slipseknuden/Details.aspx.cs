using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class Details : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null)
        {
            Response.Redirect("Categories.aspx");
        }
        else
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            cmd.CommandText = "SELECT * FROM products WHERE product_id = @id";
            conn.Open();
            Repeater_Details.DataSource = cmd.ExecuteReader();
            Repeater_Details.DataBind();
            conn.Close();
        }

    }
}