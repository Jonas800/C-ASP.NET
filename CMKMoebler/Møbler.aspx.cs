using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Møbler : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        if (!IsPostBack)
        {
            cmd.CommandText = "SELECT * FROM serier";
            conn.Open();
            CheckBoxList_Serie.DataSource = cmd.ExecuteReader();
            CheckBoxList_Serie.DataBind();
            conn.Close();

            cmd.CommandText = "SELECT * FROM designere";
            conn.Open();
            DropDownList_Designer.DataSource = cmd.ExecuteReader();
            DropDownList_Designer.DataBind();
            conn.Close();
            DropDownList_Designer.Items.Insert(0, "Alle");

            TextBox_År_Max.Text = DateTime.Now.AddYears(5).ToString("yyyy");
        }
    }
    protected void Button_Varenummer_Click(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@varenummer", "%" + TextBox_Varenummer.Text + "%");

        cmd.CommandText = "SELECT produkt_id, produkt_navn, produkt_beskrivelse FROM produkter WHERE produkt_varenummer LIKE @varenummer";
        conn.Open();
        if (cmd.ExecuteReader().Read())
        {
            conn.Close();
            Repeater_Produkter.Visible = true;
            Panel_Søgning.Visible = false;
            conn.Open();
            Repeater_Produkter.DataSource = cmd.ExecuteReader();
            Repeater_Produkter.DataBind();
            conn.Close();
        }
        else
        {
            conn.Close();
            Panel_Fejl.Visible = true;
        }


    }
    protected void Button_Udvidet_Søgning_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "SELECT produkt_id, produkt_navn, produkt_beskrivelse FROM produkter INNER JOIN designere ON fk_designer_id = designer_id INNER JOIN serier ON fk_serie_id = serie_id WHERE 1=1 ";

        //Checkbox search
        int checkbox_selected = 0;
        foreach (ListItem li in CheckBoxList_Serie.Items)
        {
            if (li.Selected)
            {
                checkbox_selected++;
            }
        }

        if (checkbox_selected > 0)
        {
            cmd.CommandText += " AND (";

            int i = 0;
            foreach (ListItem cb in CheckBoxList_Serie.Items)
            {
                if (cb.Selected)
                {
                    cmd.CommandText += " fk_serie_id = @serie_id" + i.ToString() + " OR ";
                    cmd.Parameters.AddWithValue("@serie_id" + i.ToString(), cb.Value);
                    i++;
                }
            }
            if (i > 0)
            {
                cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 3);
            }
            cmd.CommandText += ") ";
        }

        //DDL
        if (DropDownList_Designer.SelectedValue != "Alle")
        {
            cmd.CommandText += " AND fk_designer_id = @designer_id ";
            cmd.Parameters.AddWithValue("@designer_id", DropDownList_Designer.SelectedValue);
        }

        //år
        int år_min = 0;
        if (int.TryParse(TextBox_År_Min.Text, out år_min))
        {
            if (år_min >= 0)
            {
                cmd.CommandText += " AND produkt_aar >= @produkt_aar_min ";
                cmd.Parameters.AddWithValue("@produkt_aar_min", år_min);
            }
        }

        int år_max = 0;
        if (int.TryParse(TextBox_År_Max.Text, out år_max))
        {
            if (år_max >= 0)
            {
                cmd.CommandText += " AND produkt_aar < @produkt_aar_max ";
                cmd.Parameters.AddWithValue("@produkt_aar_max", TextBox_År_Max.Text);
            }
        }

        //Pris
        int pris_min = 0;
        if (int.TryParse(TextBox_Pris_Min.Text, out pris_min))
        {
            if (pris_min >= 0)
            {
                cmd.CommandText += " AND produkt_pris >= @produkt_pris_min ";
                cmd.Parameters.AddWithValue("@produkt_pris_min", pris_min);
            }
        }

        int pris_max = 0;
        if (int.TryParse(TextBox_Pris_Max.Text, out pris_max))
        {
            if (pris_max >= 0)
            {
                cmd.CommandText += " AND produkt_pris < @produkt_pris_max ";
                cmd.Parameters.AddWithValue("@produkt_pris_max", TextBox_Pris_Max.Text);
            }
        }


        conn.Open();
        if (cmd.ExecuteReader().Read())
        {
            conn.Close();
            Repeater_Produkter.Visible = true;
            Panel_Søgning.Visible = false;
            conn.Open();
            Repeater_Produkter.DataSource = cmd.ExecuteReader();
            Repeater_Produkter.DataBind();
            conn.Close();
        }
        else
        {
            conn.Close();
            Panel_Fejl.Visible = true;
        }
    }
    protected void Repeater_Produkter_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // det er kun Item og AlternatingItem der skal håndteres
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DbDataRecord row = e.Item.DataItem as DbDataRecord;
            Repeater nested = e.Item.FindControl("Repeater_nested") as Repeater;
            // vi har brug for en ny connection, da den udenomliggende connecion stadig er åben... 
            SqlConnection nested_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connectionstring"].ToString());

            SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM billeder WHERE fk_produkt_id = @produkt_id ORDER BY billede_prioritet DESC", nested_conn);
            cmd.Parameters.AddWithValue("@produkt_id", row["produkt_id"]);
            nested_conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            nested_conn.Close();
        }
    }

    public static string Truncate(string text)
    {
        if (text.Length > 180)
        {
            string substring = text.Substring(0, 181);

            if (substring.EndsWith("<br ") || substring.EndsWith("</p ") || substring.EndsWith("</strong ") || substring.EndsWith("</em ") || substring.EndsWith("</s ") || substring.EndsWith("</blockquote "))
            {
                return text.Substring(0, 180) + ">";
            }
            else
            {
                return text.Substring(0, 180);
            }
        }
        return text;
    }
}