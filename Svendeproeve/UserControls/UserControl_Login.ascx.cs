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
        if (Session["bruger"] != null)
        {
            //Skifter paneler fra login til logud
            Panel_Login.Visible = false;
            //Panel_Logud.Visible = true;
        }

        //Logud link der sletter sessionen
        if (Request.QueryString["logud"] != null)
        {
            Session.Abandon();
            Response.Redirect(Request.RawUrl);
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
            Response.Redirect(Request.RawUrl);
        }
        else
        {
            Label_Error.Text = "Ugyldig email eller kodeord.";
        }
    }
}