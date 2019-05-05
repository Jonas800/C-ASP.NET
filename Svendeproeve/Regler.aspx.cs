using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Regler : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("SELECT oplysning_regler FROM oplysninger", conn);

        conn.Open();
        Repeater_Regler.DataSource = cmd.ExecuteReader();
        Repeater_Regler.DataBind();
        conn.Close();
    }
}