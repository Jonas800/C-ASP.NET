using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Kundeprofil : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_id"] == null && Session["role_id"] == null)
        {
            Response.Redirect("Tilmeld.aspx");
        }

        /*KUNDEOPLYSNINGER*/
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT * FROM customers INNER JOIN users ON user_id = fk_user_id WHERE fk_user_id = @user_id";
        cmd.Parameters.AddWithValue("@user_id", Convert.ToInt16(Session["user_id"]));

        Repeater_List.DataSource = db.SelectTable(cmd);
        Repeater_List.DataBind();

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

                if (reader.ItemArray.Length > 0)
                {
                    HiddenField_FK_user_id.Value = reader["fk_user_id"].ToString();
                }
                int fk_user_id = Convert.ToInt32(HiddenField_FK_user_id.Value);

                db.DeleteFromTable("customers", "customer_id", id);
                db.DeleteFromTable("users", "user_id", fk_user_id);
                db.DeleteFromTable("teams_and_customers", "fk_customer_id", id);
                Session.Abandon();
                Response.Redirect("Tilmeld.aspx");
            }
        }

        /*HOLDOPLYSNINGER*/
        cmd.CommandText = "SELECT customer_id FROM customers WHERE fk_user_id = @user_id";
        cmd.Connection = conn;
        conn.Open();
        object customer_id = cmd.ExecuteScalar();
        conn.Close();
        cmd.Parameters.AddWithValue("@customer_id", customer_id);
        cmd.CommandText = "SELECT * FROM teams_and_customers INNER JOIN teams ON fk_team_id = team_id INNER JOIN customers ON fk_customer_id = customer_id INNER JOIN styles ON fk_style_id = style_id INNER JOIN instructors ON fk_instructor_id = instructor_id WHERE fk_customer_id = @customer_id";
        Repeater_Teams.DataSource = db.SelectTable(cmd);
        Repeater_Teams.DataBind();

        /*SLET HOLDOPLYSNINGER/AFMELD HOLD*/

        if (Request.QueryString["afmeld"] != null)
        {
            if (Request.QueryString["id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                db.DeleteFromTable("teams_and_customers", "teams_and_customers_id", id);
                Response.Redirect("Kundeprofil.aspx");
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
                Response.Redirect("Kundeprofil.aspx");
            }
        }
    }
}