using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_UserControl_Login : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Højre kolonne visning når brugeren er logget ind
        if (Session["bruger"] != null)
        {
            //Skifter paneler fra login til logud
            Panel_Login.Visible = false;
            Panel_Logud.Visible = true;

            //Tilføjer et link til admin panelet hvis brugeren er admin
            Bruger bruger = Session["bruger"] as Bruger;

            if (bruger.Er_Admin == true)
            {
                Literal_Go_Admin.Text = "<a href='/Admin/Default.aspx'>Admin panel</a>";
            }

            //Viser kundens navn
            if (Session["kunde"] != null)
            {
                Kunde kunde = Session["kunde"] as Kunde;

                Label_Navn.Text = kunde.Navn;
            }
        }

        //Logud link der sletter sessionen
        if (Request.QueryString["logud"] != null)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
    protected void Button_Login_Click(object sender, EventArgs e)
    {
        //Login

        //Opretter et bruger-objekt og forsøger at logge ind
        Bruger bruger = new Bruger(TextBox_Email.Text, TextBox_Kodeord.Text);

        //Tjekker om brugeren er gyldig
        if (bruger.ID > 0)
        {
            Session["bruger"] = bruger;

            if (bruger.Er_Admin)
            {
                Response.Redirect("~/Admin/Default.aspx");
            }
            else
            {
                Response.Redirect(Request.RawUrl);
            }
        }
        else
        {
            Label_Error.Text = "Ugyldig email eller kodeord";
        }
    }
}