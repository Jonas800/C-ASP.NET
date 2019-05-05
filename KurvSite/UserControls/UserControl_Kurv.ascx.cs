using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_UserControl_Kurv : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        VisKurv();
    }
    public void VisKurv()
    {
        Repeater_Kurv.DataSource = Kurv.HentKurven();
        Repeater_Kurv.DataBind();
    }
}