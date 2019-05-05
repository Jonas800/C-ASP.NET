using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Kontaktoplysninger : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM kontaktoplysninger";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                TextBox_Adresse.Text = reader["kontaktoplysning_adresse"].ToString();
                TextBox_By.Text = reader["kontaktoplysning_by"].ToString();
                TextBox_Email.Text = reader["kontaktoplysning_email"].ToString();
                TextBox_Postnummer.Text = reader["kontaktoplysning_postnummer"].ToString();
                TextBox_Telefon.Text = reader["kontaktoplysning_telefon"].ToString();
            }
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        //Sessionbeskyttelse
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;

            if (bruger.Er_Admin)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE kontaktoplysninger SET kontaktoplysning_adresse = @adresse, kontaktoplysning_by = @by, kontaktoplysning_email = @email, kontaktoplysning_postnummer = @postnummer, kontaktoplysning_telefon = @telefon";

                cmd.Parameters.AddWithValue("@adresse", TextBox_Adresse.Text);
                cmd.Parameters.AddWithValue("@by", TextBox_By.Text);
                cmd.Parameters.AddWithValue("@email", TextBox_Email.Text);
                cmd.Parameters.AddWithValue("@postnummer", TextBox_Postnummer.Text);
                cmd.Parameters.AddWithValue("@telefon", TextBox_Telefon.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                Response.Redirect("Kontaktoplysninger.aspx");
            }
        }
    }
}