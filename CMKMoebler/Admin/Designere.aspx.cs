using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Designere : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            SelectDesignere(cmd);

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
                        cmd.CommandText = "SELECT designer_navn FROM designere WHERE designer_id = @id";
                        conn.Open();
                        string designer_navn = cmd.ExecuteScalar().ToString();
                        conn.Close();

                        TextBox_Designere.Text = designer_navn;
                    }
                    break;
                case "slet":
                    cmd.CommandText = "SELECT * FROM produkter WHERE fk_designer_id = @id";

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        conn.Close();
                        Label_Besked.Text = "Slet eller ændre produkter med denne designer først.";
                    }
                    else
                    {
                        conn.Close();
                        int slettede_rækker = 0;
                        cmd.CommandText = "DELETE FROM designere WHERE designer_id = @id";
                        conn.Open();
                        slettede_rækker = cmd.ExecuteNonQuery();
                        conn.Close();
                        if (slettede_rækker > 0)
                        {
                            Session["besked"] = "Sletning lykkedes.";
                            Response.Redirect("Designere.aspx");
                        }
                        else
                        {
                            Label_Besked.Text = "Sletning fejlede. Prøv igen eller kontakt webmaster.";
                        }
                    }
                    SelectDesignere(cmd);
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
    private void SelectDesignere(SqlCommand cmd)
    {
        cmd.CommandText = "SELECT * FROM designere ORDER BY designer_navn ASC";
        conn.Open();
        Repeater_Designere.DataSource = cmd.ExecuteReader();
        Repeater_Designere.DataBind();
        conn.Close();
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.Parameters.AddWithValue("@navn", TextBox_Designere.Text);

        switch (Request.QueryString["action"])
        {
            case "ret":
                int opdaterede_rækker = 0;
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                cmd.CommandText = "UPDATE designere SET designer_navn = @navn WHERE designer_id = @id";
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
                cmd.CommandText = "INSERT INTO designere (designer_navn) VALUES (@navn)";
                conn.Open();
                indsatte_rækker = cmd.ExecuteNonQuery();
                conn.Close();

                if (indsatte_rækker > 0)
                {
                    Label_Besked.Text = "Oprettelse lykkedes.";
                }
                else
                {
                    Label_Besked.Text = "Oprettelse fejlede. Prøv igen eller kontakt webmaster";
                }
                break;
        }
        SelectDesignere(cmd);
    }
}