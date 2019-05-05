using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Profil : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        //Tjekker om brugeren er på
        if (Session["bruger"] != null)
        {
            //Opretter Bruger objekt via Session
            Bruger bruger = (Bruger)Session["bruger"];

            //Sørger for at forudfyldning ikke genindlæses ved postback 
            if (!IsPostBack)
            {
                //Forudfylder siden
                TextBox_Navn.Text = bruger.Navn;
                TextBox_Email.Text = bruger.Email;

                //Finder alle genrer
                CheckBoxList_Genre.DataSource = db.SelectAllFrom("genrer");
                CheckBoxList_Genre.DataBind();

                //Tjekker om bruger.Genre er tom
                if (bruger.Genre != null)
                {
                    //Tjekker checkboxlistens checkboxes ved alle fundne genrer i bruger objektet 
                    foreach (var item in bruger.Genre)
                    {
                        ListItem li = CheckBoxList_Genre.Items.FindByText(item.Navn);

                        if (li != null)
                        {
                            li.Selected = true;
                        }
                    }
                }

                //Tjekker om nyhedsbrev er tjekket eller ej og sætter den tilhørende boolske værdi
                if (bruger.Nyhedsbrev == true)
                {
                    CheckBox_Nyhedsbrev.Checked = true;
                }
                else
                {
                    CheckBox_Nyhedsbrev.Checked = false;
                }
            }
            //Sletter objekt i databasen
            if (Request.QueryString["slet"] != null)
            {
                bruger.SletObjekt();
                Response.Redirect("Login.aspx");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        //Opretter Bruger objektet via session og fylder det med nye informationer der hvor behovet er
        Bruger bruger = (Bruger)Session["bruger"];

        bruger.Navn = TextBox_Navn.Text;
        bruger.Email = TextBox_Email.Text;
        bruger.Kodeord = TextBox_Kodeord.Text;

        //Tjekker om nyhedsbrev er tjekket eller ej og sætter den tilhørende boolske værdi til objektet
        if (CheckBox_Nyhedsbrev.Checked)
        {
            bruger.Nyhedsbrev = true;
        }
        else
        {
            bruger.Nyhedsbrev = false;
        }

        //while (CheckBoxList_Genre.SelectedIndex > 0)
        //{
        //    bruger.Genre.Add(new Genre(CheckBoxList_Genre.SelectedValue.ToString()));
        //}

        //Opdaterer objektet i databasen med de nye informationer
        bruger.UpdateObjekt();

        List<Genre> genre = new List<Genre>();

        //Tilføjer genrer til bruger.Genre ved hver tjekkede checkbox
        foreach (ListItem item in CheckBoxList_Genre.Items)
        {
            if (item.Selected)
            {
                genre.Add(new Genre(item.Text, Convert.ToInt32(item.Value)));
            }
        }
        bruger.Genre = genre;

        //Sletter alle tilhørende genrer for derefter at ligge nye ind. Update kan ikke bruges da man jo ligger ændrer på en masse rækker, tilføjer nye og fjerner andre. Ellers ville man kun kunne ændre på de få man valgte til at starte med.
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "DELETE FROM brugere_genrer WHERE fk_bruger_id = @bruger_id; ";
        cmd.Parameters.AddWithValue("@bruger_id", bruger.ID);
        //Denne foreach tilføjer en CommandText hver gang den finder en værdi i listen genre, som vi lavede tidligere.
        foreach (Genre item in genre)
        {
            cmd.CommandText += "INSERT INTO brugere_genrer (fk_bruger_id, fk_genre_id) VALUES(@bruger_id" + item.ID.ToString() + ", @genre_id" + item.ID.ToString() + "); ";
            cmd.Parameters.AddWithValue("@genre_id" + item.ID.ToString(), item.ID);
            cmd.Parameters.AddWithValue("@bruger_id" + item.ID.ToString(), bruger.ID);
        }
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();

        Session["bruger"] = bruger;

        Response.Redirect("Profil.aspx");
    }
}