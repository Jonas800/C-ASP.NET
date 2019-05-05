using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class MasterPage : System.Web.UI.MasterPage
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["logout"] != null)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
        if (Session["user_id"] != null)
        {
            Panel_Login_Button.Visible = false;
            Panel_Logout_Button.Visible = true;
        }

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "SELECT * FROM categories ORDER BY category_name ASC";
        conn.Open();
        Repeater_Nav.DataSource = cmd.ExecuteReader();
        Repeater_Nav.DataBind();
        conn.Close();

        cmd.CommandText = "SELECT TOP 2 * FROM products ORDER BY newid()";
        conn.Open();
        Repeater_Aside_Right.DataSource = cmd.ExecuteReader();
        Repeater_Aside_Right.DataBind();
        conn.Close();

        if (Session["user_id"] != null)
        {
            cmd.CommandText = "SELECT * FROM users WHERE user_id = @user_id";
            cmd.Parameters.AddWithValue("@user_id", Session["user_id"]);
            conn.Open();
            Repeater_Online.DataSource = cmd.ExecuteReader();
            Repeater_Online.DataBind();
        }
        conn.Close();
    }
}
