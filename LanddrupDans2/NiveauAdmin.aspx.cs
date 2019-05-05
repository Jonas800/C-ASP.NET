using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class NiveauAdmin : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["column"] != null && Request.QueryString["direction"] != null)
        {
            string column = Request.QueryString["column"];
            string direction = Request.QueryString["direction"];
            Repeater_List.DataSource = db.SelectAllFromOrderBy("levels", column, direction);
            Repeater_List.DataBind();
        }
        else
        {
            Repeater_List.DataSource = db.SelectAllFrom("levels");
            Repeater_List.DataBind();
        }

        if (Request.QueryString["ret"] != null)
        {
            if (!IsPostBack)
            {
                int id = Convert.ToInt16(Request.QueryString["id"]);
                DataRow reader = db.SelectSingleRowFrom("levels", "level_id", id);

                if (reader.ItemArray.Length > 0)
                {
                    TextBox_Niveau_Name.Text = reader["level_name"].ToString();
                    TextBox_Niveau_Description.Text = reader["level_description"].ToString();
                }
            }

            Repeater_List.Visible = false;
            Panel_Niveau_Create.Visible = true;
        }
        if (Request.QueryString["opret"] != null)
        {
            Repeater_List.Visible = false;
            Panel_Niveau_Create.Visible = true;
        }

        if (Request.QueryString["slet"] != null)
        {
            if (Request.QueryString["id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                db.DeleteFromTable("levels", "level_id", id);
                Response.Redirect("NiveauAdmin.aspx");
            }
        }
    }
    protected void Button_Niveau_Create_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.Parameters.AddWithValue("@level_description", TextBox_Niveau_Description.Text);
        cmd.Parameters.AddWithValue("@level_name", TextBox_Niveau_Name.Text);


        if (Request.QueryString["ret"] != null)
        {
            if (Request.QueryString["id"] != null)
            {
                cmd.CommandText = "UPDATE levels SET level_name = @level_name, level_description = @level_description WHERE level_id = @id";
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                db.UpdateTable(cmd);

                Response.Redirect("NiveauAdmin.aspx");
            }
        }
        else
        {
            cmd.CommandText = "INSERT INTO levels (level_name, level_description) VALUES(@level_name, @level_description)";
            db.InsertIntoTable(cmd);

            Response.Redirect("NiveauAdmin.aspx");
        }
    }
}