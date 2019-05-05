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

public partial class DefaultAdmin : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        DataRow reader = db.SelectSingleRow("forside");

        if (!IsPostBack)
        {
            if (reader.ItemArray.Length > 0)
            {
                TextBox_Forside_Text.Text = reader["forside_text"].ToString();
                Image_Forside_Current_Image.ImageUrl = "/billeder/" + reader["forside_image"].ToString();
                HiddenField_Forside_Current_Image.Value = reader["forside_image"].ToString();
            }
        }
    }
    protected void Button_Forside_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.Parameters.AddWithValue("@forside_text", TextBox_Forside_Text.Text);

        cmd.CommandText = "SELECT * FROM forside";
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            conn.Close();
            cmd.CommandText = "UPDATE forside SET forside_text = @forside_text";
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            if (FileUpload_Forside_Image.HasFile)
            {
                Helpers.DeletePictures(HiddenField_Forside_Current_Image.Value);

                string NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Forside_Image.FileName);
                FileUpload_Forside_Image.SaveAs(Server.MapPath("~/billeder/") + NewFileName);

                Resizing(NewFileName);

                cmd.CommandText = "UPDATE forside SET forside_image = @forside_image";
                cmd.Parameters.AddWithValue("@forside_image", NewFileName);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            Response.Redirect("DefaultAdmin.aspx");
        }
        else
        {
            conn.Close();
            if (FileUpload_Forside_Image.HasFile)
            {
                string NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Forside_Image.FileName);
                FileUpload_Forside_Image.SaveAs(Server.MapPath("~/billeder/") + NewFileName);

                Resizing(NewFileName);

                cmd.CommandText = "INSERT INTO forside (forside_image, forside_text) VALUES(@forside_image, @forside_text)";
                cmd.Parameters.AddWithValue("@forside_image", NewFileName);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("DefaultAdmin.aspx");
            }
        }
    }
    private static void Resizing(string NewFileName)
    {

        //skalering

        ResizeSettings settings = new ResizeSettings();

        //til forsidebanner
        settings = new ResizeSettings();
        settings.Height = 600;
        settings.Width = 1920;
        settings.Mode = FitMode.Crop;
        settings.Anchor = System.Drawing.ContentAlignment.MiddleCenter;
        settings.Scale = ScaleMode.Both;

        ImageBuilder.Current.Build("~/billeder/" + NewFileName, "~/billeder/resized/" + NewFileName, settings);
    }
}