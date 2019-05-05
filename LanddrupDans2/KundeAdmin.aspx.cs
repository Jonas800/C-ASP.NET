using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class KundeAdmin : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["column"] != null && Request.QueryString["direction"] != null)
        {
            string column = Request.QueryString["column"];
            string direction = Request.QueryString["direction"];
            Repeater_List.DataSource = db.SelectAllFromOrderBy("customers INNER JOIN users ON fk_user_id = user_id", column, direction);
            Repeater_List.DataBind();
        }
        else
        {
            Repeater_List.DataSource = db.SelectAllFrom("customers INNER JOIN users ON fk_user_id = user_id");
            Repeater_List.DataBind();
        }
        if (Request.QueryString["opret"] != null)
        {
            Repeater_List.Visible = false;
            Panel_Users.Visible = true;
        }
        if (Request.QueryString["ret"] != null)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt16(Request.QueryString["id"]);
                DataRow reader = db.SelectSingleRowFrom("customers", "customer_id", id);

                if (reader.ItemArray.Length > 0)
                {
                    TextBox_User_Firstname.Text = reader["customer_firstname"].ToString();
                    TextBox_User_Lastname.Text = reader["customer_lastname"].ToString();
                    TextBox_User_Postal.Text = reader["customer_postal"].ToString();
                    TextBox_User_Phone.Text = reader["customer_phone"].ToString();
                    TextBox_User_City.Text = reader["customer_city"].ToString();
                    TextBox_User_Address.Text = reader["customer_address"].ToString();

                    HiddenField_FK_user_id.Value = reader["fk_user_id"].ToString();
                }

                id = Convert.ToInt32(HiddenField_FK_user_id.Value);
                DataRow reader_users = db.SelectSingleRowFrom("users", "user_id", id);
                if (reader.ItemArray.Length > 0)
                {
                    TextBox_Login_Name.Text = reader_users["user_email"].ToString();
                }
            }

            Repeater_List.Visible = false;
            Panel_Users.Visible = true;
        }
        if (Request.QueryString["slet"] != null)
        {
            if (Request.QueryString["id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);

                DataRow reader = db.SelectSingleRowFrom("customers", "customer_id", id);

                int fk_user_id = Convert.ToInt16(reader["fk_user_id"]);

                db.DeleteFromTable("customers", "customer_id", id);
                db.DeleteFromTable("users", "user_id", fk_user_id);
                db.DeleteFromTable("teams_and_customers", "fk_customer_id", id);
                Response.Redirect("KundeAdmin.aspx");
            }
        }
    }
    protected void Button_User_Create_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@customer_firstname", TextBox_User_Firstname.Text);
        cmd.Parameters.AddWithValue("@customer_lastname", TextBox_User_Lastname.Text);
        cmd.Parameters.AddWithValue("@customer_city", TextBox_User_City.Text);
        cmd.Parameters.AddWithValue("@customer_address", TextBox_User_Address.Text);
        cmd.Parameters.AddWithValue("@customer_postal", TextBox_User_Postal.Text);
        cmd.Parameters.AddWithValue("@customer_phone", TextBox_User_Phone.Text);
        if (Request.QueryString["ret"] != null)
        {
            if (Request.QueryString["id"] != null)
            {
                cmd.CommandText = "UPDATE customers SET customer_firstname = @customer_firstname, customer_lastname = @customer_lastname, customer_city = @customer_city, customer_address = @customer_address, customer_postal = @customer_postal, customer_phone = @customer_phone WHERE customer_id = @id";
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                db.UpdateTable(cmd);

                cmd.Parameters.AddWithValue("@user_email", TextBox_Login_Name.Text);
                cmd.Parameters.AddWithValue("@user_password", TextBox_User_Password.Text);
                cmd.Parameters.AddWithValue("@fk_user_id", HiddenField_FK_user_id.Value);
                if (TextBox_User_Password.Text == String.Empty)
                {
                    cmd.CommandText = "UPDATE users SET user_email = @user_email WHERE user_id = @fk_user_id";
                    db.UpdateTable(cmd);
                }
                else
                {
                    cmd.CommandText = "UPDATE users SET user_email = @user_email, user_password = @user_password WHERE user_id = @fk_user_id";
                    db.UpdateTable(cmd);
                }
                Response.Redirect("KundeAdmin.aspx");
            }
        }
        else
        {
            cmd.CommandText = "INSERT INTO users (user_email, user_password, fk_role_id) VALUES(@user_email, @user_password, @fk_role_id)";
            cmd.Parameters.AddWithValue("@user_email", TextBox_Login_Name.Text);
            cmd.Parameters.AddWithValue("@user_password", TextBox_User_Password.Text);
            cmd.Parameters.AddWithValue("@fk_role_id", 2);
            int user_id = db.InsertIntoTable(cmd);

            if (TextBox_User_Phone.Text == String.Empty)
            {
                cmd.CommandText = "INSERT INTO customers (customer_firstname, customer_lastname, customer_city, customer_address, customer_postal, fk_user_id) VALUES(@customer_firstname, @customer_lastname, @customer_city, @customer_address, @customer_postal, @fk_user_id)";
                cmd.Parameters.AddWithValue("@fk_user_id", user_id);
                db.InsertIntoTable(cmd);
            }
            else
            {
                cmd.CommandText = "INSERT INTO customers (customer_firstname, customer_lastname, customer_city, customer_address, customer_postal, customer_phone, fk_user_id) VALUES(@customer_firstname, @customer_lastname, @customer_city, @customer_address, @customer_postal, @customer_phone, @fk_user_id)";
                cmd.Parameters.AddWithValue("@fk_user_id", user_id);
                db.InsertIntoTable(cmd);
            }
            Response.Redirect("KundeAdmin.aspx");
        }
    }
}