using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session.Abandon();
        Spil spil;
        if (Session["spil"] == null)
        {
            spil = new Spil();
            spil.Credit = 100;

            Session["spil"] = spil;
        }
        else
        {
            spil = Session["spil"] as Spil;
        }
        if (!IsPostBack)
        {
            Label_Bets.Text = "Pokéballs tilbage: " + spil.Credit.ToString();
            if (spil.Credit < 0)
            {
                Label_End.Visible = true;
                Label_End.Font.Size = 30;
                Button_Spin.Visible = false;
            }
        }

        Image1.ImageUrl = spil.symboler[0].Billede.ToString();
        Image2.ImageUrl = spil.symboler[0].Billede.ToString();
        Image3.ImageUrl = spil.symboler[0].Billede.ToString();


        if (Request.QueryString["session"] != null)
        {
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
    protected void Button_Spin_Click(object sender, EventArgs e)
    {
        Spil spil;
        spil = Session["spil"] as Spil;
        spil.SpilNu();


        Image1.ImageUrl = spil.resultat[0].Billede.ToString();
        Image2.ImageUrl = spil.resultat[1].Billede.ToString();
        Image3.ImageUrl = spil.resultat[2].Billede.ToString();


        if (spil.Credit <= 0)
        {
            if (IsPostBack)
            {
                Label_End.Visible = true;
                Button_Spin.Visible = false;
                Panel_Kill.Visible = true;
            }
        }

        if (spil.gevinst)
        {
            Label_Gevinst.Visible = true;
            Label_Gevinst.Text = "DU HAR VUNDET " + spil.resultat[0].Gevinst + " POKÉBALLS!";

        }
        else
        {
            Label_Gevinst.Visible = false;
        }

        Label_Bets.Text = "Pokéballs tilbage: " + spil.Credit.ToString();

        Session["spil"] = spil;
    }

}