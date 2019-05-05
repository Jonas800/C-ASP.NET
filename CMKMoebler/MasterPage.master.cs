using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "SELECT oplysning_kontakt FROM oplysninger";
        conn.Open();
        Repeater_Kontaktoplysninger.DataSource = cmd.ExecuteReader();
        Repeater_Kontaktoplysninger.DataBind();
        conn.Close();

        cmd.CommandText = "SELECT TOP 1 * FROM produkter INNER JOIN designere ON designer_id = fk_designer_id INNER JOIN serier ON fk_serie_id = serie_id ORDER BY newid() ASC";
        conn.Open();
        Repeater_Møbel_Aside.DataSource = cmd.ExecuteReader();
        Repeater_Møbel_Aside.DataBind();
        conn.Close();

        Brødkrumme();
    }

    private void Brødkrumme()
    {
        if (!Request.RawUrl.Contains("Møbel.aspx"))
        {
            if (Request.RawUrl != "/")
            {
                string brødkrumme = "";
                brødkrumme = Request.RawUrl;
                brødkrumme = brødkrumme.Replace(".aspx", "").Replace("/", "").Replace("Default", "Forside");
                Label_Brødkrumme.Text = brødkrumme;
                Literal_Brødkrumme_Title.Text += brødkrumme;
            }
            else
            {
                string brødkrumme = "Forside";
                Label_Brødkrumme.Text = brødkrumme;
                Literal_Brødkrumme_Title.Text += brødkrumme;
            }
        }
        else
        {
            SqlCommand cmd = new SqlCommand("SELECT produkt_navn FROM produkter WHERE produkt_id = @produkt_id", conn);
            cmd.Parameters.AddWithValue("@produkt_id", Request.QueryString["id"]);
            conn.Open();
            string brødkrumme = cmd.ExecuteScalar().ToString();
            conn.Close();
            Label_Brødkrumme.Text = brødkrumme;
            Literal_Brødkrumme_Title.Text += brødkrumme;
        }
    }
    protected void Repeater_Møbel_Aside_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // det er kun Item og AlternatingItem der skal håndteres
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DbDataRecord row = e.Item.DataItem as DbDataRecord;
            Repeater nested = e.Item.FindControl("Repeater_nested") as Repeater;
            // vi har brug for en ny connection, da den udenomliggende connecion stadig er åben... 
            SqlConnection nested_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connectionstring"].ToString());

            SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM billeder WHERE fk_produkt_id = @produkt_id ORDER BY billede_prioritet DESC", nested_conn);
            cmd.Parameters.AddWithValue("@produkt_id", row["produkt_id"]);
            nested_conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            nested_conn.Close();

        }
    }
    /// <summary>
    /// Highlighter (i NavBaren) siden man er inde på
    /// </summary>
    /// <param name="site"></param>
    /// <returns></returns>
    public string NavHighlight(string site)
    {
        string url = Request.RawUrl;

        if (url.Contains(site))
        {
            return "NavHighlight";
        }

        return "";
    }
}
