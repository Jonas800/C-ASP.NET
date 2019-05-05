using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Ordrer : System.Web.UI.Page
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

                    cmd.CommandText = "SELECT * FROM ordrer INNER JOIN status ON fk_status_id = status_id INNER JOIN kunder ON fk_kunde_id = kunde_id ORDER BY fk_status_id, ordre_datetime";

                    conn.Open();
                    Repeater_Ordrer.DataSource = cmd.ExecuteReader();
                    Repeater_Ordrer.DataBind();
                    conn.Close();
                }
            }
        }
    }
    protected void Repeater_Ordrer_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        // det er kun Item og AlternatingItem der skal håndteres
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DbDataRecord row = e.Item.DataItem as DbDataRecord;
            Repeater nested = e.Item.FindControl("Repeater_Ordrer_Linjer") as Repeater;
            // vi har brug for en ny connection, da den udenomliggende connecion stadig er åben... 
            SqlConnection nested_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

            SqlCommand cmd = new SqlCommand("SELECT * FROM ordre_linjer INNER JOIN produkter ON fk_produkt_id = produkt_id WHERE fk_ordre_id = @ordre_id", nested_conn);
            cmd.Parameters.AddWithValue("@ordre_id", row["ordre_id"]);
            nested_conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            nested_conn.Close();

        }
    }
}