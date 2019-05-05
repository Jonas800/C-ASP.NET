using ImageResizer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Billeder : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {//Sessionbeskyttelse. Samtidig sørger vi for at man ikke kommer ind på siden uden en produkt id
        if (Session["bruger"] != null && Request.QueryString["id"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;

            if (bruger.Er_Admin)
            {
                if (!IsPostBack)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM produkter WHERE produkt_id = @id";
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                    conn.Open();
                    Repeater_Produkter.DataSource = cmd.ExecuteReader();
                    Repeater_Produkter.DataBind();
                    conn.Close();

                    //Forudfyldning ved klik på ret og slet billede på både database og server ved klik på slet
                    switch (Request.QueryString["action"])
                    {
                        case "ret":
                            cmd.CommandText = "SELECT * FROM billeder WHERE billede_id = @billede_id";
                            cmd.Parameters.AddWithValue("@billede_id", Request.QueryString["billede_id"]);
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                Image_Billede.ImageUrl = "../billeder/produkter/" + reader["billede_sti"].ToString();
                                HiddenField_Billede.Value = reader["billede_sti"].ToString();
                            }
                            conn.Close();
                            break;

                        case "slet":
                            //Finder billede stien så vi kan slette billedet fra serveren
                            string billede_sti = "";
                            cmd.CommandText = "SELECT * FROM billeder WHERE billede_id = @billede_id";
                            cmd.Parameters.AddWithValue("@billede_id", Request.QueryString["billede_id"]);
                            conn.Open();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                billede_sti = reader["billede_sti"].ToString();
                            }
                            conn.Close();
                            DeletePictures(billede_sti);

                            //Sletter billedet i databasen
                            cmd.CommandText = "DELETE FROM billeder WHERE billede_id = @billede_id";
                            conn.Open();
                            cmd.ExecuteReader();
                            conn.Close();

                            Helpers.Return();
                            break;
                    }
                }
            }
        }
    }
    protected void Repeater_Produkter_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // det er kun Item og AlternatingItem der skal håndteres
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DbDataRecord row = e.Item.DataItem as DbDataRecord;
            Repeater nested = e.Item.FindControl("Repeater_Billeder") as Repeater;
            // vi har brug for en ny connection, da den udenomliggende connecion stadig er åben... 
            SqlConnection nested_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connectionstring"].ToString());

            SqlCommand cmd = new SqlCommand("SELECT * FROM billeder WHERE fk_produkt_id = @id", nested_conn);
            cmd.Parameters.AddWithValue("@id", row["produkt_id"]);
            nested_conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            nested_conn.Close();

        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@fk_produkt_id", Request.QueryString["id"]);

        string NewFileName = "";

        switch (Request.QueryString["action"])
        {
            case "ret":
                cmd.Parameters.AddWithValue("@billede_id", Request.QueryString["billede_id"]);


                if (FileUpload_Billede.HasFile)
                {
                    HttpFileCollection fileCollection = Request.Files;

                    if (fileCollection.Count != 1)
                    {
                        Label_Error.Text = "Du kan kun rette ét billede af gangen";
                    }
                    else
                    {
                        DeletePictures(Image_Billede.ImageUrl);

                        NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Billede.FileName);
                        FileUpload_Billede.SaveAs(Server.MapPath("../billeder/backup/") + NewFileName);

                        Resizing(NewFileName);

                        cmd.CommandText = "UPDATE billeder SET billde_sti = @sti WHERE billede_id = @billede_id";

                        cmd.Parameters.AddWithValue("@sti", NewFileName);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        Helpers.Return();
                    }
                }
                else
                {
                    Label_Error.Text = "Skal have et billede";
                }
                break;
            default:

                if (FileUpload_Billede.HasFile)
                {
                    cmd.Parameters.Add("@sti", SqlDbType.VarChar);

                    foreach (HttpPostedFile billede in FileUpload_Billede.PostedFiles)
                    {
                        NewFileName = Guid.NewGuid() + Path.GetExtension(billede.FileName);
                        billede.SaveAs(Server.MapPath("../billeder/backup/") + NewFileName);

                        Resizing(NewFileName);

                        cmd.Parameters["@sti"].Value = NewFileName;

                        cmd.CommandText = "INSERT INTO billeder (billede_sti, fk_produkt_id) VALUES (@sti, @fk_produkt_id)";
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    Response.Redirect(Request.RawUrl);

                }
                else
                {
                    Label_Error.Text = "Skal have mindst ét billede";
                }
                break;
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
        }
    }
    public static void Resizing(string NewFileName)
    {

        //skalering

        //thumbnail filmliste
        ResizeSettings settings = new ResizeSettings();
        settings.Width = 120;
        settings.Height = 90;
        settings.Scale = ScaleMode.Both;
        settings.Mode = FitMode.Crop;


        ImageBuilder.Current.Build("~/billeder/backup/" + NewFileName, "~/billeder/produkter/" + NewFileName, settings);
    }
}