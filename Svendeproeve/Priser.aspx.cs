using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Priser : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "SELECT *, (SELECT pris_pris = pris_pris * (100 - pris_tilbud) / 100 WHERE pris_tilbud > 0) AS pris_nu FROM priser";

        conn.Open();
        Repeater_Priser.DataSource = cmd.ExecuteReader();
        Repeater_Priser.DataBind();
        conn.Close();
    }
}