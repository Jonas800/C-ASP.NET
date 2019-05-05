using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class Login : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button_Login_Click(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "SELECT * FROM users WHERE user_login = @user_login AND user_password = @user_password";

        cmd.Parameters.AddWithValue("@user_login", TextBox_Login_Name.Text);
        cmd.Parameters.AddWithValue("@user_password", TextBox_Login_Password.Text);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Session["user_id"] = reader["user_id"];
            Response.Redirect("Admin.aspx");
        }
        else
        {
            Label_Message.Text = "Forkert indtastet brugernavn eller kodeord";
        }
        conn.Close();
    }
}