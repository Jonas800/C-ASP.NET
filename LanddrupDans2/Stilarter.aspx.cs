using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Stilarter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Repeater_Stilarter.DataSource = db.SelectAllFrom("styles");
        Repeater_Stilarter.DataBind();
    }
}