using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Point : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //holdvisning
        //links til tilmeldte
        //giv tilmeldte point

        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;
            if (bruger.Rolle == 1 || bruger.Rolle == 2)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (Request.QueryString["hold_id"] == null)
                {

                    if (bruger.Rolle == 1)
                    {
                        Panel_Ryd_Point.Visible = true;
                    }

                    cmd.CommandText = "SELECT *, (SELECT COUNT (fk_hold_id) FROM hold_brugere WHERE fk_hold_id = hold_id) as total FROM hold INNER JOIN aktiviteter ON fk_aktivitet_id = aktivitet_id INNER JOIN brugere ON fk_bruger_id = bruger_id ORDER BY hold_tidspunkt ASC, aktivitet_navn ASC, bruger_navn ASC";

                    conn.Open();
                    Repeater_Hold.DataSource = cmd.ExecuteReader();
                    Repeater_Hold.DataBind();
                    conn.Close();
                }
                else
                {
                    int hold_id = 0;
                    if (int.TryParse(Request.QueryString["hold_id"], out hold_id))
                    {
                        if (hold_id > 0)
                        {
                            if (!IsPostBack)
                            {
                                Button_Point.Visible = true;

                                cmd.Parameters.AddWithValue("@hold_id", hold_id);
                                cmd.CommandText = "SELECT * FROM hold_brugere INNER JOIN brugere ON hold_brugere.fk_bruger_id = bruger_id INNER JOIN hold ON fk_hold_id = hold_id INNER JOIN medlemmer ON medlemmer.fk_bruger_id = bruger_id WHERE hold_id = @hold_id";

                                conn.Open();
                                Repeater_Tilmeldte.DataSource = cmd.ExecuteReader();
                                Repeater_Tilmeldte.DataBind();
                                conn.Close();
                            }
                        }
                        else
                        {
                            Response.Redirect("Point.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("Point.aspx");
                    }
                }
            }
        }
    }
    protected void Button_Point_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        int i = 0;
        foreach (RepeaterItem item in Repeater_Tilmeldte.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                string id = (item.FindControl("HiddenField_HoldBrugereID") as HiddenField).Value;
                bool check = (item.FindControl("CheckBox_Tilstede") as CheckBox).Checked;
                cmd.Parameters.AddWithValue("@godkendt" + i, check);
                cmd.Parameters.AddWithValue("@id" + i, id);

                int point = 0;
                if (check)
                {
                    point = Convert.ToInt32((item.FindControl("HiddenField_Point") as HiddenField).Value);
                }
                cmd.Parameters.AddWithValue("@point" + i, point);

                cmd.CommandText = "UPDATE hold_brugere SET hold_brugere_godkendt = @godkendt" + i + ", hold_brugere_point = @point" + i + " WHERE hold_brugere_id = @id" + i;

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                i++;
            }
        }
        Response.Redirect(Request.RawUrl);
    }
    protected void Button_Ryd_Point_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "UPDATE hold_brugere SET hold_brugere_point = 0";

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        Session["besked"] = "Ryddede alle point.";

        Response.Redirect("Point.aspx");
    }
}