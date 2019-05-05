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

public partial class StilarterAdmin : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["column"] != null && Request.QueryString["direction"] != null)
        {
            string column = Request.QueryString["column"];
            string direction = Request.QueryString["direction"];
            Repeater_List.DataSource = db.SelectAllFromOrderBy("styles", column, direction);
            Repeater_List.DataBind();
        }
        else
        {
            Repeater_List.DataSource = db.SelectAllFrom("styles");
            Repeater_List.DataBind();
        }
        

        if (Request.QueryString["ret"] != null)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt16(Request.QueryString["id"]);
                DataRow reader = db.SelectSingleRowFrom("styles", "style_id", id);

                if (reader.ItemArray.Length > 0)
                {
                    TextBox_Styles_Name.Text = reader["style_name"].ToString();
                    TextBox_Styles_Description.Text = reader["style_description"].ToString();
                    Image_Old_Picture.ImageUrl = "/billeder/thumbs/" + reader["style_image"].ToString();
                    HiddenField_Old_Picture.Value = reader["style_image"].ToString();
                }
            }

            Repeater_List.Visible = false;
            Panel_Styles_Create.Visible = true;
        }
        if (Request.QueryString["opret"] != null)
        {
            Repeater_List.Visible = false;
            Panel_Styles_Create.Visible = true;
        }

        if (Request.QueryString["slet"] != null)
        {
            if (Request.QueryString["id"] != null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT style_image FROM styles WHERE style_id = @id";
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                conn.Open();
                object file_name = cmd.ExecuteScalar();
                conn.Close();
                Helpers.DeletePictures(file_name);
                int id = Convert.ToInt16(Request.QueryString["id"]);
                db.DeleteFromTable("styles", "style_id", id);
                Response.Redirect("StilarterAdmin.aspx");
            }
        }
    }
    protected void Button_Styles_Create_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.Parameters.AddWithValue("@style_description", TextBox_Styles_Description.Text);
        cmd.Parameters.AddWithValue("@style_name", TextBox_Styles_Name.Text);


        if (Request.QueryString["ret"] != null)
        {
            if (Request.QueryString["id"] != null)
            {
                cmd.CommandText = "UPDATE styles SET style_name = @style_name, style_description = @style_description WHERE style_id = @id";

                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                db.UpdateTable(cmd);

                if (FileUpload_Styles_Image.HasFile)
                {
                    Helpers.DeletePictures(HiddenField_Old_Picture.Value);

                    string NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Styles_Image.FileName);
                    FileUpload_Styles_Image.SaveAs(Server.MapPath("~/billeder/") + NewFileName);

                    Resizing(NewFileName);

                    cmd.CommandText = "UPDATE styles SET style_image = @style_image WHERE style_id = @id";
                    cmd.Parameters.AddWithValue("@style_image", NewFileName);

                    db.UpdateTable(cmd);
                }

                Response.Redirect("StilarterAdmin.aspx");
            }
        }
        else
        {
            if (FileUpload_Styles_Image.HasFile)
            {
                string NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Styles_Image.FileName);
                FileUpload_Styles_Image.SaveAs(Server.MapPath("~/billeder/") + NewFileName);

                Resizing(NewFileName);

                cmd.CommandText = "INSERT INTO styles (style_name, style_description, style_image) VALUES(@style_name, @style_description, @style_image)";
                cmd.Parameters.AddWithValue("@style_image", NewFileName);

                db.InsertIntoTable(cmd);
                Response.Redirect("StilarterAdmin.aspx");
            }
            else
            {
                Label_Styles_Image_Error.Text = "Skal have fil";
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