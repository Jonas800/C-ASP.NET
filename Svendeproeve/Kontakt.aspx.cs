﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Kontakt : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT oplysning_kontakt, oplysning_adresse, oplysning_telefon, oplysning_mobil, oplysning_email FROM oplysninger";

        conn.Open();
        Repeater_Kontakt.DataSource = cmd.ExecuteReader();
        Repeater_Kontakt.DataBind();
        conn.Close();
    }
}