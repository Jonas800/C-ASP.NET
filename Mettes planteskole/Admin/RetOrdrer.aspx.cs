using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_RetOrdrer : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        //Sessionbeskyttelse
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;

            if (bruger.Er_Admin)
            {
                if (!IsPostBack)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "SELECT * FROM status";

                    conn.Open();
                    DropDownList_Status.DataSource = cmd.ExecuteReader();
                    DropDownList_Status.DataBind();
                    conn.Close();

                    if (Request.QueryString["id"] != null)
                    {
                        cmd.CommandText = "SELECT * FROM ordrer WHERE ordre_id = @id";
                        cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            DropDownList_Status.SelectedValue = reader["fk_status_id"].ToString();
                        }
                    }
                }
            }
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "UPDATE ordrer SET fk_status_id = @status_id WHERE ordre_id = @id";

        cmd.Parameters.AddWithValue("@status_id", DropDownList_Status.SelectedValue);
        cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        Response.Redirect("Ordrer.aspx");
    }
}