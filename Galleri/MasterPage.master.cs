using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = conn;

        cmd.CommandText = "SELECT * from categories ORDER BY category_name asc";
        conn.Open();
        Repeater_Galleries.DataSource = cmd.ExecuteReader();
        Repeater_Galleries.DataBind();
        conn.Close();
        conn.Open();
        Repeater_Slider.DataSource = cmd.ExecuteReader();
        Repeater_Slider.DataBind();
        conn.Close();
    }
}
