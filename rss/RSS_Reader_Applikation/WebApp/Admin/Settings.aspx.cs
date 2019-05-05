using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebApp_Admin_Settings : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "SELECT * FROM feeds";
        conn.Open();
        Repeater_RSS.DataSource = cmd.ExecuteReader();
        Repeater_RSS.DataBind();
        conn.Close();

        if (Request.QueryString["action"] == "slet" && Request.QueryString["id"] != null)
        {
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            cmd.CommandText = "DELETE FROM feeds WHERE feed_id = @id";
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("~/Admin/Default.aspx");
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["action"] == "ret" && Request.QueryString["id"] != null)
            {
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                cmd.CommandText = "SELECT * FROM feeds WHERE feed_id = @id";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    TextBox_Titel.Text = reader["feed_titel"].ToString();
                    TextBox_Url.Text = reader["feed_url"].ToString();
                }
                conn.Close();
            }
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@titel", TextBox_Titel.Text);
        cmd.Parameters.AddWithValue("@url", TextBox_Url.Text);

        if (Request.QueryString["action"] == "ret" && Request.QueryString["id"] != null)
        {
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            cmd.CommandText = "UPDATE feeds SET feed_titel = @titel, feed_url = @url WHERE feed_id = @id";
        }
        else
        {
            cmd.CommandText = "INSERT INTO feeds (feed_titel, feed_url) VALUES(@titel, @url)";
        }
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        Response.Redirect(Request.RawUrl);
    }
}