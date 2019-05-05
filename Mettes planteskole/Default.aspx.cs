using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        //Finder alt fra forsidetabellen frem og binder det til en repeater
        cmd.CommandText = "SELECT * FROM forside";
        conn.Open();
        Repeater_Forside.DataSource = cmd.ExecuteReader();
        Repeater_Forside.DataBind();
        conn.Close();

        //Finder alt fra kontaktoplysningertabellen frem og binder det til en repeater
        cmd.CommandText = "SELECT * FROM kontaktoplysninger";
        conn.Open();
        Repeater_Kontaktoplysninger.DataSource = cmd.ExecuteReader();
        Repeater_Kontaktoplysninger.DataBind();
        conn.Close();

        //Finder alt fra åbningstidertabellen frem og binder det til en repeater
        cmd.CommandText = "SELECT * FROM åbningstider ORDER by åbningstid_rækkefølge";
        conn.Open();
        Repeater_Åbningstider.DataSource = cmd.ExecuteReader();
        Repeater_Åbningstider.DataBind();
        conn.Close();

    }
}