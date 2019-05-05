using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Log ud
        if (Request.QueryString["logout"] != null)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }

        //Tjekker om sessionen er tom, hvis den ikke er, smider den sessionen over i et Bruger objekt. Derefter finder den ud af om brugeren har rollerettigheder til at tilgå administrationsdelen af siden 
        if (Session["bruger"] != null)
        {
            Bruger bruger = (Bruger)Session["bruger"];

            if (bruger.Rolle_Navn != rolle.Administrator)
            {
                Response.Redirect("Profil.aspx");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
}
