using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Åbningstider : System.Web.UI.Page
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

                    //Henter alle kategorier og binder dem til en repeater
                    cmd.CommandText = "SELECT * FROM åbningstider ORDER BY åbningstid_rækkefølge";
                    conn.Open();
                    Repeater_Åbningstider.DataSource = cmd.ExecuteReader();
                    Repeater_Åbningstider.DataBind();
                    conn.Close();


                    //Forudfylder formularen ved tryk på ret og sletter kolonnen i databasen ved slet, via et id fra url'en
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                    switch (Request.QueryString["action"])
                    {
                        case "ret":
                            cmd.CommandText = "SELECT * FROM åbningstider WHERE åbningstid_id = @id";

                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                TextBox_Tid.Text = reader["åbningstid_tid"].ToString();
                                TextBox_Dag.Text = reader["åbningstid_dag"].ToString();
                            }
                            conn.Close();
                            break;

                        case "slet":
                            cmd.CommandText = "DELETE FROM åbningstider WHERE åbningstid_id = @id";

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            Response.Redirect("åbningstider.aspx");
                            break;
                    }
                }
            }
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        //Opret og ret åbningstider
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.Parameters.AddWithValue("@dag", TextBox_Dag.Text);
        cmd.Parameters.AddWithValue("@tid", TextBox_Tid.Text);

        if (Request.QueryString["action"] == "ret")
        {
            cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
            cmd.CommandText = "UPDATE åbningstider SET åbningstid_tid = @tid, åbningstid_dag = @dag WHERE åbningstid_id = @id";
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        else
        {
            cmd.Parameters.AddWithValue("@rækkefølge", 7); //Default value, admin kan selv bestemme rækkefølgen bagefter
            cmd.CommandText = "INSERT INTO åbningstider (åbningstid_dag, åbningstid_tid, åbningstid_rækkefølge) VALUES (@dag, @tid, @rækkefølge)";
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        Response.Redirect("Åbningstider.aspx");

    }
    protected void Button_Ret_Click(object sender, EventArgs e)
    {
        //Finder controls frem i Repeateren og håndterer deres data
        foreach (RepeaterItem item in Repeater_Åbningstider.Items)
        {
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                int rækkefølge = 0;
                int id = 0;
                int.TryParse((item.FindControl("TextBox_Rækkefølge") as TextBox).Text, out rækkefølge);
                int.TryParse((item.FindControl("HiddenField_ID") as HiddenField).Value, out id);

                //Updaterer rækkefølgen åbningstider vises i repeateren og på hjemmmeside
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@rækkefølge", rækkefølge);
                cmd.CommandText = "UPDATE åbningstider SET åbningstid_rækkefølge = @rækkefølge WHERE åbningstid_id = @id";

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        Response.Redirect("Åbningstider.aspx");
    }
}