using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProduktInfo : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            //Til undermenu
            cmd.CommandText = "SELECT * FROM kategorier";
            conn.Open();
            Repeater_UnderMenu.DataSource = cmd.ExecuteReader();
            Repeater_UnderMenu.DataBind();
            conn.Close();

            cmd.CommandText = "SELECT * FROM produkter INNER JOIN jordtyper ON fk_jordtype_id = jordtype_id INNER JOIN dyrkningstider ON fk_dyrkningstid_id = dyrkningstid_id WHERE produkt_er_aktiv = @er_aktiv AND produkt_id = @id";
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            cmd.Parameters.AddWithValue("@er_aktiv", true);
            conn.Open();
            Repeater_ProduktInfo.DataSource = cmd.ExecuteReader();
            Repeater_ProduktInfo.DataBind();
            conn.Close();
        }
    }
    protected void Repeater_ProduktInfo_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // det er kun Item og AlternatingItem der skal håndteres
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DbDataRecord row = e.Item.DataItem as DbDataRecord;
            Repeater nested = e.Item.FindControl("Repeater_Billeder") as Repeater;
            // vi har brug for en ny connection, da den udenomliggende connecion stadig er åben... 
            SqlConnection nested_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            SqlCommand cmd = new SqlCommand("SELECT * FROM billeder WHERE fk_produkt_id = @produkt_id", nested_conn);
            cmd.Parameters.AddWithValue("@produkt_id", row["produkt_id"]);
            nested_conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            nested_conn.Close();

        }
    }
    protected void Repeater_ProduktInfo_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "køb")
        {
            int id = 0;
            int antal = 0;
            decimal pris = 0;

            int.TryParse(Request.QueryString["id"], out id);
            string navn = (e.Item.FindControl("Literal_Navn") as Literal).Text;
            int.TryParse((e.Item.FindControl("TextBox_Antal") as TextBox).Text, out antal);
            decimal.TryParse((e.Item.FindControl("Literal_Pris") as Literal).Text, out pris);

            Kurv.PutFlereVareIKurv(id, navn, pris, antal);

            Response.Redirect(Request.RawUrl);
        }
    }
}