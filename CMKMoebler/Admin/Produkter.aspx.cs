using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Produkter : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null) { 
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        //Henter alt fra produkter, inner joiner med jordtyper, kategorier og dyrkningstider, og binder det til en repeater
        SelectProdukter(cmd);

        if (Session["besked"] != null)
        {
            Label_Besked.Text = Session["besked"].ToString();
            Session.Remove("besked");
        }

        if (Request.QueryString["id"] != null)
        {
            int slettede_rækker = 0;

            cmd.CommandText = "SELECT * FROM produkter INNER JOIN billeder ON fk_produkt_id = produkt_id WHERE produkt_id = @id";
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DeletePictures(reader["billede_sti"]);

            }
            conn.Close();

            cmd.CommandText = "DELETE FROM billeder WHERE fk_produkt_id = @id; ";
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            cmd.CommandText = "DELETE FROM produkter WHERE produkt_id = @id";
            conn.Open();
            slettede_rækker = cmd.ExecuteNonQuery();
            conn.Close();
            if (slettede_rækker > 0)
            {
                Session["besked"] = "Sletning lykkedes.";
                Response.Redirect("Produkter.aspx");
            }
            else
            {
                Label_Besked.Text = "Sletning fejlede. Prøv igen eller kontakt webmaster.";
            }
        }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void SelectProdukter(SqlCommand cmd)
    {
        cmd.CommandText = "SELECT *, (SELECT COUNT (fk_produkt_id) FROM billeder where fk_produkt_id = produkt_id) as total FROM produkter INNER JOIN serier ON fk_serie_id = serie_id INNER JOIN designere ON fk_designer_id = designer_id ORDER BY serie_navn, produkt_navn ASC";
        conn.Open();
        Repeater_Produkter.DataSource = cmd.ExecuteReader();
        Repeater_Produkter.DataBind();
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