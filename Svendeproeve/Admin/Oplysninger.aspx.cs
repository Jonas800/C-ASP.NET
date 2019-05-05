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
                    TextBox_Adresse.Text = reader["oplysning_adresse"].ToString();
                    TextBox_Footer.Text = reader["oplysning_footer"].ToString();
                    TextBox_Mobil.Text = reader["oplysning_mobil"].ToString();
                    TextBox_Regler.Text = reader["oplysning_regler"].ToString();
                    TextBox_Telefon.Text = reader["oplysning_telefon"].ToString();
                    TextBox_Forside.Text = reader["oplysning_forside"].ToString();
                    TextBox_Medlem.Text = reader["oplysning_medlem"].ToString();
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
        cmd.Parameters.AddWithValue("@telefon", TextBox_Telefon.Text);
        cmd.Parameters.AddWithValue("@regler", TextBox_Regler.Text);
        cmd.Parameters.AddWithValue("@mobil", TextBox_Mobil.Text);
        cmd.Parameters.AddWithValue("@footer", TextBox_Footer.Text);
        cmd.Parameters.AddWithValue("@adresse", TextBox_Adresse.Text);
        cmd.Parameters.AddWithValue("@forside", TextBox_Forside.Text);
        cmd.Parameters.AddWithValue("@medlem", TextBox_Medlem.Text);


        cmd.CommandText = "SELECT * FROM oplysninger";
        conn.Open();
        if (cmd.ExecuteReader().Read())
        {
            conn.Close();
            int opdaterede_rækker = 0;
            cmd.CommandText = "UPDATE oplysninger SET oplysning_åbningstid = @oplysning_åbningstid, oplysning_kontakt = @oplysning_kontakt, oplysning_email = @oplysning_email, oplysning_telefon = @telefon, oplysning_regler = @regler, oplysning_mobil = @mobil, oplysning_footer = @footer, oplysning_adresse = @adresse, oplysning_forside = @forside, oplysning_medlem = @medlem";
            conn.Open();
            opdaterede_rækker = cmd.ExecuteNonQuery();
            conn.Close();

            if (opdaterede_rækker > 0)
            {
                Session["besked"] = "Rettelse lykkedes.";
            }
            else
            {
                Session["besked"] = "Rettelse fejlede, prøv igen.";
            }
            Response.Redirect("Oplysninger.aspx");
        }
        else
        {
            conn.Close();
            int indsatte_rækker = 0;
            cmd.CommandText = "INSERT INTO oplysninger (oplysning_åbningstid, oplysning_kontakt, oplysning_email, oplysning_telefon, oplysning_regler, oplysning_mobil, oplysning_footer, oplysning_adresse, oplysning_forside, oplysning_medlem) VALUES (@oplysning_åbningstid, @oplysning_kontakt, @oplysning_email, @telefon, @regler, @mobil, @footer, @adresse, @forside, @medlem)";
            conn.Open();
            indsatte_rækker = cmd.ExecuteNonQuery();
            conn.Close();
            if (indsatte_rækker > 0)
            {
                Session["besked"] = "Oprettelse lykkedes.";
            }
            else
            {
                Session["besked"] = "Oprettelse fejlede, prøv igen.";
            }
            Response.Redirect("Oplysninger.aspx");
        }
    }
}