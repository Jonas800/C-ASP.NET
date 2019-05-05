using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class AdminBrugere : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            Bruger admin = (Bruger)Session["bruger"];
            if (admin.Rolle_Navn == rolle.Administrator)
            {
                //Opretter tomt brugerobjekt
                Bruger bruger = new Bruger();

                switch (Request.QueryString["handling"])
                {
                    case "ret":
                        //Henter brugerdata til objektet og forudfylder textboxerne
                        bruger.HentBruger(Convert.ToInt32(Request.QueryString["id"]));

                        if (!IsPostBack)
                        {
                            TextBox_Navn.Text = bruger.Navn;
                            TextBox_Email.Text = bruger.Email;
                            RadioButtonList_Roller.SelectedValue = bruger.Rolle_Navn.ToString();
                        }
                        RequiredFieldValidator_Kodeord.Enabled = false;
                        break;
                    case "slet":
                        //Sletter brugeren
                        bruger.SletObjekt(Convert.ToInt32(Request.QueryString["id"]));
                        Response.Redirect("AdminBrugere.aspx");
                        break;
                    default:
                        break;
                }
                Repeater_Roller.DataSource = db.SelectAllFrom("roller");
                Repeater_Roller.DataBind();

                if (!IsPostBack)
                {
                    //Ligger enumerne rolle ind i en radiobuttonlist
                    RadioButtonList_Roller.DataSource = Enum.GetNames(typeof(rolle));
                    RadioButtonList_Roller.DataBind();
                }
            }
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        //Opretter tomt bruger objekt
        Bruger bruger = new Bruger();

        switch (Request.QueryString["handling"])
        {
            case "ret":
                //Ligger sendt data over i objektet
                bruger.Navn = TextBox_Navn.Text;
                bruger.Kodeord = TextBox_Kodeord.Text;
                bruger.Email = TextBox_Email.Text;
                bruger.Rolle_Navn = (rolle)Enum.Parse(typeof(rolle), RadioButtonList_Roller.SelectedValue);
                //Opdaterer databasen med objektets data
                bruger.UpdateObjekt(Convert.ToInt32(Request.QueryString["id"]));
                Response.Redirect("AdminBrugere.aspx");
                break;
            default:
                //Ligger sendt data over i objektet
                bruger.Navn = TextBox_Navn.Text;
                bruger.Kodeord = TextBox_Kodeord.Text;
                bruger.Email = TextBox_Email.Text;
                bruger.Nyhedsbrev = false;
                bruger.Rolle_Navn = (rolle)Enum.Parse(typeof(rolle), RadioButtonList_Roller.SelectedValue);
                //Gemmer objektet i databasen
                bruger.GemProfilMedRolle();
                Response.Redirect("AdminBrugere.aspx");

                break;
        }
    }
    protected void Repeater_Roller_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Nested repeater der tilknytter brugerer til roller i repeater i pageload
        // det er kun Item og AlternatingItem der skal håndteres
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            Repeater nested = e.Item.FindControl("Repeater_Brugere") as Repeater;

            SqlCommand cmd = new SqlCommand("SELECT * FROM roller INNER JOIN brugere ON fk_rolle_id = rolle_id WHERE rolle_id = @id ORDER BY rolle_id DESC", conn);
            cmd.Parameters.AddWithValue("@id", row["rolle_id"]);
            conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            conn.Close();
        }
    }
}