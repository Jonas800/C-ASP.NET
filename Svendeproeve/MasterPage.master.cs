using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class MasterPage : System.Web.UI.MasterPage
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "SELECT DISTINCT TOP 5 bruger_navn, (SELECT SUM(hold_brugere_point) FROM hold_brugere WHERE fk_bruger_id = bruger_id AND hold_brugere_godkendt = 1) as total_point FROM hold_brugere INNER JOIN brugere ON fk_bruger_id = bruger_id WHERE fk_rolle_id = 3 ORDER BY total_point DESC";

        conn.Open();
        Repeater_Top5.DataSource = cmd.ExecuteReader();
        Repeater_Top5.DataBind();
        conn.Close();

        cmd.CommandText = "SELECT oplysning_åbningstid FROM oplysninger";
        conn.Open();
        Repeater_Åbningstider.DataSource = cmd.ExecuteReader();
        Repeater_Åbningstider.DataBind();
        conn.Close();

        cmd.CommandText = "SELECT pris_overskrift, (SELECT pris_pris = pris_pris * (100 - pris_tilbud) / 100) AS pris_nu FROM priser WHERE pris_tilbud IS NOT NULL";
        conn.Open();
        Repeater_Tilbud.DataSource = cmd.ExecuteReader();
        Repeater_Tilbud.DataBind();
        conn.Close();

        cmd.CommandText = "SELECT oplysning_footer FROM oplysninger";
        conn.Open();
        string footer = cmd.ExecuteScalar().ToString();
        conn.Close();
        Label_Footer.Text = footer;

        Brødkrumme();

        if (Session["bruger"] != null)
        {
            Panel_Login.Visible = false;
            Panel_Logud.Visible = true;

            if (Request.QueryString["action"] == "logud")
            {
                Session.Abandon();
                Response.Redirect("Default.aspx");
            }

            Bruger bruger = Session["bruger"] as Bruger;
            if (bruger.Rolle == 3)
            {
                cmd.CommandText = "SELECT DISTINCT bruger_navn, (SELECT SUM(hold_brugere_point) FROM hold_brugere WHERE fk_bruger_id = bruger_id AND hold_brugere_godkendt = 1) as total_point FROM hold_brugere INNER JOIN brugere ON fk_bruger_id = bruger_id WHERE fk_rolle_id = 3 AND bruger_id = @bruger_id";
                cmd.Parameters.AddWithValue("@bruger_id", bruger.ID);

                conn.Open();
                Repeater_Mine_Point.DataSource = cmd.ExecuteReader();
                Repeater_Mine_Point.DataBind();
                conn.Close();

                Panel_Mine_Point.Visible = true;
            }
            else if (bruger.Rolle == 1)
            {
                Literal_Admin.Visible = true;
            }
        }

        if (Request.QueryString["action"] == "login")
        {
            Panel_Login_Form.Visible = true;
        }

        if (Request.RawUrl.Contains("Kontakt.aspx") || Request.RawUrl.Contains("BlivMedlem.aspx"))
        {
            Panel_Aside.Visible = false;
            if (Request.RawUrl.Contains("Kontakt.aspx"))
            {
                Panel_Kontakt.Visible = true;
            }
            else if (Request.RawUrl.Contains("BlivMedlem"))
            {
                Panel_Bliv_Medlem.Visible = true;
            }
        }
        if (Session["besked"] != null)
        {
            Panel_Besked.Visible = true;
            Panel_Besked2.Visible = true;
            Label_Besked.Text = Session["besked"].ToString();
            Label_Besked2.Text = Session["besked"].ToString();

            Session.Remove("besked");
        }
    }

    private void Brødkrumme()
    {
        string brødkrumme = "";
        brødkrumme = HttpContext.Current.Request.RawUrl.ToLower();
        int index = brødkrumme.LastIndexOf("?");
        if (index > 0)
        {
            if (Request.RawUrl != "/")
            {
                if (Request.RawUrl.Contains("BlivMedlem"))
                {
                    brødkrumme = "Bliv medlem";
                    Label_Brødkrumme.Text = brødkrumme;
                    Literal_Brødkrumme_Title.Text += brødkrumme;
                }
                else if (Request.RawUrl.Contains("MinSide"))
                {
                    brødkrumme = "Min side";
                    Label_Brødkrumme.Text = brødkrumme;
                    Literal_Brødkrumme_Title.Text += brødkrumme;
                }
                else
                {
                    brødkrumme = Request.RawUrl;
                    brødkrumme = brødkrumme.Substring(0, index);
                    brødkrumme = brødkrumme.Replace(".aspx", "").Replace("/", "").Replace("Default", "Velkommen til motionscentret");
                    Label_Brødkrumme.Text = brødkrumme;
                    Literal_Brødkrumme_Title.Text += brødkrumme;
                }
            }
            else
            {
                brødkrumme = brødkrumme.Substring(0, index);
                brødkrumme = "Velkommen til motionscentret";
                Label_Brødkrumme.Text = brødkrumme;
                Literal_Brødkrumme_Title.Text += brødkrumme;
            }
        }
        else
        {
            if (Request.RawUrl != "/")
            {
                if (Request.RawUrl.Contains("BlivMedlem"))
                {
                    brødkrumme = "Bliv medlem";
                    Label_Brødkrumme.Text = brødkrumme;
                    Literal_Brødkrumme_Title.Text += brødkrumme;
                }
                else if (Request.RawUrl.Contains("MinSide"))
                {
                    brødkrumme = "Min side";
                    Label_Brødkrumme.Text = brødkrumme;
                    Literal_Brødkrumme_Title.Text += brødkrumme;
                }
                else
                {
                    brødkrumme = Request.RawUrl;
                    brødkrumme = brødkrumme.Replace(".aspx", "").Replace("/", "").Replace("Default", "Velkommen til motionscentret");
                    Label_Brødkrumme.Text = brødkrumme;
                    Literal_Brødkrumme_Title.Text += brødkrumme;
                }
            }

            else
            {
                brødkrumme = "Velkommen til motionscentret";
                Label_Brødkrumme.Text = brødkrumme;
                Literal_Brødkrumme_Title.Text += brødkrumme;
            }
        }
    }
    //protected void Repeater_Møbel_Aside_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    // det er kun Item og AlternatingItem der skal håndteres
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        DbDataRecord row = e.Item.DataItem as DbDataRecord;
    //        Repeater nested = e.Item.FindControl("Repeater_nested") as Repeater;
    //        // vi har brug for en ny connection, da den udenomliggende connecion stadig er åben... 
    //        SqlConnection nested_conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Connectionstring"].ToString());

    //        SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM billeder WHERE fk_produkt_id = @produkt_id ORDER BY billede_prioritet DESC", nested_conn);
    //        cmd.Parameters.AddWithValue("@produkt_id", row["produkt_id"]);
    //        nested_conn.Open();
    //        nested.DataSource = cmd.ExecuteReader();
    //        nested.DataBind();
    //        nested_conn.Close();

    //    }
    //}
    /// <summary>
    /// Highlighter (i NavBaren) siden man er inde på
    /// </summary>
    /// <param name="site"></param>
    /// <returns></returns>
    public string NavHighlight(string site)
    {
        string url = Request.RawUrl;

        if (url.Contains(site))
        {
            return "NavHighlight";
        }

        return "";
    }
    public string NavHighlightWithEmpty(string site)
    {
        string url = Request.RawUrl;

        if (url.Contains(site) || url == "/")
        {
            return "NavHighlight";
        }

        return "";
    }

    public string SiteReferalOnRole()
    {
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;

            if (bruger.Rolle == 1 || bruger.Rolle == 2)
            {
                return "Point.aspx";
            }
            else
            {
                return "MinSide.aspx";
            }
        }
        else
        {
            return "BlivMedlem.aspx";
        }
    }
    public string SiteNameOnRole()
    {
        if (Session["bruger"] != null)
        {
            Bruger bruger = Session["bruger"] as Bruger;

            if (bruger.Rolle == 1 || bruger.Rolle == 2)
            {
                return "POINT";
            }
            else
            {
                return "MIN SIDE";
            }
        }
        else
        {
            return "BLIV MEDLEM";
        }
    }
    protected void Button_Send_Click(object sender, EventArgs e)
    {
        //Send mail

        //Henter admins email og navn ud af databasen
        SqlCommand cmd = new SqlCommand("SELECT oplysning_email FROM oplysninger", conn);

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            //Mail instillinger
            SmtpClient smtp = new SmtpClient("localhost", 25);
            smtp.UseDefaultCredentials = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //Mail objekt og værdier (emne og besked)
            MailMessage mail = new MailMessage();
            mail.Subject = TextBox_Navn.Text;
            mail.Body = TextBox_Kommentar.Text + "\r\n\r Adresse: " + TextBox_Adresse.Text;
            //mail.IsBodyHtml = true;
            //Fra og til værdier
            mail.From = new MailAddress(TextBox_Email.Text, TextBox_Navn.Text);
            mail.To.Add(new MailAddress(reader["oplysning_email"].ToString(), "Motionscentret"));

            //Sender og sletter data efter den er sendt afsted
            smtp.Send(mail);
            smtp.Dispose();

            Panel_Besked.Visible = true;
            Label_Besked.Text = "Tak for din henvendelse. Du vil høre fra os hurtigst muligt.";
        }
        else
        {
            Panel_Besked.Visible = true;
            Label_Besked.Text = "Noget er gået galt med vores email, vi beklager.";
        }
        conn.Close();
    }
    protected void Button_Gem_Click(object sender, EventArgs e)
    {
        if (CheckBox_Regler.Checked)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            Bruger nyBruger = new Bruger();

            int bruger_id = nyBruger.OpretBrugerReturnID(TextBox_Opret_Email.Text, TextBox_Kodeord.Text, TextBox_Opret_Navn.Text, "3");

            if (bruger_id > 0)
            {
                int er_medlem_oprettet = 0;
                cmd.CommandText = "INSERT INTO medlemmer (medlem_adresse, fk_bruger_id) VALUES (@adresse, @bruger_id)";
                cmd.Parameters.AddWithValue("@adresse", TextBox_Opret_Adresse.Text);
                cmd.Parameters.AddWithValue("@bruger_id", bruger_id);

                conn.Open();
                er_medlem_oprettet = cmd.ExecuteNonQuery();
                conn.Close();

                if (er_medlem_oprettet > 0)
                {
                    Session["besked"] = "Tillykke, du er nu oprettet!";
                }
                else
                {
                    Session["besked"] = "Oprettelse fejlede. Prøv med en anden email eller kontakt os.";
                    cmd.CommandText = "DELETE FROM brugere WHERE bruger_id = @bruger_id";
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                Response.Redirect("BlivMedlem.aspx");

            }
            else
            {
                Session["besked"] = "Oprettelse fejlede. Prøv med en anden email eller kontakt os.";
                Response.Redirect("BlivMedlem.aspx");
            }
        }
        else
        {
            CheckBox_Regler.Text = "Klik accepter for at fortsætte.";
        }
    }
}
