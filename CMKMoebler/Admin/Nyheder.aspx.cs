using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Nyheder : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            SelectNyheder(cmd);

            if (Session["besked"] != null)
            {
                Label_Besked.Text = Session["besked"].ToString();
                Session.Remove("besked");
            }

            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            switch (Request.QueryString["action"])
            {
                case "ret":
                    if (!IsPostBack)
                    {
                        cmd.CommandText = "SELECT * FROM nyheder WHERE nyhed_id = @id";
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            TextBox_Nyhed.Text = reader["nyhed_tekst"].ToString();
                            TextBox_Overskrift.Text = reader["nyhed_overskrift"].ToString();
                        }
                        conn.Close();

                    }
                    break;
                case "slet":

                    int slettede_rækker = 0;
                    cmd.CommandText = "DELETE FROM nyheder WHERE nyhed_id = @id";
                    conn.Open();
                    slettede_rækker = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (slettede_rækker > 0)
                    {
                        Session["besked"] = "Nyhed blev slettet.";
                        Response.Redirect("Nyheder.aspx");
                    }
                    else
                    {
                        Label_Besked.Text = "Nyhed blev ikke slettet. Prøv igen eller kontakt webmaster.";
                    }

                    SelectNyheder(cmd);
                    break;
                default:
                    break;
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    private void SelectNyheder(SqlCommand cmd)
    {
        cmd.CommandText = "SELECT * FROM nyheder LEFT OUTER JOIN brugere ON fk_bruger_id = bruger_id ORDER BY nyhed_dato DESC";
        conn.Open();
        Repeater_Nyheder.DataSource = cmd.ExecuteReader();
        Repeater_Nyheder.DataBind();
        conn.Close();
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        Bruger bruger = Session["bruger"] as Bruger;

        cmd.Parameters.AddWithValue("@tekst", TextBox_Nyhed.Text);
        cmd.Parameters.AddWithValue("@overskrift", TextBox_Overskrift.Text);
        cmd.Parameters.AddWithValue("@bruger_id", bruger.ID);
        cmd.Parameters.AddWithValue("@dato", DateTime.Now);

        switch (Request.QueryString["action"])
        {
            case "ret":
                int opdaterede_rækker = 0;
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                cmd.CommandText = "UPDATE nyheder SET nyhed_tekst = @tekst, nyhed_overskrift = @overskrift, nyhed_dato = @dato, fk_bruger_id = @bruger_id WHERE nyhed_id = @id";
                conn.Open();
                opdaterede_rækker = cmd.ExecuteNonQuery();
                conn.Close();

                if (opdaterede_rækker > 0)
                {
                    Label_Besked.Text = "Rettelse lykkedes.";
                }
                else
                {
                    Label_Besked.Text = "Rettelse fejlede. Prøv igen eller kontakt webmaster.";
                }

                break;
            default:
                int indsatte_rækker = 0;
                cmd.CommandText = "INSERT INTO nyheder (nyhed_tekst, nyhed_overskrift, nyhed_dato, fk_bruger_id) VALUES (@tekst, @overskrift, @dato, @bruger_id)";
                conn.Open();
                indsatte_rækker = cmd.ExecuteNonQuery();
                conn.Close();

                if (indsatte_rækker > 0)
                {
                    Label_Besked.Text = "Oprettelse lykkedes.";
                }
                else
                {
                    Label_Besked.Text = "Oprettelsen fejlede. Prøv igen eller kontakt webmaster";
                }
                break;
        }
        SelectNyheder(cmd);
    }
}