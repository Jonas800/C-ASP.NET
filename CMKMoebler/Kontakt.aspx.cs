using System;
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

        cmd.CommandText = "SELECT oplysning_åbningstid FROM oplysninger";
        conn.Open();
        Repeater_Åbningstider.DataSource = cmd.ExecuteReader();
        Repeater_Åbningstider.DataBind();
        conn.Close();
    }
    protected void Button_Send_Click(object sender, EventArgs e)
    {
        //Send mail

        //Henter admins email og navn ud af databasen
        SqlCommand cmd = new SqlCommand("SELECT oplysning_email FROM oplysninger", conn);

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
            mail.Subject = TextBox_Navn.Text;
            mail.Body = TextBox_Kommentar.Text + "\r\n\r Adresse: " + TextBox_Adresse.Text + "\r\n\r Telefon: " + TextBox_Telefon.Text;
            //mail.IsBodyHtml = true;
            //Fra og til værdier
            mail.From = new MailAddress(TextBox_Email.Text, TextBox_Navn.Text);
            mail.To.Add(new MailAddress(reader["oplysning_email"].ToString(), "Mettes planteskole"));

            //Sender og sletter data efter den er sendt afsted
            smtp.Send(mail);
            smtp.Dispose();

            Panel_Besked.Visible = true;
            Label_Besked.Text = "Tak for din henvendelse. Du vil høre fra os hurtigst muligt.";
        }
        else
        {
            Panel_Besked.Visible = true;
            Label_Besked.Text = "Noget er gået galt med vores email, vi beklager.";
        }
        conn.Close();
    }
}