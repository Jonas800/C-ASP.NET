using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null)
        {
            Kurv.FjernEtProduktFraKurven(Convert.ToInt32(Request.QueryString["produkt"]));
        }

        if (!IsPostBack)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM produkter", conn);
            conn.Open();
            Repeater_Produkter.DataSource = cmd.ExecuteReader();
            Repeater_Produkter.DataBind();
            conn.Close();
        }
    }
    protected void Button_Send_Click(object sender, EventArgs e)
    {
        //Kurv.PutVareIKurv(
        //    Convert.ToInt32(TextBox_ID.Text),
        //    TextBox_Navn.Text,
        //    Convert.ToDecimal(TextBox_Pris.Text),
        //    Convert.ToInt32(TextBox_Antal.Text)
        //);

        //UserControl_Kurv.VisKurv();
    }
    protected void Repeater_Produkter_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "køb")
        {
            int id = 0;
            int antal = 0;
            decimal pris = 0;

            int.TryParse((e.Item.FindControl("HiddenField_ProduktID") as HiddenField).Value, out id);
            string navn = (e.Item.FindControl("Literal_ProduktNavn") as Literal).Text;
            int.TryParse((e.Item.FindControl("TextBox_Antal") as TextBox).Text, out antal);
            decimal.TryParse((e.Item.FindControl("Literal_ProduktPris") as Literal).Text, out pris);
            int lagerstand = Convert.ToInt32((e.Item.FindControl("Literal_ProduktLagerStand") as Literal).Text);

            if (lagerstand < antal + Kurv.AntalIKurv(id))
            {
                Response.Write("IKKE NOK PÅ LAGER");
            }
            else
            {
                Kurv.PutVareIKurv(id, navn, pris, antal);
            }
            UserControl_Kurv.VisKurv();
        }
    }
}