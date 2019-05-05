using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

public partial class AdminBilleder : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        //Opretter Billede objekt
        Billede billede = new Billede();
        switch (Request.QueryString["handling"])
        {
            case "ret":
                //Forudfylder siden med data fra databasen via objektet, der findes ud fra querystring
                if (!IsPostBack)
                {
                    billede.HentBillede(Convert.ToInt32(Request.QueryString["billede_id"]));
                    TextBox_Prioritet.Text = billede.Prioritet.ToString();
                    Image_Billede.ImageUrl = "/billeder/filmbeskrivelse/" + billede.Sti;
                }
                break;
            case "slet":
                //Sletter billedet ud fra querystring
                billede.HentBillede(Convert.ToInt32(Request.QueryString["billede_id"]));
                Helpers.DeletePictures(billede.Sti);

                billede.SletBillede(Convert.ToInt32(Request.QueryString["billede_id"]));
                Helpers.Return();
                break;
            default:
                break;
        }
        //Viser alle billeder der passer til den valgte film
        Repeater_Billede.DataSource = db.SelectAllFromByParameter("film", "film_id", Convert.ToInt32(Request.QueryString["id"]));
        Repeater_Billede.DataBind();
    }
    protected void Repeater_Billede_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Nested repeater der tilknytter billeder til film i repeater i pageload
        // det er kun Item og AlternatingItem der skal håndteres
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView row = e.Item.DataItem as DataRowView;
            Repeater nested = e.Item.FindControl("Repeater_nested") as Repeater;

            SqlCommand cmd = new SqlCommand("SELECT * FROM film INNER JOIN billeder ON fk_film_id = film_id WHERE film_id = @id ORDER BY billede_prioritet ASC", conn);
            cmd.Parameters.AddWithValue("@id", row["film_id"]);
            conn.Open();
            nested.DataSource = cmd.ExecuteReader();
            nested.DataBind();
            conn.Close();
        }
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        //Opretter Billede objekt og udfylder det med data, som så enten oprettes eller opdateres i databasen
        Billede billede = new Billede();
        string NewFileName = Guid.NewGuid() + Path.GetExtension(FileUpload_Billede.FileName);

        switch (Request.QueryString["handling"])
        {
            case "ret":
                if (FileUpload_Billede.HasFile)
                {
                    //Sletter gammelt billede på serveren
                    billede.HentBillede(Convert.ToInt32(Request.QueryString["billede_id"]));
                    Helpers.DeletePictures(billede.Sti);

                    //Giver billede nye informationer
                    FileUpload_Billede.SaveAs(Server.MapPath("~/billeder/") + NewFileName);
                    billede.Prioritet = Convert.ToInt32(TextBox_Prioritet.Text);
                    Helpers.Resizing(NewFileName);
                    billede.Sti = NewFileName;
                    billede.ID = Convert.ToInt32(Request.QueryString["billede_id"]);
                    billede.UpdateBilledeMedBillede();
                    Response.Redirect("AdminBilleder.aspx?id=" + Request.QueryString["id"]);
                }
                else
                {
                    billede.Prioritet = Convert.ToInt32(TextBox_Prioritet.Text);
                    billede.ID = Convert.ToInt32(Request.QueryString["billede_id"]);
                    billede.UpdateBilledeInfo();
                    Response.Redirect("AdminBilleder.aspx?id=" + Request.QueryString["id"]);
                }
                break;
            default:
                FileUpload_Billede.SaveAs(Server.MapPath("~/billeder/") + NewFileName);
                Helpers.Resizing(NewFileName);
                billede.Prioritet = Convert.ToInt32(TextBox_Prioritet.Text);
                billede.Film = Convert.ToInt32(Request.QueryString["id"]);
                billede.Prioritet = Convert.ToInt32(TextBox_Prioritet.Text);
                billede.Sti = NewFileName;
                billede.GemBillede();
                Helpers.Return();
                break;
        }
    }
}