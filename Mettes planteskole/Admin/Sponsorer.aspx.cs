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

public partial class Admin_Sponsorer : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //Sessionbeskyttelse
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;

            if (bruger.Er_Admin)
            {
                if (!IsPostBack)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    //Henter alle kategorier og binder dem til en repeater
                    cmd.CommandText = "SELECT * FROM sponsorer";
                    conn.Open();
                    Repeater_Sponsor.DataSource = cmd.ExecuteReader();
                    Repeater_Sponsor.DataBind();
                    conn.Close();


                    //Forudfylder formularen ved tryk på ret og sletter kolonnen i databasen ved slet, via et id fra url'en
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                    switch (Request.QueryString["action"])
                    {
                        case "ret":
                            cmd.CommandText = "SELECT * FROM sponsorer WHERE sponsor_id = @id";

                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                TextBox_Url.Text = reader["sponsor_url"].ToString();
                                Image_GammeltBillede.ImageUrl = "../billeder/sponsor/" + reader["sponsor_billede"].ToString();
                                HiddenField_Billede.Value = reader["sponsor_billede"].ToString();
                            }
                            conn.Close();
                            break;

                        case "slet":
                            cmd.CommandText = "SELECT * FROM sponsorer WHERE sponsor_id = @id";

                            conn.Open();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                HiddenField_Billede.Value = reader["sponsor_billede"].ToString();
                            }
                            conn.Close();

                            cmd.CommandText = "DELETE FROM sponsorer WHERE sponsor_id = @id";
                            DeletePictures(HiddenField_Billede.Value);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            Response.Redirect("Sponsorer.aspx");
                            break;
                    }
                }
            }
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@url", TextBox_Url.Text);

        string NewFileName = "";

        switch (Request.QueryString["action"])
        {
            case "ret":
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                if (FileUpload_Billede.HasFile)
                {
                    DeletePictures(HiddenField_Billede.Value);

                    NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Billede.FileName);
                    FileUpload_Billede.SaveAs(Server.MapPath("../billeder/backup/") + NewFileName);

                    Resizing(NewFileName);

                    cmd.CommandText = "UPDATE sponsorer SET sponsor_url = @url, sponsor_billede = @billede WHERE sponsor_id = @id";

                    cmd.Parameters.AddWithValue("@billede", NewFileName);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("Sponsorer.aspx");

                }
                else
                {
                    cmd.CommandText = "UPDATE sponsorer SET sponsor_url = @url WHERE sponsor_id = @id";
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("Sponsorer.aspx");

                }
                break;
            default:
                if (FileUpload_Billede.HasFile)
                {
                    NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Billede.FileName);
                    FileUpload_Billede.SaveAs(Server.MapPath("../billeder/backup/") + NewFileName);

                    Resizing(NewFileName);

                    cmd.Parameters.AddWithValue("@billede", NewFileName);

                    cmd.CommandText = "INSERT INTO sponsorer (sponsor_url, sponsor_billede) VALUES (@url, @billede)";
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Response.Redirect("Sponsorer.aspx");

                }
                else
                {
                    Label_Error.Text = "Skal have fil når der oprettes";
                }
                break;
        }


    }
    public static void DeletePictures(object file_name)
    {
        if (file_name != DBNull.Value)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("../billeder/sponsor/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("../billeder/sponsor/") + file_name.ToString());
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
        settings.Width = 140;
        settings.Scale = ScaleMode.Both;
        settings.Mode = FitMode.Crop;


        ImageBuilder.Current.Build("~/billeder/backup/" + NewFileName, "~/billeder/sponsor/" + NewFileName, settings);
    }
}