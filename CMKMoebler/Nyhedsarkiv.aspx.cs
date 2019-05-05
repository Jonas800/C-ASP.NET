using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Nyhedsarkiv : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "SELECT * FROM nyheder LEFT OUTER JOIN brugere ON fk_bruger_id = bruger_id ORDER BY nyhed_dato DESC";

        conn.Open();
        Repeater_Nyheder.DataSource = cmd.ExecuteReader();
        Repeater_Nyheder.DataBind();
        conn.Close();
    }
}