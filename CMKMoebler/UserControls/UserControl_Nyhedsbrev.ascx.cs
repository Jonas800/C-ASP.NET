using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_UserControl_Nyhedsbrev : System.Web.UI.UserControl
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button_Tilmeld_ServerClick(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.Parameters.AddWithValue("@email", TextBox_Email.Text);

        cmd.CommandText = "SELECT * FROM nyhedsbreve WHERE nyhedsbrev_email = @email";
        conn.Open();
        if (cmd.ExecuteReader().Read())
        {
            conn.Close();
            Label_Nyhedsbrev_Besked.Text = "Du er allerede tilmeldt.";
        }
        else
        {
            conn.Close();

            int indsatte_rækker = 0;
            cmd.CommandText = "INSERT INTO nyhedsbreve (nyhedsbrev_email) VALUES (@email)";
            conn.Open();
            indsatte_rækker = cmd.ExecuteNonQuery();
            conn.Close();
            if (indsatte_rækker > 0)
            {
                Label_Nyhedsbrev_Besked.Text = "Tillykke, du er nu tilmeldt vores nyhedsbrev.";
            }
            else
            {
                Label_Nyhedsbrev_Besked.Text = "Noget gik galt";
            }
        }


    }
    protected void Button_Frameld_ServerClick(object sender, EventArgs e)
    {
        int slettede_rækker = 0;
        SqlCommand cmd = new SqlCommand("DELETE FROM nyhedsbreve WHERE nyhedsbrev_email = @email", conn);
        cmd.Parameters.AddWithValue("@email", TextBox_Email.Text);
        conn.Open();
        slettede_rækker = cmd.ExecuteNonQuery();
        conn.Close();

        if (slettede_rækker > 0)
        {
            Label_Nyhedsbrev_Besked.Text = "Du er nu frameldt.";
        }
        else
        {
            Label_Nyhedsbrev_Besked.Text = "Email findes ikke i vores database.";
        }
    }
}