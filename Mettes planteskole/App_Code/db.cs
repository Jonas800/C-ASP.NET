using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for db
/// </summary>
public class db
{
    private static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    public static void PopulateRepeater(SqlCommand cmd, string commandtext, Repeater repeater)
    {
        cmd.Connection = conn;
        cmd.CommandText = commandtext;
        conn.Open();
        repeater.DataSource = cmd.ExecuteReader();
        repeater.DataBind();
        conn.Close();
    }
}