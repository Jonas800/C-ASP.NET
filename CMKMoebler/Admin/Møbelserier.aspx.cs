using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Møbelserier : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            SelectSerier(cmd);

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
                        cmd.CommandText = "SELECT serie_navn FROM serier WHERE serie_id = @id";
                        conn.Open();
                        string serie_navn = cmd.ExecuteScalar().ToString();
                        conn.Close();

                        TextBox_Navn.Text = serie_navn;
                    }
                    break;
                case "slet":
                    cmd.CommandText = "SELECT * FROM produkter WHERE fk_serie_id = @id";

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        conn.Close();
                        Label_Besked.Text = "Slet eller ændre produkter med denne møbelserie først";
                    }
                    else
                    {
                        conn.Close();
                        int slettede_rækker = 0;
                        cmd.CommandText = "DELETE FROM serier WHERE serie_id = @id";
                        conn.Open();
                        slettede_rækker = cmd.ExecuteNonQuery();
                        conn.Close();
                        if (slettede_rækker > 0)
                        {
                            Session["besked"] = "Serier blev slettet.";
                            Response.Redirect("Møbelserier.aspx");
                        }
                        else
                        {
                            Label_Besked.Text = "Serier blev ikke slettet. Prøv igen.";
                        }
                    }
                    SelectSerier(cmd);
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
    private void SelectSerier(SqlCommand cmd)
    {
        cmd.CommandText = "SELECT * FROM serier ORDER BY serie_navn ASC";
        conn.Open();
        Repeater_Serier.DataSource = cmd.ExecuteReader();
        Repeater_Serier.DataBind();
        conn.Close();
    }

    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.Parameters.AddWithValue("@navn", TextBox_Navn.Text);

        switch (Request.QueryString["action"])
        {
            case "ret":
                int opdaterede_rækker = 0;
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                cmd.CommandText = "UPDATE serier SET serie_navn = @navn WHERE serie_id = @id";
                conn.Open();
                opdaterede_rækker = cmd.ExecuteNonQuery();
                conn.Close();

                if (opdaterede_rækker > 0)
                {
                    Label_Besked.Text = "Rettelse lykkedes.";
                }
                else
                {
                    Label_Besked.Text = "Rettelse fejlede. Prøv igen.";
                }

                break;
            default:
                int indsatte_rækker = 0;
                cmd.CommandText = "INSERT INTO serier (serie_navn) VALUES (@navn)";
                conn.Open();
                indsatte_rækker = cmd.ExecuteNonQuery();
                conn.Close();

                if (indsatte_rækker > 0)
                {
                    Label_Besked.Text = "Møbelserie er nu oprettet.";
                }
                else
                {
                    Label_Besked.Text = "Oprettelse af møbelserie fejlede.";
                }
                break;
        }
        SelectSerier(cmd);
    }
}