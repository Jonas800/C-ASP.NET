using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Aldersgruppe_Form : System.Web.UI.UserControl
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ret"] != null)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt16(Request.QueryString["id"]);
                DataRow reader = db.SelectSingleRowFrom("agegroups", "agegroup_id", id);

                if (reader.ItemArray.Length > 0)
                {
                    TextBox_Aldersgruppe_Name.Text = reader["agegroup_name"].ToString();
                    TextBox_Aldersgruppe_Description.Text = reader["agegroup_description"].ToString();
                }
            }

            Panel_Aldersgruppe_Create.Visible = true;
        }
        if (Request.QueryString["opret"] != null)
        {
            Panel_Aldersgruppe_Create.Visible = true;
        }
    }
    protected void Button_Aldersgruppe_Create_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.Parameters.AddWithValue("@agegroup_description", TextBox_Aldersgruppe_Description.Text);
        cmd.Parameters.AddWithValue("@agegroup_name", TextBox_Aldersgruppe_Name.Text);


        if (Request.QueryString["ret"] != null)
        {
            if (Request.QueryString["id"] != null)
            {
                cmd.CommandText = "UPDATE agegroups SET agegroup_name = @agegroup_name, agegroup_description = @agegroup_description WHERE agegroup_id = @id";
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                db.UpdateTable(cmd);

                Response.Redirect("AldersgrupperAdmin.aspx");
            }
        }
        else
        {
            cmd.CommandText = "INSERT INTO agegroups (agegroup_name, agegroup_description) VALUES(@agegroup_name, @agegroup_description)";
            db.InsertIntoTable(cmd);

            Response.Redirect("AldersgrupperAdmin.aspx");
        }
    }
}