using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;

        cmd.CommandText = "SELECT TOP 8 * FROM pictures ORDER BY newid()";
        conn.Open();
        Repeater_Pictures.DataSource = cmd.ExecuteReader();
        Repeater_Pictures.DataBind();
        conn.Close();

        if (Request.QueryString["category"] != null)
        {
            Repeater_Pictures.Visible = false;
            Repeater_Filtered_Pictures_Large.Visible = true;

            SqlCommand cmd_filter = new SqlCommand();
            cmd_filter.Connection = conn;
            cmd_filter.CommandText = "SELECT * FROM pictures INNER JOIN categories ON category_id = fk_category_id WHERE fk_category_id = @category_id ORDER BY picture_name ASC";
            cmd_filter.Parameters.AddWithValue("@category_id", Request.QueryString["category"]);

            conn.Open();
            Repeater_Filtered_Pictures_Large.DataSource = cmd_filter.ExecuteReader();
            Repeater_Filtered_Pictures_Large.DataBind();
            conn.Close();
        }
        if (Request.QueryString["slider"] != null)
        {
            Repeater_Pictures.Visible = false;
            Repeater_Slider.Visible = true;

            SqlCommand cmd_filter = new SqlCommand();
            cmd_filter.Connection = conn;
            cmd_filter.CommandText = "SELECT * FROM pictures INNER JOIN categories ON category_id = fk_category_id WHERE fk_category_id = @category_id ORDER BY picture_name ASC";
            cmd_filter.Parameters.AddWithValue("@category_id", Request.QueryString["slider"]);

            conn.Open();
            Repeater_Slider.DataSource = cmd_filter.ExecuteReader();
            Repeater_Slider.DataBind();
            conn.Close();
        }
    }
}