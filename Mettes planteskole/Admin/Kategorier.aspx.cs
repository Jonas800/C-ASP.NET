using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Kategorier : System.Web.UI.Page
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
                    SqlCommand cmd = new SqlCommand();

                    ////Henter alle kategorier og binder dem til en repeater
                    //cmd.CommandText = "SELECT * FROM kategorier";
                    //conn.Open();
                    //Repeater_Kategorier.DataSource = cmd.ExecuteReader();
                    //Repeater_Kategorier.DataBind();
                    //conn.Close();

                    db.PopulateRepeater(cmd, "SELECT * FROM kategorier", Repeater_Kategorier);

                    //Forudfylder formularen ved tryk på ret og sletter kolonnen i databasen ved slet, via et id fra url'en
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                    switch (Request.QueryString["action"])
                    {
                        case "ret":
                            cmd.CommandText = "SELECT * FROM kategorier WHERE kategori_id = @id";

                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                TextBox_Navn.Text = reader["kategori_navn"].ToString();
                                CheckBox_Er_Aktiv.Checked = Convert.ToBoolean(reader["kategori_er_aktiv"]);
                            }
                            conn.Close();
                            break;

                        case "slet":
                            cmd.CommandText = "UPDATE kategorier SET kategori_er_aktiv = @er_aktiv WHERE kategori_id = @id";
                            cmd.Parameters.AddWithValue("@er_aktiv", false);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            Response.Redirect("Kategorier.aspx");
                            break;
                    }
                }
            }
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@navn", TextBox_Navn.Text);

        switch (Request.QueryString["action"])
        {
            case "ret":
                cmd.CommandText = "UPDATE kategorier SET kategori_navn = @navn, kategori_er_aktiv = @er_aktiv WHERE kategori_id = @id";
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                cmd.Parameters.AddWithValue("@er_aktiv", CheckBox_Er_Aktiv.Checked);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                break;
            default:
                cmd.CommandText = "INSERT INTO kategorier (kategori_navn) VALUES (@navn)";

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                break;
        }
        Response.Redirect("Kategorier.aspx");
    }
}