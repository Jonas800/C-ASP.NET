using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Priser : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;
            if (bruger.Rolle == 1)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT *, (SELECT pris_pris = pris_pris * (100 - pris_tilbud) / 100) AS pris_nu FROM priser";

                conn.Open();
                Repeater_Priser.DataSource = cmd.ExecuteReader();
                Repeater_Priser.DataBind();
                conn.Close();

                if (Request.QueryString["id"] != null)
                {
                    int pris_id = 0;
                    if (int.TryParse(Request.QueryString["id"], out pris_id))
                    {
                        if (!IsPostBack)
                        {
                            cmd.Parameters.AddWithValue("@pris_id", pris_id);
                            switch (Request.QueryString["action"])
                            {
                                case "ret":
                                    cmd.CommandText = "SELECT * FROM priser WHERE pris_id = @pris_id";
                                    conn.Open();
                                    SqlDataReader reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        TextBox_Beskrivelse.Text = reader["pris_tekst"].ToString();
                                        TextBox_Overskrift.Text = reader["pris_overskrift"].ToString();
                                        TextBox_Pris.Text = reader["pris_pris"].ToString();
                                        TextBox_Tilbud.Text = reader["pris_tilbud"].ToString();
                                    }
                                    conn.Close();
                                    break;
                                case "slet":

                                    cmd.CommandText = "DELETE FROM priser WHERE pris_id = @pris_id";

                                    conn.Open();
                                    int slettede_rækker = cmd.ExecuteNonQuery();
                                    conn.Close();

                                    if (slettede_rækker > 0)
                                    {
                                        Session["besked"] = "Sletning lykkedes.";
                                    }
                                    else
                                    {
                                        Session["besked"] = "Sletning fejlede. Prøv igen eller kontakt webmaster.";
                                    }
                                    Response.Redirect("Priser.aspx");
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("Priser.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
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
        cmd.Parameters.AddWithValue("@overskrift", TextBox_Overskrift.Text);
        cmd.Parameters.AddWithValue("@tekst", TextBox_Beskrivelse.Text);
        cmd.Parameters.AddWithValue("@pris", Convert.ToDecimal(TextBox_Pris.Text));

        if (Request.QueryString["id"] != null && Request.QueryString["action"] == "ret")
        {
            int rettede_rækker = 0;
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

            if (TextBox_Tilbud.Text != String.Empty)
            {
                cmd.Parameters.AddWithValue("@tilbud", TextBox_Tilbud.Text);
                cmd.CommandText = "UPDATE priser SET pris_overskrift = @overskrift, pris_tekst = @tekst, pris_pris = @pris, pris_tilbud = @tilbud WHERE pris_id = @id";
                conn.Open();
                rettede_rækker = cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                cmd.CommandText = "UPDATE priser SET pris_overskrift = @overskrift, pris_tekst = @tekst, pris_pris = @pris WHERE pris_id = @id";
                conn.Open();
                rettede_rækker = cmd.ExecuteNonQuery();
                conn.Close();
            }
            if (rettede_rækker > 0)
            {
                Session["besked"] = "Rettelse lykkedes.";
                Response.Redirect("Priser.aspx");
            }
            else
            {
                Session["besked"] = "Rettelse fejlede. Prøv igen eller kontakt webmaster.";
                Response.Redirect("Priser.aspx");
            }
        }
        else
        {
            int oprettede_rækker = 0;

            if (TextBox_Tilbud.Text != String.Empty)
            {
                cmd.Parameters.AddWithValue("@tilbud", TextBox_Tilbud.Text);
                cmd.CommandText = "INSERT INTO priser (pris_overskrift, pris_tekst, pris_pris, pris_tilbud) VALUES (@overskrift, @tekst, @pris, @tilbud)";

                conn.Open();
                oprettede_rækker = cmd.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                cmd.CommandText = "INSERT INTO priser (pris_overskrift, pris_tekst, pris_pris) VALUES (@overskrift, @tekst, @pris)";

                conn.Open();
                oprettede_rækker = cmd.ExecuteNonQuery();
                conn.Close();
            }
            if (oprettede_rækker > 0)
            {
                Session["besked"] = "Oprettelse lykkedes.";
                Response.Redirect("Priser.aspx");
            }
            else
            {
                Session["besked"] = "Oprettelse fejlede. Prøv igen eller kontakt webmaster.";
                Response.Redirect("Priser.aspx");
            }
        }
    }
}