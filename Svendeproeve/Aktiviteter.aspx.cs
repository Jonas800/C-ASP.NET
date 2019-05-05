using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Aktiviteter : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("SELECT aktivitet_navn, aktivitet_billede, aktivitet_tekst FROM aktiviteter", conn);

        conn.Open();
        Repeater_Aktiviteter.DataSource = cmd.ExecuteReader();
        Repeater_Aktiviteter.DataBind();
        conn.Close();
    }
}