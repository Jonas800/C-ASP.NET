using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //DataTable dt = new DataTable();
        //SqlDataAdapter da = new SqlDataAdapter("SELECT TOP 1 * FROM film ORDER BY NEWID()", conn);
        //da.Fill(dt);
        //Repeater_Forside.DataSource = dt;
        //Repeater_Forside.DataBind();

        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "SELECT TOP 1 film_id, film_navn, film_beskrivelse, ISNULL((SELECT CAST(SUM(rating_tal) as decimal(10,2)) / COUNT(rating_tal) FROM rating WHERE fk_film_id = film_id), 0) as vurderinger FROM film ORDER BY newid()";
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dt);

        Repeater_Forside.DataSource = dt;
        Repeater_Forside.DataBind();
    }
    protected void Repeater_Forside_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Nested repeater der tilknytter billeder til film i repeater i pageload
        // det er kun Item og AlternatingItem der skal håndteres
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            Repeater nested = e.Item.FindControl("Repeater_nested") as Repeater;

            SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM billeder INNER JOIN film ON fk_film_id = film_id WHERE film_id = @id ORDER BY billede_prioritet ASC", conn);
            cmd.Parameters.AddWithValue("@id", row["film_id"]);
            conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            conn.Close();
        }
        // det er kun Item og AlternatingItem der skal håndteres
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            Repeater nested = e.Item.FindControl("Repeater_Genrer") as Repeater;

            SqlCommand cmd = new SqlCommand("SELECT * FROM film INNER JOIN film_genrer ON film_genrer.fk_film_id = film_id INNER JOIN genrer ON fk_genre_id = genre_id WHERE film_id = @id", conn);
            cmd.Parameters.AddWithValue("@id", row["film_id"]);
            conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            conn.Close();
        }
    }
}