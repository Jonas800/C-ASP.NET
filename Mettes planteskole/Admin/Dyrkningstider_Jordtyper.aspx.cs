using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Dyrkningstider_Jordtyper : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //Sessionbeskyttelse
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;

            if (bruger.Er_Admin)
            {
                if (!IsPostBack)
                {
                    #region Dyrkningstider
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    //Henter alle dyrkningstider og binder dem til en repeater
                    cmd.CommandText = "SELECT * FROM dyrkningstider";
                    conn.Open();
                    Repeater_Dyrkningstider.DataSource = cmd.ExecuteReader();
                    Repeater_Dyrkningstider.DataBind();
                    conn.Close();



                    //Forudfylder formularen ved tryk på ret og sletter kolonnen i databasen ved slet, via et id fra url'en
                    switch (Request.QueryString["action"])
                    {
                        case "ret_dyrkningstid":
                            cmd.Parameters.AddWithValue("@dyrkningstid_id", Request.QueryString["dyrkningstid_id"]);
                            cmd.CommandText = "SELECT * FROM dyrkningstider WHERE dyrkningstid_id = @dyrkningstid_id";

                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                TextBox_Dyrkningstid.Text = reader["dyrkningstid_navn"].ToString();
                            }
                            conn.Close();
                            break;

                        case "slet_dyrkningstid":
                            cmd.Parameters.AddWithValue("@dyrkningstid_id", Request.QueryString["dyrkningstid_id"]);
                            cmd.CommandText = "DELETE FROM dyrkningstider WHERE dyrkningstid_id = @dyrkningstid_id";

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            Response.Redirect("Dyrkningstider_Jordtyper.aspx");
                            break;
                    }

                    #endregion
                    #region Jordtyper
                    cmd.CommandText = "SELECT * FROM jordtyper";
                    conn.Open();
                    Repeater_Jordtyper.DataSource = cmd.ExecuteReader();
                    Repeater_Jordtyper.DataBind();
                    conn.Close();


                    //Forudfylder formularen ved tryk på ret og sletter kolonnen i databasen ved slet, via et id fra url'en


                    switch (Request.QueryString["action"])
                    {
                        case "ret_jordtype":
                            cmd.Parameters.AddWithValue("@jordtype_id", Request.QueryString["jordtype_id"]);
                            cmd.CommandText = "SELECT * FROM jordtyper WHERE jordtype_id = @jordtype_id";

                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                TextBox_Jordtyper.Text = reader["jordtype_navn"].ToString();
                            }
                            conn.Close();
                            break;

                        case "slet_jordtype":
                            cmd.Parameters.AddWithValue("@jordtype_id", Request.QueryString["jordtype_id"]);
                            cmd.CommandText = "DELETE FROM jordtyper WHERE jordtype_id = @jordtype_id";

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            Response.Redirect("Dyrkningstider_Jordtyper.aspx");
                            break;
                    }
                    #endregion
                }
            }
        }
    }
    protected void Button_Dyrkningstid_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@navn", TextBox_Dyrkningstid.Text);

        //Opretter en ny kolonne eller retter en alt efter url'en 
        switch (Request.QueryString["action"])
        {
            case "ret_dyrkningstid":
                cmd.CommandText = "UPDATE dyrkningstider SET dyrkningstid_navn = @navn WHERE dyrkningstid_id = @id";
                cmd.Parameters.AddWithValue("@id", Request.QueryString["dyrkningstid_id"]);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                break;
            default:
                cmd.CommandText = "INSERT INTO dyrkningstider (dyrkningstid_navn) VALUES (@navn)";

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                break;
        }
        Response.Redirect("Dyrkningstider_Jordtyper.aspx");
    }
    protected void Button_Jordtyper_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@navn", TextBox_Jordtyper.Text);

        //Opretter en ny kolonne eller retter en alt efter url'en 
        switch (Request.QueryString["action"])
        {
            case "ret_jordtype":
                cmd.CommandText = "UPDATE jordtyper SET jordtype_navn = @navn WHERE jordtype_id = @id";
                cmd.Parameters.AddWithValue("@id", Request.QueryString["jordtype_id"]);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                break;
            default:
                cmd.CommandText = "INSERT INTO jordtyper (jordtype_navn) VALUES (@navn)";

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                break;
        }
        Response.Redirect("Dyrkningstider_Jordtyper.aspx");
    }
}