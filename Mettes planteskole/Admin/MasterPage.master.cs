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
        ////Sessionbeskyttelse
        //if (Session["bruger"] != null)
        //{
        //    Bruger bruger = Session["bruger"] as Bruger;

        //    if (bruger.Er_Admin)
        //    {
        //    }
        //    else
        //    {
        //        Response.Redirect("~/Default.aspx");
        //    }
        //}
        //else
        //{
        //    Response.Redirect("~/Default.aspx");
        //}
    }
    public string NavHighlight(string rawUrl)
    {
        string url = Request.RawUrl;

        if (url == rawUrl)
        {
            return "NavHighlight";
        }


        return "";
    }
}
