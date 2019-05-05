using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Tilmeld : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button_User_Create_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.Parameters.AddWithValue("@user_email", TextBox_Login_Name.Text);
        cmd.Parameters.AddWithValue("@user_password", TextBox_User_Password.Text);
        cmd.Parameters.AddWithValue("@fk_role_id", 2);
        cmd.CommandText = "INSERT INTO users (user_email, user_password, fk_role_id) VALUES(@user_email, @user_password, @fk_role_id)";
        int user_id = db.InsertIntoTable(cmd);

        cmd.Parameters.AddWithValue("@customer_firstname", TextBox_User_Firstname.Text);
        cmd.Parameters.AddWithValue("@customer_lastname", TextBox_User_Lastname.Text);
        cmd.Parameters.AddWithValue("@customer_city", TextBox_User_City.Text);
        cmd.Parameters.AddWithValue("@customer_address", TextBox_User_Address.Text);
        cmd.Parameters.AddWithValue("@customer_postal", TextBox_User_Postal.Text);
        cmd.Parameters.AddWithValue("@customer_phone", TextBox_User_Phone.Text);
        cmd.Parameters.AddWithValue("@fk_user_id", user_id);

        if (TextBox_User_Phone.Text == String.Empty)
        {
            cmd.CommandText = "INSERT INTO customers (customer_firstname, customer_lastname, customer_city, customer_address, customer_postal, fk_user_id) VALUES(@customer_firstname, @customer_lastname, @customer_city, @customer_address, @customer_postal, @fk_user_id)";
            db.InsertIntoTable(cmd);
        }
        else
        {
            cmd.CommandText = "INSERT INTO customers (customer_firstname, customer_lastname, customer_city, customer_address, customer_postal, customer_phone, fk_user_id) VALUES(@customer_firstname, @customer_lastname, @customer_city, @customer_address, @customer_postal, @customer_phone, @fk_user_id)";
            db.InsertIntoTable(cmd);
        }
        Response.Redirect("Login.aspx");


    }
}