using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Brugere : System.Web.UI.Page
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
                    cmd.Connection = conn;

                    //Henter alle bruger, inner joiner dem med kunder og binder dem til en repeater
                    cmd.CommandText = "SELECT * FROM brugere INNER JOIN kunder ON fk_bruger_id = bruger_id";

                    conn.Open();
                    Repeater_Kunder.DataSource = cmd.ExecuteReader();
                    Repeater_Kunder.DataBind();
                    conn.Close();

                    //Forudfylder formularen ved tryk på ret og sletter kolonnen i databasen ved slet, via et id fra url'en
                    if (Request.QueryString["action"] == "ret")
                    {
                        RequiredFieldValidator_Kodeord.Enabled = false;
                        cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                        cmd.CommandText = "SELECT * FROM brugere INNER JOIN kunder ON fk_bruger_id = bruger_id WHERE bruger_id = @id";

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            TextBox_Email.Text = reader["bruger_email"].ToString();
                            TextBox_Navn.Text = reader["kunde_navn"].ToString();
                            TextBox_Adresse.Text = reader["kunde_adresse"].ToString();
                            TextBox_By.Text = reader["kunde_by"].ToString();
                            TextBox_Postnummer.Text = reader["kunde_postnummer"].ToString();
                            TextBox_Telefon.Text = reader["kunde_telefon"].ToString();
                            CheckBox_Rolle.Checked = Convert.ToBoolean(reader["bruger_er_admin"]);
                            CheckBox_Deaktiver.Checked = Convert.ToBoolean(reader["bruger_er_aktiv"]);
                        }
                        conn.Close();

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
        cmd.Parameters.AddWithValue("@email", TextBox_Email.Text);
        cmd.Parameters.AddWithValue("@by", TextBox_By.Text);
        cmd.Parameters.AddWithValue("@postnummer", TextBox_Postnummer.Text);
        cmd.Parameters.AddWithValue("@adresse", TextBox_Adresse.Text);
        cmd.Parameters.AddWithValue("@telefon", TextBox_Telefon.Text);

        //Ser på om checkboxen er checked og giver rettigheder derefter
        bool er_admin = false;
        if (CheckBox_Rolle.Checked)
        {
            er_admin = true;
        }
        bool er_aktiv = false;
        if (CheckBox_Deaktiver.Checked)
        {
            er_aktiv = true;
        }
        //Opretter en ny kolonne eller retter en alt efter url'en 
        switch (Request.QueryString["action"])
        {
            case "ret":
                if (TextBox_Kodeord.Text == String.Empty)
                {
                    cmd.CommandText = "UPDATE brugere SET bruger_email = @email, bruger_er_admin = @er_admin, bruger_er_aktiv = @er_aktiv WHERE bruger_id = @id; UPDATE kunder SET kunde_navn = @navn, kunde_by = @by, kunde_postnummer = @postnummer, kunde_adresse = @adresse, kunde_telefon = @telefon WHERE fk_bruger_id = @id";
                }
                else
                {
                    cmd.Parameters.AddWithValue("@kodeord", TextBox_Kodeord.Text);
                    cmd.CommandText = "UPDATE brugere SET bruger_email = @email, bruger_er_admin = @er_admin, bruger_er_aktiv = @er_aktiv, bruger_kodeord = @kodeord WHERE bruger_id = @id; UPDATE kunder SET kunde_navn = @navn, kunde_by = @by, kunde_postnummer = @postnummer, kunde_adresse = @adresse, kunde_telefon = @telefon WHERE fk_bruger_id = @id";
                }
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                cmd.Parameters.AddWithValue("@er_admin", er_admin);
                cmd.Parameters.AddWithValue("@er_aktiv", er_aktiv);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("Brugere.aspx");

                break;
            default:
                cmd.CommandText = "SELECT * FROM brugere WHERE bruger_email = @email";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Label_Error.Text = "Emailen er allerede brugt";
                }
                else
                {
                    //Opretter Bruger objekt og gemmer det i databasen
                    Bruger bruger = new Bruger();

                    bruger.OpretBrugerForAdmin(TextBox_Email.Text, TextBox_Kodeord.Text, TextBox_Navn.Text, TextBox_By.Text, TextBox_Adresse.Text, Convert.ToInt32(TextBox_Postnummer.Text), Convert.ToInt32(TextBox_Telefon.Text), er_admin);
                    Response.Redirect("Brugere.aspx");

                }
                conn.Close();
                break;
        }
    }
}