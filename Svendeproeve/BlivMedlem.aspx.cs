﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BlivMedlem : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand("SELECT oplysning_medlem FROM oplysninger", conn);

        conn.Open();
        Repeater_BlivMedlem.DataSource = cmd.ExecuteReader();
        Repeater_BlivMedlem.DataBind();
        conn.Close();
    }
}