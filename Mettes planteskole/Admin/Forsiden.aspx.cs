using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ImageResizer;
using CKEditor;

public partial class Admin_Forsiden : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "SELECT * FROM forside";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                TextBox_Titel.Text = reader["forside_titel"].ToString();
                TextBox_Tekst.Text = reader["forside_tekst"].ToString();
                Image_Billede.ImageUrl = "~/billeder/forside/" + reader["forside_billede"].ToString();
                HiddenField_Billede.Value = reader["forside_billede"].ToString();
            }
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@tekst", TextBox_Tekst.Text);
        cmd.Parameters.AddWithValue("@titel", TextBox_Titel.Text);

        if (FileUpload_Billede.HasFile)
        {
            DeletePictures(HiddenField_Billede.Value);

            string NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Billede.FileName);
            FileUpload_Billede.SaveAs(Server.MapPath("../billeder/backup/") + NewFileName);

            Resizing(NewFileName);

            cmd.Parameters.AddWithValue("@billede", NewFileName);

            cmd.CommandText = "UPDATE forside SET forside_tekst = @tekst, forside_titel = @titel, forside_billede = @billede";
        }
        else
        {
            cmd.CommandText = "UPDATE forside SET forside_tekst = @tekst, forside_titel = @titel";
        }
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        Response.Redirect("Forsiden.aspx");
    }
    public static void DeletePictures(object file_name)
    {
        if (file_name != DBNull.Value)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("../billeder/forside/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("../billeder/forside/") + file_name.ToString());
            }
            if (File.Exists(HttpContext.Current.Server.MapPath("../billeder/backup/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("../billeder/backup/") + file_name.ToString());
            }
        }
    }
    public static void Resizing(string NewFileName)
    {
        //skalering

        //thumbnail filmliste
        ResizeSettings settings = new ResizeSettings();
        settings.Width = 390;
        settings.Scale = ScaleMode.Both;
        settings.Mode = FitMode.Crop;

        ImageBuilder.Current.Build("~/billeder/backup/" + NewFileName, "~/billeder/forside/" + NewFileName, settings);
    }
}