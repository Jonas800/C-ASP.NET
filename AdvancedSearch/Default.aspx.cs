using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        //forudfyldning
        if (!IsPostBack)
        {
            cmd.CommandText = "SELECT * FROM studios";
            conn.Open();
            DropDownList_Studio.DataSource = cmd.ExecuteReader();
            DropDownList_Studio.DataBind();
            conn.Close();
            DropDownList_Studio.Items.Insert(0, "Vælg");

            cmd.CommandText = "SELECT * FROM genrer";
            conn.Open();
            CheckBoxList_Genre.DataSource = cmd.ExecuteReader();
            CheckBoxList_Genre.DataBind();
            conn.Close();

            TextBox_År_Max.Text = DateTime.Now.AddYears(5).ToString("yyyy");
        }

        cmd.CommandText = "SELECT anime_id, anime_navn, studio_name, YEAR(anime_aar) AS aar FROM anime INNER JOIN studios ON fk_studio_id = studio_id";
        conn.Open();
        Repeater_Results.DataSource = cmd.ExecuteReader();
        Repeater_Results.DataBind();
        conn.Close();
    }
    protected void Button_Søg_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "SELECT DISTINCT anime_id, anime_navn, studio_name, YEAR (anime_aar) AS aar FROM anime INNER JOIN studios ON fk_studio_id = studio_id INNER JOIN anime_genrer ON fk_anime_id = anime_id INNER JOIN genrer ON fk_genre_id = genre_id WHERE 1=1 ";

        //Checkbox search
        int genres_selected = 0;
        foreach (ListItem li in CheckBoxList_Genre.Items)
        {
            if (li.Selected)
            {
                genres_selected++;
            }
        }

        if (genres_selected > 0)
        {
            cmd.CommandText += " AND (";

            int i = 0;
            foreach (ListItem cb in CheckBoxList_Genre.Items)
            {
                if (cb.Selected)
                {
                    cmd.CommandText += " fk_genre_id = @genre_id" + i.ToString() + " OR ";
                    cmd.Parameters.AddWithValue("@genre_id" + i.ToString(), cb.Value);
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
        if (DropDownList_Studio.SelectedValue != "Vælg")
        {
            cmd.CommandText += " AND fk_studio_id = @studio_id ";
            cmd.Parameters.AddWithValue("@studio_id", DropDownList_Studio.SelectedValue);
        }
        //fritekst
        if (TextBox_Fritekst.Text != "")
        {
            cmd.CommandText += " AND (";
            string tekst = TextBox_Fritekst.Text;
            string[] array = null;
            int count = 0;
            char[] split = { ' ' };
            array = tekst.Split(' ');

            for (count = 0; count <= array.Length - 1; count++)
            {
                cmd.CommandText += " studio_name LIKE @fritekst" + count + " OR anime_navn LIKE @fritekst" + count + " OR genre_navn LIKE @fritekst" + count + " OR ";
                cmd.Parameters.AddWithValue("@fritekst" + count, "%" + array[count] + "%");
            }
            if (count > 0)
            {
                cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 3);
            }
            cmd.CommandText += " ) ";

            //cmd.CommandText += " AND (studio_name LIKE @fritekst OR anime_navn LIKE @fritekst OR genre_navn LIKE @fritekst ) ";
            //cmd.Parameters.AddWithValue("@fritekst", "%" + TextBox_Fritekst.Text + "%");
        }

        //år
        if (Convert.ToInt32(TextBox_År_Min.Text) >= 0)
        {
            int int_år = Convert.ToInt32(TextBox_År_Min.Text);
            DateTime dt;
            DateTime.TryParseExact(int_år.ToString(), "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            cmd.CommandText += " AND anime_aar >= @anime_aar_min ";
            cmd.Parameters.AddWithValue("@anime_aar_min", dt);
        }
        if (Convert.ToInt32(TextBox_År_Max.Text) >= 0)
        {
            int int_år = Convert.ToInt32(TextBox_År_Max.Text);
            DateTime dt;
            DateTime.TryParseExact(int_år.ToString(), "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
            cmd.CommandText += " AND anime_aar < @anime_aar_max ";
            cmd.Parameters.AddWithValue("@anime_aar_max", TextBox_År_Max.Text);
        }

        conn.Open();

        if (cmd.ExecuteReader().Read())
        {
            conn.Close();
            conn.Open();
            Repeater_Results.DataSource = cmd.ExecuteReader();
            Repeater_Results.DataBind();
            conn.Close();
        }
        else
        {
            Label_Error.Text = "No hits. Try again.";
            Repeater_Results.DataSource = null;
            Repeater_Results.DataBind();
            conn.Close();

        }

    }
    protected void Repeater_Results_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // det er kun Item og AlternatingItem der skal håndteres
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DbDataRecord row = e.Item.DataItem as DbDataRecord;
            Repeater nested = e.Item.FindControl("Repeater_nested") as Repeater;
            // vi har brug for en ny connection, da den udenomliggende connecion stadig er åben... 
            SqlConnection nested_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connectionstring"].ToString());

            SqlCommand cmd = new SqlCommand("SELECT genre_navn FROM anime_genrer INNER JOIN genrer ON fk_genre_id = genre_id INNER JOIN anime ON fk_anime_id = anime_id WHERE anime_id = @anime_id", nested_conn);
            cmd.Parameters.AddWithValue("@anime_id", row["anime_id"]);
            nested_conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            nested_conn.Close();

        }
    }
}