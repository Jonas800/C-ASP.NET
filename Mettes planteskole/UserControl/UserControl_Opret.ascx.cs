using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Helpers;

public partial class UserControl_Opret : System.Web.UI.UserControl
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Button_Opret_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT * FROM brugere WHERE bruger_email = @email";

        cmd.Parameters.AddWithValue("@email", TextBox_Email.Text);
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            Label_Error.Text = "Emailen er allerede brugt";
        }
        else
        {
            Bruger bruger = new Bruger();

            bruger.OpretBruger(TextBox_Email.Text, PasswordHash(TextBox_Kodeord.Text), TextBox_Navn.Text, TextBox_By.Text, TextBox_Adresse.Text, Convert.ToInt32(TextBox_Postnummer.Text), Convert.ToInt32(TextBox_Telefon.Text), false);

            Session["bruger"] = bruger;

            Label_Success.Visible = true;
            Panel_Opret.Visible = false;
        }
        conn.Close();
    }
    public string PasswordHash(string kodeord)
    {
        //The password hash is generated with the RFC 2898 algorithm using a 128-bit salt, a 256-bit subkey, and 1000 iterations. The format of the generated hash bytestream is {0x00, salt, subkey}, which is base-64 encoded before it is returned.
        var hashedPassword = Crypto.HashPassword(kodeord);

        return hashedPassword;
    }
}