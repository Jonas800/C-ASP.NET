using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using ImageResizer;
using System.Drawing;


public partial class Admin_Billeder : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            try
            {

                int id = 0;
                if (int.TryParse(Request.QueryString["id"], out id))
                {
                    if (!IsPostBack)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                        SelectBilleder(cmd);

                        if (Request.QueryString["billede_id"] != null)
                        {
                            cmd.Parameters.AddWithValue("@billede_id", Request.QueryString["billede_id"]);

                        }

                        if (Session["besked"] != null)
                        {
                            Label_Besked.Text = Session["besked"].ToString();
                            Session.Remove("besked");
                        }
                        //Forudfyldning ved klik på ret og slet billede på både database og server ved klik på slet
                        switch (Request.QueryString["action"])
                        {
                            case "ret":
                                cmd.CommandText = "SELECT * FROM billeder WHERE billede_id = @billede_id";

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
                                cmd.CommandText = "SELECT * FROM billeder WHERE billede_id = @billede_id";

                                conn.Open();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    DeletePictures(reader["billede_sti"].ToString());
                                }
                                conn.Close();

                                //Sletter billedet i databasen
                                int slettede_billeder = 0;
                                cmd.CommandText = "DELETE FROM billeder WHERE billede_id = @billede_id";
                                conn.Open();
                                slettede_billeder = cmd.ExecuteNonQuery();
                                conn.Close();

                                if (slettede_billeder > 0)
                                {
                                    Session["besked"] = "Billedet er blevet slettet.";
                                    Helpers.Return();
                                }
                                else
                                {
                                    Session["besked"] = "Billedet blev ikke slettet.";
                                }

                                SelectBilleder(cmd);
                                break;

                            case "head":
                                cmd.CommandText = "UPDATE billeder SET billede_prioritet = 0 WHERE fk_produkt_id = @id; UPDATE billeder SET billede_prioritet = 1 WHERE billede_id = @billede_id";
                                conn.Open();
                                int ændrede_rækker = cmd.ExecuteNonQuery();
                                conn.Close();

                                if (ændrede_rækker > 0)
                                {
                                    Session["besked"] = "Prioritet blev ændret.";
                                    Helpers.Return();

                                }
                                else
                                {
                                    Session["besked"] = "Ingen billeder blev ændret.";
                                }
                                SelectBilleder(cmd);
                                Helpers.Return();
                                break;
                        }

                    }
                }
                else
                {
                    Response.Redirect("Produkter.aspx");
                }

            }
            catch (SqlException ex)
            {
                Panel_Besked.BackColor = Color.Red;
                Label_Besked.ForeColor = Color.Yellow;
                Panel_Besked.Visible = true;
                Label_Besked.Text = "SQL/Databasefejl: " + ex.Message;
            }
            catch (Exception ex)
            {
                Panel_Besked.BackColor = Color.Red;
                Label_Besked.ForeColor = Color.Yellow;
                Panel_Besked.Visible = true;
                Label_Besked.Text = "Kodefejl: " + ex.Message;
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void SelectBilleder(SqlCommand cmd)
    {
        cmd.CommandText = "SELECT * FROM produkter WHERE produkt_id = @id";

        conn.Open();
        Repeater_Produkter.DataSource = cmd.ExecuteReader();
        Repeater_Produkter.DataBind();
        conn.Close();
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

            SqlCommand cmd = new SqlCommand("SELECT * FROM billeder WHERE fk_produkt_id = @id ORDER BY billede_prioritet DESC", nested_conn);
            cmd.Parameters.AddWithValue("@id", row["produkt_id"]);
            nested_conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            nested_conn.Close();

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
    public static void Resizing(string NewFileName)
    {
        //skalering
        ResizeSettings settings = new ResizeSettings();
        settings.Width = 300;
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

        cmd.Parameters.AddWithValue("@fk_produkt_id", Request.QueryString["id"]);
        cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);


        string NewFileName = "";
        int indsatte_rækker = 0;

        switch (Request.QueryString["action"])
        {
            case "ret":
                cmd.Parameters.AddWithValue("@billede_id", Request.QueryString["billede_id"]);


                if (FileUpload_Billede.HasFile)
                {
                    HttpFileCollection fileCollection = Request.Files;

                    if (fileCollection.Count != 1)
                    {
                        Label_Besked.Text = "Du kan kun rette ét billede af gangen";
                    }
                    else
                    {
                        string billede_sti = Image_Billede.ImageUrl;
                        billede_sti = billede_sti.Replace("../billeder/produkter/", "");
                        DeletePictures(billede_sti);

                        NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Billede.FileName);
                        FileUpload_Billede.SaveAs(Server.MapPath("../billeder/backup/") + NewFileName);

                        Resizing(NewFileName);

                        cmd.CommandText = "UPDATE billeder SET billede_sti = @sti WHERE billede_id = @billede_id";

                        cmd.Parameters.AddWithValue("@sti", NewFileName);
                        conn.Open();
                        indsatte_rækker = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (indsatte_rækker > 0)
                        {
                            Session["billede_besked"] = "Billede blev ændret.";
                            string url = Request.RawUrl;
                            url = url.Replace("&action=ret&billede_id=" + Request.QueryString["billede_id"], "");
                            Response.Redirect(url);
                        }
                        else
                        {
                            Label_Besked.Text = "Ingen billeder blev ændret.";
                        }
                        SelectBilleder(cmd);

                        Image_Billede.ImageUrl = null;
                    }
                }
                else
                {
                    Label_Besked.Text = "Skal have et billede";
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
                        indsatte_rækker += cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    if (indsatte_rækker == 1)
                    {
                        Label_Besked.Text = "Billede blev uploadet.";
                    }
                    else if (indsatte_rækker > 1)
                    {
                        Label_Besked.Text = "Billeder blev uploadet.";
                    }
                    else
                    {
                        Label_Besked.Text = "Ingen billeder blev uploadet.";
                    }
                    SelectBilleder(cmd);
                }
                else
                {
                    Label_Besked.Text = "Skal have mindst ét billede";
                }
                break;
        }
    }
    //protected void DropDownList_Order_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Response.Write("HALLO");
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.Connection = conn;

    //    Response.Write(((DropDownList)sender).SelectedItem.Text + ((DropDownList)sender).SelectedValue);

    //    cmd.CommandText = "UPDATE billeder SET billede_prioritet = billede_prioritet + 1 WHERE billede_prioritet >= @prioritet; UPDATE billeder SET billede_prioritet = @prioritet WHERE billede_id = @billede_id";

    //    cmd.Parameters.AddWithValue("@prioritet", ((DropDownList)sender).SelectedItem.Text);
    //    cmd.Parameters.AddWithValue("@billede_id", ((DropDownList)sender).SelectedValue);

    //    conn.Open();
    //    cmd.ExecuteNonQuery();
    //    conn.Close();
    //}
    //protected void Repeater_Billeder_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    // det er kun Item og AlternatingItem der skal håndteres
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        DbDataRecord row = e.Item.DataItem as DbDataRecord;
    //        Repeater nested = e.Item.FindControl("Repeater_Billeder") as Repeater;
    //        // vi har brug for en ny connection, da den udenomliggende connecion stadig er åben... 
    //        SqlConnection nested_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connectionstring"].ToString());


    //        SqlCommand cmd = new SqlCommand();
    //        cmd.Connection = nested_conn;
    //        cmd.CommandText = "SELECT COUNT (*) AS total FROM billeder WHERE fk_produkt_id = @id";
    //        cmd.Parameters.AddWithValue("@id", row["fk_produkt_id"]);
    //        cmd.Parameters.AddWithValue("@billede_id", row["billede_id"]);
    //        DropDownList ddlOrder = e.Item.FindControl("DropDownList_Order") as DropDownList;

    //        nested_conn.Open();
    //        int total = Convert.ToInt32(cmd.ExecuteScalar());
    //        nested_conn.Close();

    //        for (int i = 1; i <= total; i++)
    //        {
    //            ddlOrder.Items.Add(new ListItem(i.ToString(), row["billede_id"].ToString()));
    //        }
    //        cmd.CommandText = "SELECT billede_prioritet FROM billeder WHERE billede_id = @billede_id";

    //        nested_conn.Open();
    //        int current = Convert.ToInt32(cmd.ExecuteScalar()) - 1;
    //        nested_conn.Close();

    //        ddlOrder.SelectedValue = current.ToString();


    //    }
    //}
}