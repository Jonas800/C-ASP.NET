using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_OpretProdukt : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            if (Session["besked"] != null)
            {
                Label_Besked.Text = Session["besked"].ToString();
                Session.Remove("besked");
            }

            if (!IsPostBack)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                //Udfylder dropdownliste til designere
                cmd.CommandText = "SELECT * FROM designere";
                conn.Open();
                DropDownList_Designer.DataSource = cmd.ExecuteReader();
                DropDownList_Designer.DataBind();
                conn.Close();
                DropDownList_Designer.Items.Insert(0, "Skal vælges");


                //Udfylder dropdownliste til serier
                cmd.CommandText = "SELECT * FROM serier";
                conn.Open();
                DropDownList_Serie.DataSource = cmd.ExecuteReader();
                DropDownList_Serie.DataBind();
                conn.Close();
                DropDownList_Serie.Items.Insert(0, "Skal vælges");

                if (Request.QueryString["id"] != null)
                {
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                    cmd.CommandText = "SELECT * FROM produkter WHERE produkt_id = @id";

                    //Forudfylder formularfelterne med det produkt vi vil rette
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        TextBox_Beskrivelse.Text = reader["produkt_beskrivelse"].ToString();
                        TextBox_Navn.Text = reader["produkt_navn"].ToString();
                        TextBox_Pris.Text = reader["produkt_pris"].ToString();
                        TextBox_Varenummer.Text = reader["produkt_varenummer"].ToString();
                        TextBox_År.Text = reader["produkt_aar"].ToString();
                        DropDownList_Designer.SelectedValue = reader["fk_designer_id"].ToString();
                        DropDownList_Serie.SelectedValue = reader["fk_serie_id"].ToString();
                    }
                }
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

        cmd.Parameters.AddWithValue("@navn", TextBox_Navn.Text);
        cmd.Parameters.AddWithValue("@pris", TextBox_Pris.Text);
        cmd.Parameters.AddWithValue("@beskrivelse", TextBox_Beskrivelse.Text);
        cmd.Parameters.AddWithValue("@varenummer", TextBox_Varenummer.Text);
        cmd.Parameters.AddWithValue("@aar", TextBox_År.Text);
        cmd.Parameters.AddWithValue("@designer_id", DropDownList_Designer.SelectedValue);
        cmd.Parameters.AddWithValue("@serie_id", DropDownList_Serie.SelectedValue);

        int berørte_rækker = 0;

        if (Request.QueryString["id"] != null)
        {
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            //Ret/Update statement
            cmd.CommandText = "UPDATE produkter SET produkt_navn = @navn, produkt_pris = @pris, produkt_beskrivelse = @beskrivelse, produkt_varenummer = @varenummer, fk_designer_id = @designer_id, fk_serie_id = @serie_id WHERE produkt_id = @id";
            conn.Open();
            berørte_rækker = cmd.ExecuteNonQuery();
            conn.Close();

            if (berørte_rækker > 0)
            {
                Session["besked"] = "Rettelse lykkedes.";
                Response.Redirect("Produkter.aspx");
            }
            else
            {
                Label_Besked.Text = "Rettelse fejlede. Prøv igen eller kontakt webmaster.";
            }
        }
        else
        {
            cmd.CommandText = "INSERT INTO produkter (produkt_navn, produkt_pris, produkt_beskrivelse, produkt_varenummer, produkt_aar, fk_designer_id, fk_serie_id) VALUES (@navn, @pris, @beskrivelse, @varenummer, @aar, @designer_id, @serie_id)";
            conn.Open();
            berørte_rækker = cmd.ExecuteNonQuery();
            conn.Close();

            if (berørte_rækker > 0)
            {
                Session["besked"] = "Oprettelse lykkedes. Opret endnu et eller gå tilbage til produkter.";
            }
            else
            {
                Label_Besked.Text = "Oprettelse fejlede. Prøv igen eller kontakt webmaster.";
            }
        }

    }
}