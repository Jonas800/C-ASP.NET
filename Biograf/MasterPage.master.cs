using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class MasterPage : System.Web.UI.MasterPage
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        //Log ud
        if (Request.QueryString["logout"] != null)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
        if (Session["bruger"] != null)
        {
            Literal_Login.Visible = false;

            //Tilbud på alle sider der filtreres efter rolle
            Bruger bruger = (Bruger)Session["bruger"];

            SqlCommand cmd = new SqlCommand("SELECT TOP 1 tilbud_billede FROM tilbud WHERE tilbud_rolle = @bruger_rolle ORDER BY newid()", conn);
            cmd.Parameters.AddWithValue("@bruger_rolle", bruger.Rolle_Navn.ToString());
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Image_Tilbud.ImageUrl = "/billeder/tilbud/" + reader["tilbud_billede"];
            }
            conn.Close();

        }
        else
        {
            Literal_Profil.Visible = false;
            Literal_Logud.Visible = false;
        }


    }
    protected void Timer_Point_Tick(object sender, EventArgs e)
    {
        //Timer der giver en bruger et point i et interval (sættes på aspx siden)
        if (Session["bruger"] != null)
        {
            Bruger bruger = (Bruger)Session["bruger"];

            bruger.Point++;

            bruger.PointUpdate();
        }
    }
}
