using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;
            if (bruger.Rolle != 1)
            {
                Panel_Nav.Visible = false;
            }
        }
        else
        {
            Panel_Nav.Visible = false;
        }

        if (Request.QueryString["logud"] != null)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
        if (Session["besked"] != null)
        {
            Panel_Besked.Visible = true;
            Label_Besked.Text = Session["besked"].ToString();
            Session.Remove("besked");
        }

        Helpers.Brødkrumme(Literal_Brødkrumme_Title, Label_Brødkrumme, "/admin/", "Motioncentret Adminpanel", "OpretAktivitet", "Aktivitet");
    }
}
