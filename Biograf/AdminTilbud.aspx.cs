using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class AdminTilbud : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Repeater_Tilbud.DataSource = db.SelectAllFrom("tilbud");
        Repeater_Tilbud.DataBind();

        //Populerer radiobuttonlist og sikrer os via !ispostback at de ikke resettes ved postback
        if (!IsPostBack)
        {
            RadioButtonList_Roller.DataSource = Enum.GetNames(typeof(rolle));
            RadioButtonList_Roller.DataBind();
        }

        //Opretter et tilbud objekt
        Tilbud tilbud = new Tilbud();
        switch (Request.QueryString["handling"])
        {
            case "ret":
                //Forudfylder siden med data fra databasen via objektet, der findes ud fra querystring
                if (!IsPostBack)
                {
                    tilbud.HentTilbud(Convert.ToInt32(Request.QueryString["id"]));
                    RadioButtonList_Roller.SelectedValue = tilbud.Rolle.ToString();
                    HiddenField_Billede.Value = tilbud.Billede;
                    RequiredFieldValidator_Tilbud.Enabled = false;
                }
                break;
            case "slet":
                //Sletter et tilbud ud fra objektet der hentes via en id fra addressebaren samt sletter billederne fra serveren
                tilbud.HentTilbud(Convert.ToInt32(Request.QueryString["id"]));
                Helpers.DeletePictures(tilbud.Billede);
                tilbud.SletTilbud(Convert.ToInt32(Request.QueryString["id"]));
                Helpers.Return();
                break;
        }

    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        //Opretter objekt og sætter dets egenskaber til det valgte fra siden
        Tilbud tilbud = new Tilbud();
        tilbud.Rolle = (rolle)Enum.Parse(typeof(rolle), RadioButtonList_Roller.SelectedValue);
        //Giver filnavnet en unik identifier
        string NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Tilbud.FileName);
        tilbud.Billede = NewFileName;


        switch (Request.QueryString["handling"])
        {
            case "ret":
                //Hvis fileupload controllen har en fået sendt en fil med sig
                if (FileUpload_Tilbud.HasFile)
                {
                    //så slettes det gamle billede på serveren via en værdi i en hiddenfield control. Derefter gemmes et nyt billede
                    Helpers.DeletePictures(HiddenField_Billede.Value);
                    FileUpload_Tilbud.SaveAs(Server.MapPath("~/billeder/") + NewFileName);
                    Helpers.ResizingTilbud(NewFileName);
                    //og tabellen opdateres
                    tilbud.UpdateTilbud(Convert.ToInt32(Request.QueryString["id"]));
                }
                else
                {
                    //ellers opdateres tabellen uden at håndtere billeder
                    tilbud.UpdateTilbudRolle(Convert.ToInt32(Request.QueryString["id"]));
                }
                break;
            default:
                //Gemmer billedet både i original opløsning og resized
                FileUpload_Tilbud.SaveAs(Server.MapPath("~/billeder/") + NewFileName);
                Helpers.ResizingTilbud(NewFileName);
                //og gemmer derefter objektet i databasen
                tilbud.GemTilbud();
                Response.Redirect("AdminTilbud.aspx");
                break;
        }
        Response.Redirect("AdminTilbud.aspx");
    }
}