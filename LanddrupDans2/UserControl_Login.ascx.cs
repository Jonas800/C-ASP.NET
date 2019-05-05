using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Login : System.Web.UI.UserControl
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button_Login_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "SELECT * FROM users WHERE user_email = @user_login AND user_password = @user_password AND fk_role_id = @fk_role_id";

        cmd.Parameters.AddWithValue("@user_login", TextBox_Login_Name.Text);
        cmd.Parameters.AddWithValue("@user_password", TextBox_Login_Password.Text);
        cmd.Parameters.AddWithValue("@fk_role_id", 1);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Session["user_id"] = reader["user_id"];
            Session["role_id"] = reader["fk_role_id"];
            Response.Redirect("DefaultAdmin.aspx");
        }
        else
        {
            conn.Close();
            cmd.CommandText = "SELECT * FROM users WHERE user_email = @user_login AND user_password = @user_password AND fk_role_id = @fk_role_id2";
            cmd.Parameters.AddWithValue("@fk_role_id2", 2);
            conn.Open();
            SqlDataReader reader2 = cmd.ExecuteReader();
            if (reader2.Read())
            {
                Session["user_id"] = reader2["user_id"];
                Session["role_id"] = reader2["fk_role_id"];
                Response.Redirect("Kundeprofil.aspx");
            }
            else
            {
                Label_Message.Text = "Forkert indtastet brugernavn eller kodeord";
            }
            conn.Close();
        }
        conn.Close();
    }
}