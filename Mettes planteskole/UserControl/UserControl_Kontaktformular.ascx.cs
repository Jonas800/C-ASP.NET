using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Kontaktformular : System.Web.UI.UserControl
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //Forudfylder formularen

        //Stopper genudfyldning ved postback
        if (!IsPostBack)
        {
            //Der skal kun forudfyldes hvis der er logget på (tomt ellers)
            if (Session["kunde"] != null && Session["bruger"] != null)
            {
                //Henter værdier fra sessionen
                Bruger bruger = Session["bruger"] as Bruger;
                Kunde kunde = Session["kunde"] as Kunde;

                TextBox_Navn.Text = kunde.Navn;
                TextBox_Email.Text = bruger.Email;
            }
        }
    }
    protected void Button_Send_Click(object sender, EventArgs e)
    {
        //Send mail

        //Henter admins email og navn ud af databasen
        SqlCommand cmd = new SqlCommand("SELECT bruger_email FROM brugere WHERE bruger_er_admin = @true", conn);
        cmd.Parameters.AddWithValue("@true", true);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            //Mail instillinger
            SmtpClient smtp = new SmtpClient("localhost", 25);
            smtp.UseDefaultCredentials = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //Mail objekt og værdier (emne og besked)
            MailMessage mail = new MailMessage();
            mail.Subject = TextBox_Emne.Text;
            mail.Body = TextBox_Besked.Text;

            //Fra og til værdier
            mail.From = new MailAddress(TextBox_Email.Text, TextBox_Navn.Text);
            mail.To.Add(new MailAddress(reader["bruger_email"].ToString(), "Mettes planteskole"));

            //Sender og sletter data efter den er sendt afsted
            smtp.Send(mail);
            smtp.Dispose();
        }
        else
        {
            Label_Fejl.Text = "Noget er gået galt med vores email, vi beklager.";
        }
        conn.Close();
    }
}