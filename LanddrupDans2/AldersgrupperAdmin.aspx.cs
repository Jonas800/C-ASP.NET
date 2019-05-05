using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class AldersgrupperAdmin : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["column"] != null && Request.QueryString["direction"] != null)
        {
            string column = Request.QueryString["column"];
            string direction = Request.QueryString["direction"];
            Repeater_List.DataSource = db.SelectAllFromOrderBy("agegroups", column, direction);
            Repeater_List.DataBind();
        }
        else
        {
            Repeater_List.DataSource = db.SelectAllFrom("agegroups");
            Repeater_List.DataBind();
        }

        if (Request.QueryString["ret"] != null)
        {
            Repeater_List.Visible = false;
        }
        if (Request.QueryString["opret"] != null)
        {
            Repeater_List.Visible = false;
        }

        if (Request.QueryString["slet"] != null)
        {
            if (Request.QueryString["id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                db.DeleteFromTable("agegroups", "agegroup_id", id);
                Response.Redirect("AldersgrupperAdmin.aspx");
            }
        }
    }
    //protected void Button_Aldersgruppe_Create_Click(object sender, EventArgs e)
    //{
    //    SqlCommand cmd = new SqlCommand();
    //    cmd.Connection = conn;
    //    cmd.Parameters.AddWithValue("@agegroup_description", TextBox_Aldersgruppe_Description.Text);
    //    cmd.Parameters.AddWithValue("@agegroup_name", TextBox_Aldersgruppe_Name.Text);


    //    if (Request.QueryString["ret"] != null)
    //    {
    //        if (Request.QueryString["id"] != null)
    //        {
    //            cmd.CommandText = "UPDATE agegroups SET agegroup_name = @agegroup_name, agegroup_description = @agegroup_description WHERE agegroup_id = @id";
    //            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
    //            db.UpdateTable(cmd);

    //            Response.Redirect("AldersgrupperAdmin.aspx");
    //        }
    //    }
    //    else
    //    {
    //        cmd.CommandText = "INSERT INTO agegroups (agegroup_name, agegroup_description) VALUES(@agegroup_name, @agegroup_description)";
    //        db.InsertIntoTable(cmd);

    //        Response.Redirect("AldersgrupperAdmin.aspx");
    //    }
    //}
}