using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class HoldAdmin : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT *, (SELECT COUNT(*) FROM teams_and_customers WHERE fk_team_id = team_id) AS antal FROM teams INNER JOIN instructors ON fk_instructor_id = instructor_id INNER JOIN styles ON fk_style_id = style_id INNER JOIN agegroups ON fk_agegroup_id = agegroup_id INNER JOIN levels ON fk_level_id = level_id";
        if (Request.QueryString["column"] != null && Request.QueryString["direction"] != null)
        {
            string column = Request.QueryString["column"];
            string direction = Request.QueryString["direction"];
            Repeater_List.DataSource = db.SelectAllFromOrderByComplex(cmd, column, direction);
            Repeater_List.DataBind();
        }
        else
        {
            Repeater_List.DataSource = db.SelectTable(cmd);
            Repeater_List.DataBind();
        }

        if (!IsPostBack)
        {
            DropDownList_Hold_Aldersgrupper.DataSource = db.SelectAllFrom("agegroups");
            DropDownList_Hold_Aldersgrupper.DataBind();
            DropDownList_Hold_Aldersgrupper.Items.Insert(0, new ListItem("Vælg aldersgruppe"));


            DropDownList_Hold_Instruktoerer.DataSource = db.SelectAllFrom("instructors");
            DropDownList_Hold_Instruktoerer.DataBind();
            DropDownList_Hold_Instruktoerer.Items.Insert(0, new ListItem("Vælg instruktør"));

            DropDownList_Hold_Stilarter.DataSource = db.SelectAllFrom("styles");
            DropDownList_Hold_Stilarter.DataBind();
            DropDownList_Hold_Stilarter.Items.Insert(0, new ListItem("Vælg stilart"));

            DropDownList_Hold_Niveauer.DataSource = db.SelectAllFrom("levels");
            DropDownList_Hold_Niveauer.DataBind();
            DropDownList_Hold_Niveauer.Items.Insert(0, new ListItem("Vælg niveau"));
        }

        if (Request.QueryString["ret"] != null)
        {
            if (!IsPostBack)
            {
                cmd.CommandText = "SELECT * FROM teams WHERE team_id = @id";
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    TextBox_Hold_Name.Text = reader["team_number"].ToString();
                    TextBox_Hold_Price.Text = reader["team_price"].ToString();
                    DropDownList_Hold_Aldersgrupper.SelectedValue = reader["fk_agegroup_id"].ToString();
                    DropDownList_Hold_Instruktoerer.SelectedValue = reader["fk_instructor_id"].ToString();
                    DropDownList_Hold_Niveauer.SelectedValue = reader["fk_level_id"].ToString();
                    DropDownList_Hold_Stilarter.SelectedValue = reader["fk_style_id"].ToString();
                }
                conn.Close();
            }

            Repeater_List.Visible = false;
            Panel_Hold_Create.Visible = true;
        }
        if (Request.QueryString["opret"] != null)
        {
            Repeater_List.Visible = false;
            Panel_Hold_Create.Visible = true;
        }

        if (Request.QueryString["slet"] != null)
        {
            if (Request.QueryString["id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                db.DeleteFromTable("teams", "team_id", id);
                db.DeleteFromTable("teams_and_customers", "fk_team_id", id);
                Response.Redirect("HoldAdmin.aspx");
            }
        }
        if (Request.QueryString["oversigt"] != null)
        {
            Repeater_List.Visible = false;
            Repeater_View.Visible = true;
            Repeater_View.DataSource = db.SelectAllFromByParameter("teams", "team_id", Convert.ToInt16(Request.QueryString["id"]));
            Repeater_View.DataBind();
        }
        if (Request.QueryString["afmeld"] != null)
        {
            db.DeleteFromTable("teams_and_customers", "teams_and_customers_id", Convert.ToInt16(Request.QueryString["kunde_id"]));
            Helpers.Return();
        }
    }
    protected void Button_Hold_Create_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.Parameters.AddWithValue("@team_number", TextBox_Hold_Name.Text);
        decimal pris = decimal.Parse(TextBox_Hold_Price.Text);
        cmd.Parameters.AddWithValue("@team_price", pris);
        cmd.Parameters.AddWithValue("@fk_instructor_id", DropDownList_Hold_Instruktoerer.SelectedValue);
        cmd.Parameters.AddWithValue("@fk_style_id", DropDownList_Hold_Stilarter.SelectedValue);
        cmd.Parameters.AddWithValue("@fk_level_id", DropDownList_Hold_Niveauer.SelectedValue);
        cmd.Parameters.AddWithValue("@fk_agegroup_id", DropDownList_Hold_Aldersgrupper.SelectedValue);


        if (Request.QueryString["ret"] != null)
        {
            if (Request.QueryString["id"] != null)
            {
                cmd.CommandText = "UPDATE teams SET team_number = @team_number, team_price = @team_price, fk_instructor_id = @fk_instructor_id, fk_style_id = @fk_style_id, fk_level_id = @fk_level_id, fk_agegroup_id = @fk_agegroup_id WHERE team_id = @id";
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                db.UpdateTable(cmd);
            }
        }
        else
        {
            cmd.CommandText = "INSERT INTO teams (team_number, team_price, fk_instructor_id, fk_style_id, fk_level_id, fk_agegroup_id) VALUES(@team_number, @team_price, @fk_instructor_id, @fk_style_id, @fk_level_id, @fk_agegroup_id)";
            db.InsertIntoTable(cmd);
        }
        Response.Redirect("HoldAdmin.aspx");
    }
    protected void Repeater_View_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // det er kun Item og AlternatingItem der skal håndteres
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            Repeater nested = e.Item.FindControl("Repeater_nested") as Repeater;

            SqlCommand cmd = new SqlCommand("SELECT * FROM teams INNER JOIN teams_and_customers ON fk_team_id = team_id INNER JOIN customers ON fk_customer_id = customer_id INNER JOIN users ON fk_user_id = user_id WHERE team_id = @id", conn);
            cmd.Parameters.AddWithValue("@id", row["team_id"]);
            conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            conn.Close();
        }
    }
}