using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ImageResizer;
using System.IO;

public partial class InstruktoererAdmin : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["column"] != null && Request.QueryString["direction"] != null)
        {
            string column = Request.QueryString["column"];
            string direction = Request.QueryString["direction"];
            Repeater_List.DataSource = db.SelectAllFromOrderBy("instructors", column, direction);
            Repeater_List.DataBind();
        }
        else
        {
            Repeater_List.DataSource = db.SelectAllFrom("instructors");
            Repeater_List.DataBind();
        }
        if (Request.QueryString["ret"] != null)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt16(Request.QueryString["id"]);
                DataRow reader = db.SelectSingleRowFrom("instructors", "instructor_id", id);

                if (reader.ItemArray.Length > 0)
                {
                    TextBox_Instruktoerer_Name.Text = reader["instructor_name"].ToString();
                    TextBox_Instruktoerer_Description.Text = reader["instructor_description"].ToString();
                    Image_Old_Picture.ImageUrl = "/billeder/thumbs/" + reader["instructor_image"].ToString();
                    HiddenField_Old_Picture.Value = reader["instructor_image"].ToString();
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
            Panel_Instruktoerer_Create.Visible = true;
        }
        if (Request.QueryString["opret"] != null)
        {
            Repeater_List.Visible = false;
            Panel_Instruktoerer_Create.Visible = true;
        }

        if (Request.QueryString["slet"] != null)
        {
            if (Request.QueryString["id"] != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT instructor_image FROM instructors WHERE instructor_id = @id";
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                conn.Open();
                object file_name = cmd.ExecuteScalar();
                conn.Close();
                Helpers.DeletePictures(file_name);

                int id = Convert.ToInt32(Request.QueryString["id"]);

                DataRow reader = db.SelectSingleRowFrom("instructors", "instructor_id", id);

                if (reader.ItemArray.Length > 0)
                {
                    HiddenField_FK_user_id.Value = reader["fk_user_id"].ToString();
                }
                int fk_user_id = Convert.ToInt32(HiddenField_FK_user_id.Value);

                db.DeleteFromTable("instructors", "instructor_id", id);
                db.DeleteFromTable("users", "user_id", fk_user_id);
                Response.Redirect("InstruktoererAdmin.aspx");
            }
        }
    }

    protected void Button_Instruktoerer_Create_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.Parameters.AddWithValue("@instructor_description", TextBox_Instruktoerer_Description.Text);
        cmd.Parameters.AddWithValue("@instructor_name", TextBox_Instruktoerer_Name.Text);


        if (Request.QueryString["ret"] != null)
        {
            if (Request.QueryString["id"] != null)
            {              
                cmd.CommandText = "UPDATE instructors SET instructor_name = @instructor_name, instructor_description = @instructor_description WHERE instructor_id = @id";
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

                if (FileUpload_Instruktoerer_Image.HasFile)
                {
                    Helpers.DeletePictures(HiddenField_Old_Picture.Value);

                    string NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Instruktoerer_Image.FileName);
                    FileUpload_Instruktoerer_Image.SaveAs(Server.MapPath("~/billeder/") + NewFileName);

                    Resizing(NewFileName);

                    cmd.CommandText = "UPDATE instructors SET instructor_image = @instructor_image WHERE instructor_id = @id";
                    cmd.Parameters.AddWithValue("@instructor_image", NewFileName);

                    db.UpdateTable(cmd);
                }

                Response.Redirect("InstruktoererAdmin.aspx");
            }
        }
        else
        {
            if (FileUpload_Instruktoerer_Image.HasFile)
            {
                string NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Instruktoerer_Image.FileName);
                FileUpload_Instruktoerer_Image.SaveAs(Server.MapPath("~/billeder/") + NewFileName);

                Resizing(NewFileName);

                cmd.CommandText = "INSERT INTO users (user_email, user_password, fk_role_id) VALUES(@user_email, @user_password, @fk_role_id)";
                cmd.Parameters.AddWithValue("@user_email", TextBox_Login_Name.Text);
                cmd.Parameters.AddWithValue("@user_password", TextBox_User_Password.Text);
                cmd.Parameters.AddWithValue("@fk_role_id", 1);
                int user_id = db.InsertIntoTable(cmd);

                

                cmd.CommandText = "INSERT INTO instructors (instructor_name, instructor_description, instructor_image, fk_user_id) VALUES(@instructor_name, @instructor_description, @instructor_image, @fk_user_id)";
                cmd.Parameters.AddWithValue("@instructor_image", NewFileName);
                cmd.Parameters.AddWithValue("@fk_user_id", user_id);
                db.InsertIntoTable(cmd);

                Response.Redirect("InstruktoererAdmin.aspx");
            }
            else
            {
                Label_Instruktoerer_Image_Error.Text = "Skal have fil";
            }

        }
    }
    private static void Resizing(string NewFileName)
    {

        //skalering

        //thumbnail cropped
        ResizeSettings settings = new ResizeSettings();
        settings.Height = 50;
        settings.Width = 50;
        settings.Mode = FitMode.Crop;
        settings.Anchor = System.Drawing.ContentAlignment.MiddleCenter;
        settings.Scale = ScaleMode.Both;

        ImageBuilder.Current.Build("~/billeder/" + NewFileName, "~/billeder/thumbs/" + NewFileName, settings);

        //til detajler
        settings = new ResizeSettings();
        settings.Height = 600;
        settings.Width = 600;
        settings.Mode = FitMode.Crop;
        settings.Anchor = System.Drawing.ContentAlignment.MiddleCenter;
        settings.Scale = ScaleMode.Both;

        ImageBuilder.Current.Build("~/billeder/" + NewFileName, "~/billeder/resized/" + NewFileName, settings);
    }
}