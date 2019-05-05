using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class StilarterHold : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT * FROM styles WHERE style_id = @id";
        cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
        Repeater_Hold.DataSource = db.SelectTable(cmd);
        Repeater_Hold.DataBind();

        Repeater_Stilarter.DataSource = db.SelectAllFrom("styles");
        Repeater_Stilarter.DataBind();

        if (Request.QueryString["join"] != null)
        {
            if (Session["user_id"] == null)
            {
                Response.Redirect("Tilmeld.aspx");
            }
            else if (Request.QueryString["id"] != null)
            {
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@user_id", Convert.ToInt32(Session["user_id"]));
                cmd.CommandText = "SELECT customer_id FROM customers WHERE fk_user_id = @user_id";
                conn.Open();
                object customer_id = cmd.ExecuteScalar();
                conn.Close();
                cmd.Parameters.AddWithValue("@fk_customer_id", customer_id);
                cmd.Parameters.AddWithValue("@fk_team_id", Request.QueryString["id"]);
                cmd.CommandText = "SELECT * FROM teams_and_customers WHERE fk_customer_id = @fk_customer_id AND fk_team_id = @fk_team_id";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Label_Message.Text = "Du har allerede tilmeldt dig dette hold.";
                }
                else
                {
                    cmd.CommandText = "INSERT INTO teams_and_customers (fk_customer_id, fk_team_id) VALUES(@fk_customer_id, @fk_team_id)";
                    db.InsertIntoTable(cmd);
                    Label_Message.Text = "Du er nu tilmeldt et hold.";
                }
                conn.Close();
            }
        }
    }
    protected void Repeater_Hold_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // det er kun Item og AlternatingItem der skal håndteres
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            Repeater nested = e.Item.FindControl("Repeater_nested") as Repeater;

            SqlCommand cmd = new SqlCommand("SELECT * FROM teams INNER JOIN instructors ON fk_instructor_id = instructor_id INNER JOIN styles ON fk_style_id = style_id INNER JOIN agegroups ON fk_agegroup_id = agegroup_id INNER JOIN levels ON fk_level_id = level_id WHERE style_id = @id", conn);
            cmd.Parameters.AddWithValue("@id", row["style_id"]);
            conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            conn.Close();
        }
    }
}