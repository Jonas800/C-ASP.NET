using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MinSide : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;
            if (bruger.Rolle == 3)
            {
                if (!IsPostBack)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT YEAR(hold_tidspunkt) AS aar, MONTH(hold_tidspunkt) AS maaned FROM hold WHERE hold_tidspunkt <= DATEADD(MONTH, 1, GETDATE()) OR hold_tidspunkt = DATEADD(MONTH, 0, GETDATE()) GROUP BY YEAR(hold_tidspunkt), MONTH(hold_tidspunkt)  ORDER BY YEAR(hold_tidspunkt) DESC, MONTH(hold_tidspunkt) DESC";
                    conn.Open();
                    Repeater_Måneder.DataSource = cmd.ExecuteReader();
                    Repeater_Måneder.DataBind();
                    conn.Close();

                    if (Request.QueryString["month"] != null && Request.QueryString["year"] != null)
                    {
                        cmd.CommandText = "SELECT *, (SELECT COUNT (fk_hold_id) FROM hold_brugere WHERE fk_hold_id = hold_id) as tilmeldte FROM hold INNER JOIN brugere ON hold.fk_bruger_id = bruger_id INNER JOIN aktiviteter ON fk_aktivitet_id = aktivitet_id WHERE YEAR(hold_tidspunkt) = @year AND MONTH(hold_tidspunkt) = @month ORDER BY hold_tidspunkt DESC";
                        cmd.Parameters.AddWithValue("@month", Request.QueryString["month"]);
                        cmd.Parameters.AddWithValue("@year", Request.QueryString["year"]);
                        conn.Open();
                        Repeater_Hold.DataSource = cmd.ExecuteReader();
                        Repeater_Hold.DataBind();
                        conn.Close();
                    }
                }
            }
            else
            {
                Response.Redirect("BlivMedlem.aspx");
            }
        }
        else
        {
            Response.Redirect("BlivMedlem.aspx");
        }


    }

    public static string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }
    protected void Repeater_Hold_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        Bruger bruger = Session["bruger"] as Bruger;
        int hold_id = 0;

        int.TryParse((e.Item.FindControl("HiddenField_ID") as HiddenField).Value, out hold_id);
        if (e.CommandName == "Tilmeld")
        {
            cmd.CommandText = "INSERT INTO hold_brugere (fk_hold_id, fk_bruger_id) VALUES (@hold_id, @bruger_id)";
            cmd.Parameters.AddWithValue("@hold_id", hold_id);
            cmd.Parameters.AddWithValue("@bruger_id", bruger.ID);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Response.Redirect(Request.RawUrl);
        }
        else if (e.CommandName == "Frameld")
        {
            cmd.CommandText = "DELETE FROM hold_brugere WHERE fk_hold_id = @hold_id AND fk_bruger_id = @bruger_id";
            cmd.Parameters.AddWithValue("@hold_id", hold_id);
            cmd.Parameters.AddWithValue("@bruger_id", bruger.ID);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            Response.Redirect(Request.RawUrl);
        }
    }
    protected void Repeater_Hold_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    DbDataRecord row = e.Item.DataItem as DbDataRecord;
        //    Repeater nested = e.Item.FindControl("Repeater_nested") as Repeater;
        //    // vi har brug for en ny connection, da den udenomliggende connecion stadig er åben... 
        //    SqlConnection nested_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connectionstring"].ToString());

        //    SqlCommand cmd = new SqlCommand("SELECT * FROM billeder WHERE fk_kategori_id = @kategori_id", nested_conn);
        //    cmd.Parameters.AddWithValue("@kategori_id", row["kategori_id"]);
        //    nested_conn.Open();
        //    //find antal
        //    nested_conn.Close();

        //}
    }

    public string LavKnap(int id)
    {
        SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn2;

        Bruger bruger = Session["bruger"] as Bruger;

        cmd.Parameters.AddWithValue("@hold_id", id);
        cmd.Parameters.AddWithValue("@bruger_id", bruger.ID);

        cmd.CommandText = "SELECT hold_brugere_id, hold_tidspunkt FROM hold_brugere INNER JOIN hold ON fk_hold_id = hold_id WHERE hold_brugere.fk_bruger_id = @bruger_id AND fk_hold_id = @hold_id";
        conn2.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            DateTime tidspunkt = Convert.ToDateTime(reader["hold_tidspunkt"]);
            conn2.Close();
            double tilbage = tidspunkt.Subtract(DateTime.Now).TotalHours;

            
            if (tilbage > 4)
            {
                return "Frameld";
            }
            else
            {
                return "Kan ikke frameldes";
            }
        }
        else
        {
            conn2.Close();
            cmd.CommandText = "SELECT hold_max_antal, hold_tidspunkt, (SELECT COUNT (fk_hold_id) FROM hold_brugere WHERE fk_hold_id = hold_id) as tilmeldte FROM hold WHERE hold_id = @hold_id";

            conn2.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int tilmeldte = Convert.ToInt32(reader["tilmeldte"]);
                int max = Convert.ToInt32(reader["hold_max_antal"]);
                DateTime tidspunkt = Convert.ToDateTime(reader["hold_tidspunkt"]);

                double tilbage = tidspunkt.Subtract(DateTime.UtcNow).TotalHours;

                if (tilbage > 0)
                {
                    if (tilmeldte < max)
                    {
                        return "Tilmeld";
                    }
                    else
                    {
                        conn2.Close();
                        return "Optaget";
                    }
                }
                else
                {
                    return "Overstået";
                }
            }
            else
            {
                conn2.Close();
                return "";
            }
        }


    }
}