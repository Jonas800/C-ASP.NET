using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Aktiviteter : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;
            if (bruger.Rolle == 1)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                SelectAktiviteter(cmd);

                if (Request.QueryString["id"] != null)
                {
                    int slettede_rækker = 0;

                    cmd.CommandText = "SELECT aktivitet_billede FROM aktiviteter";
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        DeletePictures(reader["aktivitet_billede"]);
                    }
                    conn.Close();

                    cmd.CommandText = "DELETE FROM aktiviteter WHERE aktivitet_id = @id";
                    conn.Open();
                    slettede_rækker = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (slettede_rækker > 0)
                    {
                        Session["besked"] = "Sletning lykkedes.";
                        Response.Redirect("Aktiviteter.aspx");
                    }
                    else
                    {
                        Session["besked"] = "Sletning fejlede. Prøv igen eller kontakt webmaster.";
                        Response.Redirect("Aktiviteter.aspx");

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
    private void SelectAktiviteter(SqlCommand cmd)
    {
        cmd.CommandText = "SELECT * FROM aktiviteter";
        conn.Open();
        Repeater_Aktiviteter.DataSource = cmd.ExecuteReader();
        Repeater_Aktiviteter.DataBind();
        conn.Close();
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