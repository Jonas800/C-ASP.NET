using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class MasterPageAdmin : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /* LOGIN/LOGOUT */

        if (Request.QueryString["logout"] != null)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        if (Session["user_id"] == null || Convert.ToInt16(Session["role_id"]) != 1)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
