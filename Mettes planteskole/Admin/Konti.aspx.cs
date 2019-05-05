using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Konti : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {        //Finder den aktuelle bruger (i tilfælde af at der skulle være flere end en admin
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;

            if (!IsPostBack)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                //Finder brugeren der er logget på
                cmd.CommandText = "SELECT * FROM brugere WHERE bruger_id = @id";
                cmd.Parameters.AddWithValue("@id", bruger.ID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    TextBox_Email.Text = reader["bruger_email"].ToString();
                }
            }
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        Bruger bruger = Session["bruger"] as Bruger;
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@id", bruger.ID);
        cmd.Parameters.AddWithValue("@email", TextBox_Email.Text);

        //Tjekker om kodeord skal ændres
        if (TextBox_Kodeord.Text == String.Empty)
        {
            //Håndterer ikke kodeord, kun email
            cmd.CommandText = "UPDATE brugere SET bruger_email = @email WHERE bruger_id = @id";
        }
        else
        {
            //Håndterer både kodeord og email
            cmd.Parameters.AddWithValue("@kodeord", TextBox_Kodeord.Text);
            cmd.CommandText = "UPDATE brugere SET bruger_email = @email, bruger_kodeord = @kodeord WHERE bruger_id = @id";
        }
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        Response.Redirect("Konti.aspx");
    }
}