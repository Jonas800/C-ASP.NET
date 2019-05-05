using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using ImageResizer;

public partial class Admin : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["nav"] == "products")
        {
            Panel_Products.Visible = true;
        }
        else if (Request.QueryString["nav"] == "categories")
        {
            Panel_Categories.Visible = true;
        }
        else
        {
            Panel_Users.Visible = true;
        }

        /* LOGIN/LOGOUT */
        if (Session["user_id"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        if (!IsPostBack)
        {
            /* OUTPUT */
            /* CATEGORY LIST FILL */
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM categories ORDER BY category_name ASC";
            conn.Open();
            Repeater_Categories.DataSource = cmd.ExecuteReader();
            Repeater_Categories.DataBind();
            conn.Close();

            /* CATEGORY DROPDOWN FILL */
            if (!IsPostBack)
            {
                cmd.CommandText = "SELECT * FROM categories ORDER BY category_name ASC";
                conn.Open();
                DropDownList_Product_Categories.DataSource = cmd.ExecuteReader();
                DropDownList_Product_Categories.DataBind();

                conn.Close();
                DropDownList_Product_Categories.Items.Insert(0, new ListItem("Vælg kategori"));
            }

            /* IMAGE FILL */
            cmd.CommandText = "SELECT * FROM products INNER JOIN categories ON category_id = fk_category_id INNER JOIN users ON user_id = fk_user_id";
            conn.Open();
            Repeater_Product.DataSource = cmd.ExecuteReader();
            Repeater_Product.DataBind();
            conn.Close();

            /* USER FILL */
            cmd.CommandText = "SELECT * FROM users";
            conn.Open();
            Repeater_Users.DataSource = cmd.ExecuteReader();
            Repeater_Users.DataBind();
            conn.Close();



            /* UPDATE CATEGORY */
            if (Request.QueryString["nav"] == "categories" && Request.QueryString["action"] == "edit")
            {
                if (!IsPostBack)
                {
                    cmd.CommandText = "SELECT category_name FROM categories WHERE category_id = @id";
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        TextBox_Category_Name.Text = reader["category_name"].ToString();
                    }
                    conn.Close();

                    Label_Category_Name.Text = "Ret kategori";

                }

            }

            /* DELETE CATEGORY*/
            if (Request.QueryString["nav"] == "categories" && Request.QueryString["action"] == "delete")
            {
                if (!IsPostBack)
                {
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                    cmd.CommandText = "SELECT count(*) FROM products WHERE fk_category_id = @id";
                    conn.Open();
                    object count = cmd.ExecuteScalar();
                    conn.Close();

                    if (Convert.ToInt32(count) == 0)
                    {
                        cmd.CommandText = "DELETE FROM categories WHERE category_id = @id";
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        Response.Redirect("Admin.aspx?nav=categories");
                    }
                    else
                    {
                        Label_Message.Text = "Products still found in selected category: Delete or move products";
                    }
                }
            }
            /* DELETE IMAGES */
            if (Request.QueryString["nav"] == "products" && Request.QueryString["action"] == "delete")
            {
                cmd.CommandText = "SELECT product_image FROM products WHERE product_id = @id";
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                conn.Open();
                object file_name = cmd.ExecuteScalar();
                conn.Close();
                DeletePictures(file_name);
                cmd.CommandText = "DELETE FROM products WHERE product_id = @id";
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("Admin.aspx?nav=products");
            }
            /* UPDATE PRODUCTS */
            if (Request.QueryString["nav"] == "products" && Request.QueryString["action"] == "edit")
            {
                cmd.CommandText = "SELECT * FROM products WHERE product_id = @id";
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    DropDownList_Product_Categories.SelectedValue = reader["fk_category_id"].ToString();
                    TextBox_Product_Name.Text = reader["product_name"].ToString();
                    TextBox_Product_Description.Text = reader["product_description"].ToString();
                    TextBox_Product_Price.Text = reader["product_price"].ToString();
                    Image_Old_Picture.ImageUrl = "~/billeder/thumbs/" + reader["product_image"].ToString();
                    HiddenField_Old_Picture.Value = reader["product_image"].ToString();
                }
                conn.Close();
                //continues at button_product
            }
            /* UPDATE AND DELETE USERS */
            if (Request.QueryString["nav"] == "users" && Request.QueryString["id"] != null)
            {
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                if (Request.QueryString["action"] == "edit")
                {
                    cmd.CommandText = "SELECT * FROM users WHERE user_id = @id";
                    conn.Open();
                    SqlDataReader reader_users = cmd.ExecuteReader();
                    if (reader_users.Read())
                    {
                        TextBox_User_Firstname.Text = reader_users["user_firstname"].ToString();
                        TextBox_User_Lastname.Text = reader_users["user_lastname"].ToString();
                        TextBox_User_Name.Text = reader_users["user_login"].ToString();
                    }
                    conn.Close();
                    Button_User_Create.Text = "Ret";
                    Label_User_Password.Text = "Kodeord (valgfri)";
                }
                if (Request.QueryString["action"] == "delete")
                {
                    cmd.CommandText = "DELETE FROM users WHERE user_id = @id";
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("Admin.aspx?nav=users");
                }
            }
        }
    }
    private void DeletePictures(object file_name)
    {
        if (file_name != DBNull.Value)
        {
            if (File.Exists(Server.MapPath("~/billeder/") + file_name.ToString()))
            {
                File.Delete(Server.MapPath("~/billeder/") + file_name.ToString());
            }
            if (File.Exists(Server.MapPath("~/billeder/thumbs/") + file_name.ToString()))
            {
                File.Delete(Server.MapPath("~/billeder/thumbs/") + file_name.ToString());
            }
            if (File.Exists(Server.MapPath("~/billeder/resized/") + file_name.ToString()))
            {
                File.Delete(Server.MapPath("~/billeder/resized/") + file_name.ToString());
            }
        }
    }
    private static void Resizing(string NewFileName)
    {

        //skalering

        //thumbnail cropped
        ResizeSettings settings = new ResizeSettings();
        settings.Height = 175;
        settings.Width = 175;
        settings.Mode = FitMode.Crop;
        settings.Anchor = System.Drawing.ContentAlignment.MiddleCenter;
        settings.Scale = ScaleMode.Both;

        ImageBuilder.Current.Build("~/billeder/" + NewFileName, "~/billeder/thumbs/" + NewFileName, settings);

        //til detajler
        settings = new ResizeSettings();
        settings.Height = 300;
        settings.Scale = ScaleMode.Both;

        ImageBuilder.Current.Build("~/billeder/" + NewFileName, "~/billeder/resized/" + NewFileName, settings);
    }
    protected void Button_Category_Create_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        if (Request.QueryString["nav"] == "categories" && Request.QueryString["action"] == "edit")
        {
            cmd.CommandText = "UPDATE categories SET category_name = @category_name WHERE category_id = @id";
            cmd.Parameters.AddWithValue("@category_name", TextBox_Category_Name.Text);
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Response.Redirect("Admin.aspx?nav=categories");
        }
        cmd.CommandText = "INSERT INTO categories (category_name) VALUES(@category_name)";
        cmd.Parameters.AddWithValue("@category_name", TextBox_Category_Name.Text);

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("Admin.aspx?nav=categories");
    }

    protected void Button_Product_Create_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.Parameters.AddWithValue("@product_description", TextBox_Product_Description.Text);
        cmd.Parameters.AddWithValue("@fk_user_id", Session["user_id"]);
        cmd.Parameters.AddWithValue("@fk_category_id", DropDownList_Product_Categories.SelectedValue);
        cmd.Parameters.AddWithValue("@product_name", TextBox_Product_Name.Text);
        cmd.Parameters.AddWithValue("@product_datetime", DateTime.Now);
        decimal pris = decimal.Parse(TextBox_Product_Price.Text);
        cmd.Parameters.AddWithValue("@product_price", pris);


        if (Request.QueryString["nav"] == "products")
        {
            if (Request.QueryString["action"] == "edit")
            {
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                cmd.CommandText = "UPDATE products SET product_description = @product_description, product_name = @product_name, fk_category_id = @fk_category_id, product_price = @product_price WHERE product_id = @id";
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                if (FileUpload_Product_Image.HasFile)
                {
                    DeletePictures(HiddenField_Old_Picture.Value);

                    string NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Product_Image.FileName);
                    FileUpload_Product_Image.SaveAs(Server.MapPath("~/billeder/") + NewFileName);

                    Resizing(NewFileName);

                    cmd.CommandText = "UPDATE products SET product_image = @product_image WHERE product_id = @id";
                    cmd.Parameters.AddWithValue("@product_image", NewFileName);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                Response.Redirect("Admin.aspx?nav=products");
            }
            else
            {

                if (FileUpload_Product_Image.HasFile)
                {

                    string NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Product_Image.FileName);
                    FileUpload_Product_Image.SaveAs(Server.MapPath("~/billeder/") + NewFileName);

                    Resizing(NewFileName);


                    cmd.CommandText = "INSERT INTO products (product_image, product_description, fk_user_id, fk_category_id, product_name, product_price, product_datetime) VALUES(@product_image, @product_description, @fk_user_id, @fk_category_id, @product_name, @product_price, @product_datetime)";

                    cmd.Parameters.AddWithValue("@product_image", NewFileName);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("Admin.aspx?nav=products");
                }
                else
                {
                    Label_Product_Image_Error.Text = "Skal have fil";
                }
            }
        }
    }
    protected void Button_User_Create_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.Parameters.AddWithValue("@user_firstname", TextBox_User_Firstname.Text);
        cmd.Parameters.AddWithValue("@user_lastname", TextBox_User_Lastname.Text);
        cmd.Parameters.AddWithValue("@user_login", TextBox_User_Name.Text);
        cmd.Parameters.AddWithValue("@user_password", TextBox_User_Password.Text);
        if (Request.QueryString["nav"] == "users" && Request.QueryString["action"] == "edit" && Request.QueryString["id"] != null)
        {
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            if (TextBox_User_Password.Text == String.Empty)
            {
                cmd.CommandText = "UPDATE users SET user_firstname = @user_firstname, user_lastname = @user_lastname, user_login = @user_login WHERE user_id = @id";
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("Admin.aspx?nav=users");
            }
            else
            {
                cmd.CommandText = "UPDATE users SET user_firstname = @user_firstname, user_lastname = @user_lastname, user_login = @user_login, user_password = @user_password WHERE user_id = @id";
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("Admin.aspx?nav=users");
            }
        }
        else
        {
            if (TextBox_User_Password.Text == "")
            {
                Label_User_Password_Error.Text = "Skal udfyldes";
            }
            else
            {
                cmd.CommandText = "INSERT INTO users (user_firstname, user_lastname, user_login, user_password) VALUES(@user_firstname, @user_lastname, @user_login, @user_password)";
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("Admin.aspx?nav=users");
            }
        }
    }
}