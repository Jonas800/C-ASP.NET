using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Nyhedsbrevsliste : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null) { 
        SqlCommand cmd = new SqlCommand("SELECT * FROM nyhedsbreve", conn);
        conn.Open();
        Repeater_Nyhedsbrevsliste.DataSource = cmd.ExecuteReader();
        Repeater_Nyhedsbrevsliste.DataBind();
        conn.Close();
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
}