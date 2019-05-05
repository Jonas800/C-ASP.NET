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

                    //Udfylder dropdownliste til dyrkningstider
                    cmd.CommandText = "SELECT * FROM dyrkningstider";
                    conn.Open();
                    DropDownList_Dyrkningstider.DataSource = cmd.ExecuteReader();
                    DropDownList_Dyrkningstider.DataBind();
                    conn.Close();

                    //Udfylder dropdownliste til jordtyper
                    cmd.CommandText = "SELECT * FROM jordtyper";
                    conn.Open();
                    DropDownList_Jordtyper.DataSource = cmd.ExecuteReader();
                    DropDownList_Jordtyper.DataBind();
                    conn.Close();

                    //Udfylder dropdownlsite til kategorier
                    cmd.CommandText = "SELECT * FROM kategorier";
                    conn.Open();
                    DropDownList_Kategorier.DataSource = cmd.ExecuteReader();
                    DropDownList_Kategorier.DataBind();
                    conn.Close();

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
                            TextBox_Lager_Max.Text = reader["produkt_lager_max"].ToString();
                            TextBox_Lager_Min.Text = reader["produkt_lager_min"].ToString();
                            TextBox_Lager_Stand.Text = reader["produkt_lager_stand"].ToString();
                            TextBox_Navn.Text = reader["produkt_navn"].ToString();
                            TextBox_Pris.Text = reader["produkt_pris"].ToString();
                            TextBox_Varenummer.Text = reader["produkt_varenummer"].ToString();
                            CheckBox_Er_Aktiv.Checked = Convert.ToBoolean(reader["produkt_er_aktiv"]);
                            DropDownList_Dyrkningstider.SelectedValue = reader["fk_dyrkningstid_id"].ToString();
                            DropDownList_Jordtyper.SelectedValue = reader["fk_jordtype_id"].ToString();
                            DropDownList_Kategorier.SelectedValue = reader["fk_kategori_id"].ToString();
                        }
                    }
                }
            }
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        //ALT FOR MANGE PARAMETER
        cmd.Parameters.AddWithValue("@navn", TextBox_Navn.Text);
        cmd.Parameters.AddWithValue("@pris", Convert.ToDecimal(TextBox_Pris.Text));
        cmd.Parameters.AddWithValue("@beskrivelse", TextBox_Beskrivelse.Text);
        cmd.Parameters.AddWithValue("@lager_max", TextBox_Lager_Max.Text);
        cmd.Parameters.AddWithValue("@lager_min", TextBox_Lager_Min.Text);
        cmd.Parameters.AddWithValue("@lager_stand", TextBox_Lager_Stand.Text);
        cmd.Parameters.AddWithValue("@varenummer", TextBox_Varenummer.Text);
        cmd.Parameters.AddWithValue("@kategori_id", DropDownList_Kategorier.SelectedValue);
        cmd.Parameters.AddWithValue("@dyrkningstid_id", DropDownList_Dyrkningstider.SelectedValue);
        cmd.Parameters.AddWithValue("@jordtype_id", DropDownList_Jordtyper.SelectedValue);

        //Gør det muligt at sætte et produkt til inaktivt
        bool er_aktiv = false;
        if (CheckBox_Er_Aktiv.Checked)
        {
            er_aktiv = true;
        }

        if (Request.QueryString["id"] != null)
        {
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            //Ret/Update statement
            cmd.CommandText = "UPDATE produkter SET produkt_navn = @navn, produkt_pris = @pris, produkt_beskrivelse = @beskrivelse, produkt_lager_max = @lager_max, produkt_lager_min = @lager_min, produkt_lager_stand = @lager_stand, produkt_varenummer = @varenummer, fk_kategori_id = @kategori_id, fk_dyrkningstid_id = @dyrkningstid_id, fk_jordtype_id = @jordtype_id, produkt_er_aktiv = @er_aktiv WHERE produkt_id = @id";
            cmd.Parameters.AddWithValue("@er_aktiv", CheckBox_Er_Aktiv.Checked);

        }
        else
        {
            //Alt for lang insert statement til at oprette produkter
            cmd.CommandText = "INSERT INTO produkter (produkt_navn, produkt_pris, produkt_beskrivelse, produkt_lager_max, produkt_lager_min, produkt_lager_stand, produkt_varenummer, fk_kategori_id, fk_dyrkningstid_id, fk_jordtype_id, produkt_er_aktiv) VALUES (@navn, @pris, @beskrivelse, @lager_max, @lager_min, @lager_stand, @varenummer, @kategori_id, @dyrkningstid_id, @jordtype_id, @er_aktiv)";
            cmd.Parameters.AddWithValue("@er_aktiv", er_aktiv);
        }

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("Produkter.aspx");
    }
}