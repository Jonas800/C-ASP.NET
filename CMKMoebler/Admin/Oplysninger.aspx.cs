using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Oplysninger : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            if (!IsPostBack)
            {
                cmd.CommandText = "SELECT * FROM oplysninger";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    TextBox_Kontaktoplysninger.Text = reader["oplysning_kontakt"].ToString();
                    TextBox_Åbningstider.Text = reader["oplysning_åbningstid"].ToString();
                    TextBox_Email.Text = reader["oplysning_email"].ToString();
                }
                conn.Close();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@oplysning_kontakt", TextBox_Kontaktoplysninger.Text);
        cmd.Parameters.AddWithValue("@oplysning_åbningstid", TextBox_Åbningstider.Text);
        cmd.Parameters.AddWithValue("@oplysning_email", TextBox_Email.Text);

        cmd.CommandText = "SELECT * FROM oplysninger";
        conn.Open();
        if (cmd.ExecuteReader().Read())
        {
            conn.Close();
            int opdaterede_rækker = 0;
            cmd.CommandText = "UPDATE oplysninger SET oplysning_åbningstid = @oplysning_åbningstid, oplysning_kontakt = @oplysning_kontakt, oplysning_email = @oplysning_email";
            conn.Open();
            opdaterede_rækker = cmd.ExecuteNonQuery();
            conn.Close();

            if (opdaterede_rækker > 0)
            {
                Label_Besked.Text = "Rettelse lykkedes.";
            }
            else
            {
                Label_Besked.Text = "Rettelse fejlede, prøv igen.";
            }
        }
        else
        {
            conn.Close();
            int indsatte_rækker = 0;
            cmd.CommandText = "INSERT INTO oplysninger (oplysning_åbningstid, oplysning_kontakt, oplysning_email) VALUES (@oplysning_åbningstid, @oplysning_kontakt, @oplysning_email)";
            conn.Open();
            indsatte_rækker = cmd.ExecuteNonQuery();
            conn.Close();
            if (indsatte_rækker > 0)
            {
                Label_Besked.Text = "Oplysninger er nu oprettet.";
            }
            else
            {
                Label_Besked.Text = "Oprettelse af oplysninger fejlede.";
            }
        }
    }
}