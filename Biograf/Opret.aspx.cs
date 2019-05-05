using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Opret : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        //Opretter bruger objektet og knytter de sendte data til det
        Bruger bruger = new Bruger();

        bruger.Navn = TextBox_Navn.Text;
        bruger.Email = TextBox_Email.Text;
        bruger.Kodeord = TextBox_Kodeord.Text;

        if (CheckBox_Nyhedsbrev.Checked)
        {
            bruger.Nyhedsbrev = true;
        }
        else
        {
            bruger.Nyhedsbrev = false;
        }

        //Gemmer objektet i databasen og gemmer det i sessionen
        bruger.GemProfil();

        Session["bruger"] = bruger;

        Response.Redirect("Profil.aspx");
    }
}