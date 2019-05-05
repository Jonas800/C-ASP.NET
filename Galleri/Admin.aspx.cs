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
        if (!IsPostBack)
        {
            /* LOGIN/LOGOUT */
            if (Session["user_id"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            if (Request.QueryString["logout"] != null)
            {
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }


            /* OUTPUT */
            /* CATEGORY LIST FILL */
            SqlCommand cmd = new SqlCommand("SELECT * FROM categories ORDER BY category_name ASC", conn);
            conn.Open();
            Repeater_Categories.DataSource = cmd.ExecuteReader();
            Repeater_Categories.DataBind();
            conn.Close();

            /* CATEGORY DROPDOWN FILL */
            if (!IsPostBack)
            {
                SqlCommand cmd_cat = new SqlCommand("SELECT * FROM categories ORDER BY category_name ASC", conn);
                conn.Open();
                DropDownList_Categories.DataSource = cmd_cat.ExecuteReader();
                DropDownList_Categories.DataBind();

                conn.Close();
                DropDownList_Categories.Items.Insert(0, new ListItem("Choose category"));
            }

            /* IMAGE FILL */
            SqlCommand cmd_pictures = new SqlCommand("SELECT * FROM pictures INNER JOIN categories ON category_id = fk_category_id INNER JOIN users ON user_id = fk_user_id", conn);
            conn.Open();
            Repeater_Pictures.DataSource = cmd_pictures.ExecuteReader();
            Repeater_Pictures.DataBind();
            conn.Close();




            /* UPDATE CATEGORY */
            if (Request.QueryString["obj"] == "cat" && Request.QueryString["action"] == "edit")
            {
                if (!IsPostBack)
                {
                    SqlCommand cmd_update = new SqlCommand("SELECT category_name FROM categories WHERE category_id = @id", conn);
                    cmd_update.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                    conn.Open();
                    SqlDataReader reader = cmd_update.ExecuteReader();
                    if (reader.Read())
                    {
                        TextBox_Category_Edit.Text = reader["category_name"].ToString();
                    }
                    conn.Close();
                    Panel_Category_Create.Visible = false;
                    Panel_Category_Edit.Visible = true;

                }

            }

            /* DELETE CATEGORY*/
            if (Request.QueryString["obj"] == "cat" && Request.QueryString["action"] == "delete")
            {
                if (!IsPostBack)
                {
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);


                    cmd.CommandText = "SELECT count(*) FROM pictures WHERE fk_category_id = @id";
                    conn.Open();
                    object count = cmd.ExecuteScalar();
                    conn.Close();

                    if (Convert.ToInt32(count) == 0)
                    {

                        cmd.CommandText = "DELETE FROM categories WHERE category_id = @id";
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        Response.Redirect("Admin.aspx");

                    }
                    else
                    {
                        Label_Message.Text = "Pictures still found in selected category: Delete or move pictures";
                    }
                }
            }
            /* DELETE IMAGES */
            if (Request.QueryString["obj"] == "picture" && Request.QueryString["action"] == "delete")
            {
                cmd.CommandText = "SELECT picture_name FROM pictures WHERE picture_id = @id";
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                conn.Open();
                object file_name = cmd.ExecuteScalar();
                conn.Close();
                DeletePictures(file_name);
                cmd.CommandText = "DELETE FROM pictures WHERE picture_id = @id";
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("Admin.aspx");
            }
            /* UPDATE PICTURES */
            if (Request.QueryString["obj"] == "picture" && Request.QueryString["action"] == "edit")
            {
                if (!IsPostBack)
                {
                    SqlCommand cmd_cat2 = new SqlCommand("SELECT * FROM categories ORDER BY category_name ASC", conn);
                    conn.Open();
                    DropDownList_Pictures_Categories.DataSource = cmd_cat2.ExecuteReader();
                    DropDownList_Pictures_Categories.DataBind();
                    conn.Close();
                }
                Panel_Manage_Pictures.Visible = true;

                SqlCommand edit = new SqlCommand("SELECT * FROM pictures WHERE picture_id = @id", conn);
                edit.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                conn.Open();
                SqlDataReader reader = edit.ExecuteReader();
                if (reader.Read())
                {
                    DropDownList_Pictures_Categories.SelectedValue = reader["fk_category_id"].ToString();
                    TextBox_Pictures_Title.Text = reader["picture_title"].ToString();
                    TextBox_Pictures_Comment.Text = reader["picture_comment"].ToString();
                    Image_Old_Picture.ImageUrl = "~/billeder/thumbs/" + reader["picture_name"].ToString();
                    HiddenField_Old_Picture.Value = reader["picture_name"].ToString();
                }
                conn.Close();
                SetFocus(Button_Pictures);
                //continues at button_pictures
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

    /* UPLOAD AND RESIZE IMAGES */
    protected void Button_Post_Click(object sender, EventArgs e)
    {


        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.Parameters.AddWithValue("@picture_comment", TextBox_Comment.Text);
        cmd.Parameters.AddWithValue("@fk_user_id", Session["user_id"]);
        cmd.Parameters.AddWithValue("@fk_category_id", DropDownList_Categories.SelectedValue);
        cmd.Parameters.AddWithValue("@picture_title", TextBox_Title.Text);
        cmd.Parameters.AddWithValue("@picture_datetime", DateTime.Now);

        if (FileUpload_Picture.HasFile)
        {

            string NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Picture.FileName);
            FileUpload_Picture.SaveAs(Server.MapPath("~/billeder/") + NewFileName);

            Resizing(NewFileName);


            cmd.CommandText = "INSERT INTO pictures (picture_name, picture_comment, fk_user_id, fk_category_id, picture_title, picture_datetime) VALUES(@picture_name, @picture_comment, @fk_user_id, @fk_category_id, @picture_title, @picture_datetime)";

            cmd.Parameters.AddWithValue("@picture_name", NewFileName);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            Response.Redirect("Admin.aspx");
        }
    }

    private static void Resizing(string NewFileName)
    {

        //skalering

        //thumbnail cropped
        ResizeSettings settings = new ResizeSettings();
        settings.Height = 200;
        settings.Width = 200;
        settings.Mode = FitMode.Crop;
        settings.Anchor = System.Drawing.ContentAlignment.MiddleCenter;
        settings.Scale = ScaleMode.Both;

        ImageBuilder.Current.Build("~/billeder/" + NewFileName, "~/billeder/thumbs/" + NewFileName, settings);

        //til slider og galleri
        settings = new ResizeSettings();
        settings.Height = 480;
        settings.Scale = ScaleMode.Both;

        ImageBuilder.Current.Build("~/billeder/" + NewFileName, "~/billeder/resized/" + NewFileName, settings);
    }

    /* CREATE CATEGORY */
    protected void Button_Category_Create_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "INSERT INTO categories (category_name) VALUES(@category_name)";
        cmd.Parameters.AddWithValue("@category_name", TextBox_Category_Input.Text);

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("Admin.aspx");
    }
    /* UPDATE CATEGORY */
    protected void Button_Category_Edit_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "UPDATE categories SET category_name = @category_name WHERE category_id = @id";
        cmd.Parameters.AddWithValue("@category_name", TextBox_Category_Edit.Text);
        cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("Admin.aspx");
    }
    /* UPDATE IMAGES */
    protected void Button_Pictures_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        if (Request.QueryString["obj"] == "picture" && Request.QueryString["action"] == "edit")
        {
            cmd.Parameters.AddWithValue("@picture_comment", TextBox_Pictures_Comment.Text);
            cmd.Parameters.AddWithValue("@fk_category_id", DropDownList_Pictures_Categories.SelectedValue);
            cmd.Parameters.AddWithValue("@picture_title", TextBox_Pictures_Title.Text);
            cmd.Parameters.AddWithValue("@picture_datetime", DateTime.Now);

            if (TextBox_Pictures_Comment.Text != String.Empty && TextBox_Pictures_Title.Text != String.Empty)
            {
                cmd.CommandText = "UPDATE pictures SET picture_comment = @picture_comment, picture_title = @picture_title, fk_category_id = @fk_category_id, picture_datetime = @picture_datetime WHERE picture_id = @id";
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            if (FileUpload_Pictures_New.HasFile)
            {
                DeletePictures(HiddenField_Old_Picture.Value);

                string NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Pictures_New.FileName);
                FileUpload_Pictures_New.SaveAs(Server.MapPath("~/billeder/") + NewFileName);

                Resizing(NewFileName);

                cmd.CommandText = "UPDATE pictures SET picture_name = @picture_name WHERE picture_id = @id";
                cmd.Parameters.AddWithValue("@picture_name", NewFileName);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            Response.Redirect("admin.aspx");
        }


    }
}
