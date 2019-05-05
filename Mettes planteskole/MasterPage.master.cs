using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        //Binder kontaktoplysninger til repeater
        cmd.CommandText = "SELECT * FROM kontaktoplysninger";
        conn.Open();
        Repeater_Footer.DataSource = cmd.ExecuteReader();
        Repeater_Footer.DataBind();
        conn.Close();

        //Binder sponsorer til repeater
        cmd.CommandText = "SELECT * FROM sponsorer";
        conn.Open();
        Repeater_Sponsorer.DataSource = cmd.ExecuteReader();
        Repeater_Sponsorer.DataBind();
        conn.Close();

        //Binder de tre mest populære elementer til repeater
        cmd.CommandText = "SELECT TOP 3 *,(SELECT COUNT(*) FROM ordre_linjer WHERE fk_produkt_id = produkt_id) as popular FROM produkter ORDER BY popular DESC";
        conn.Open();
        Repeater_Populære.DataSource = cmd.ExecuteReader();
        Repeater_Populære.DataBind();
        conn.Close();

        

    }
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
    /// <summary>
    /// Highlighter (i NavBaren) siden man er inde på, inklusiv en underside
    /// </summary>
    /// <param name="site"></param>
    /// <param name="bonus"></param>
    /// <returns></returns>
    public string NavHighlight(string site, string subsite)
    {
        string url = Request.RawUrl;

        if (url.Contains(site) || url.Contains(subsite))
        {
            return "NavHighlight";
        }

        return "";
    }
}
