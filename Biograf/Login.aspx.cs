using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        //Opretter objektet og forsøger at logge ind
        Bruger bruger = new Bruger(TextBox_Email.Text, TextBox_Kodeord.Text);

        //Tjekker om brugeren er gyldig
        if (bruger.ID > 0)
        {
            Session["bruger"] = bruger;

            //Tjekker rolle og sender brugeren til enten admin panelet eller profil
            if (bruger.Rolle_Navn == rolle.Administrator)
            {
                Response.Redirect("AdminDefault.aspx");
            }
            else
            {
                Response.Redirect("Profil.aspx");
            }
        }
        else
        {
            Label_Error.Text = "Vi kan ikke finde din email eller dit kodeord";
        }
    }
}