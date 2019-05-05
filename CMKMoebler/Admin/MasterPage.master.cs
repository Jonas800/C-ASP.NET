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
        if (Session["bruger"] == null)
        {
            Panel_Nav.Visible = false;
        }
        if (Request.QueryString["logud"] != null)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
        Helpers.Brødkrumme(Literal_Brødkrumme_Title, Label_Brødkrumme, "/admin/", "CMK Møbler Adminpanel", "OpretProdukt", "Produkt");
    }
}
