using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Navigation_Login : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user_id"] != null && Convert.ToInt16(Session["role_id"]) == 1)
        {
            Panel_Login.Visible = false;
            Panel_Logout.Visible = true;
        }
        else if (Session["user_id"] != null && Convert.ToInt16(Session["role_id"]) == 2)
        {
            Panel_Login.Visible = false;
        }
    }
}