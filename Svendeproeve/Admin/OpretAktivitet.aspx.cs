using ImageResizer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_OpretAktivitet : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;
            if (bruger.Rolle == 1)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        int id = 0;
                        if (int.TryParse(Request.QueryString["id"], out id))
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = conn;
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.CommandText = "SELECT * FROM aktiviteter WHERE aktivitet_id = @id";

                            //Forudfylder formularfelterne med det produkt vi vil rette
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                TextBox_Beskrivelse.Text = reader["aktivitet_tekst"].ToString();
                                TextBox_Navn.Text = reader["aktivitet_navn"].ToString();
                                Image_Billede.ImageUrl = "../billeder/produkter/" + reader["aktivitet_billede"].ToString();
                                HiddenField_Billede.Value = reader["aktivitet_billede"].ToString();
                            }
                        }
                        else
                        {
                            Response.Redirect("Aktiviteter.aspx");
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    public static void Resizing(string NewFileName)
    {
        //skalering
        ResizeSettings settings = new ResizeSettings();
        settings.Width = 150;
        settings.Scale = ScaleMode.Both;
        settings.Mode = FitMode.Crop;

        ImageBuilder.Current.Build("~/billeder/backup/" + NewFileName, "~/billeder/produkter/" + NewFileName, settings);

        settings = new ResizeSettings();
        settings.Width = 100;
        settings.Scale = ScaleMode.Both;
        settings.Mode = FitMode.Crop;

        ImageBuilder.Current.Build("~/billeder/backup/" + NewFileName, "~/billeder/thumbs/" + NewFileName, settings);
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        string NewFileName = "";
        int indsatte_rækker = 0;

        if (Request.QueryString["id"] != null)
        {
            //ret
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            if (FileUpload_Billede.HasFile)
            {
                NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Billede.FileName);

                string billede_sti = Image_Billede.ImageUrl;
                billede_sti = billede_sti.Replace("../billeder/produkter/", "");
                DeletePictures(billede_sti);

                cmd.Parameters.AddWithValue("@sti", NewFileName);
                cmd.Parameters.AddWithValue("@navn", TextBox_Navn.Text);
                cmd.Parameters.AddWithValue("@tekst", TextBox_Beskrivelse.Text);

                cmd.CommandText = "UPDATE aktiviteter SET aktivitet_billede = @sti, aktivitet_navn = @navn, aktivitet_tekst = @tekst WHERE aktivitet_id = @id";

                conn.Open();
                indsatte_rækker = cmd.ExecuteNonQuery();
                conn.Close();

                if (indsatte_rækker > 0)
                {
                    FileUpload_Billede.SaveAs(Server.MapPath("../billeder/backup/") + NewFileName);
                    Resizing(NewFileName);
                    Session["billede_besked"] = "Rettelse lykkedes.";
                }
                else
                {
                    Session["besked"] = "Rettelse fejlede.";
                }
                Response.Redirect("Aktiviteter.aspx");
            }
            else
            {
                cmd.Parameters.AddWithValue("@navn", TextBox_Navn.Text);
                cmd.Parameters.AddWithValue("@tekst", TextBox_Beskrivelse.Text);

                cmd.CommandText = "UPDATE aktiviteter SET aktivitet_navn = @navn, aktivitet_tekst = @tekst WHERE aktivitet_id = @id";

                conn.Open();
                indsatte_rækker = cmd.ExecuteNonQuery();
                conn.Close();

                if (indsatte_rækker > 0)
                {
                    Session["billede_besked"] = "Rettelse lykkedes.";
                }
                else
                {
                    Session["besked"] = "Rettelse fejlede.";
                }
                Response.Redirect("Aktiviteter.aspx");
            }
        }
        else
        {
            //opret
            if (FileUpload_Billede.HasFile)
            {
                NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Billede.FileName);

                cmd.Parameters.AddWithValue("@sti", NewFileName);
                cmd.Parameters.AddWithValue("@navn", TextBox_Navn.Text);
                cmd.Parameters.AddWithValue("@tekst", TextBox_Beskrivelse.Text);

                cmd.CommandText = "INSERT INTO aktiviteter (aktivitet_navn, aktivitet_tekst, aktivitet_billede) VALUES (@navn, @tekst, @sti)";

                conn.Open();
                indsatte_rækker = cmd.ExecuteNonQuery();
                conn.Close();

                if (indsatte_rækker > 0)
                {
                    FileUpload_Billede.SaveAs(Server.MapPath("../billeder/backup/") + NewFileName);
                    Resizing(NewFileName);
                    Session["besked"] = "Oprettelse lykkedes.";
                    Response.Redirect("Aktiviteter.aspx");

                }
                else
                {
                    Session["besked"] = "Oprettelse fejlede.";
                }
            }
            else
            {
                Label_FileUpload_Besked.Text = "Skal have et billede.";
            }

        }
    }

    public static void DeletePictures(object file_name)
    {
        if (file_name != DBNull.Value)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("../billeder/produkter/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("../billeder/produkter/") + file_name.ToString());
            }
            if (File.Exists(HttpContext.Current.Server.MapPath("../billeder/backup/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("../billeder/backup/") + file_name.ToString());
            }
            if (File.Exists(HttpContext.Current.Server.MapPath("../billeder/thumbs/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("../billeder/thumbs/") + file_name.ToString());
            }
        }
    }
}