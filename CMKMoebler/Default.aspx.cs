using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;

        cmd.CommandText = "SELECT TOP 3 * FROM nyheder LEFT OUTER JOIN brugere ON fk_bruger_id = bruger_id ORDER BY nyhed_dato DESC";

        conn.Open();
        Repeater_Forside.DataSource = cmd.ExecuteReader();
        Repeater_Forside.DataBind();
        conn.Close();
    }
    public static string Truncate(string text)
    {
        string stripped_text = StripHTML(text);

        if (stripped_text.Length > 180)
        {
            //string substring = text.Substring(0, 181);

            //if (substring.EndsWith("<br ") || substring.EndsWith("</p ") || substring.EndsWith("</strong ") || substring.EndsWith("</em ") || substring.EndsWith("</s ") || substring.EndsWith("</blockquote "))
            //{
            //    return text.Substring(0, 180) + ">";
            //}
            //else
            //{
            //    return text.Substring(0, 180);
            //}

            return stripped_text.Substring(0, 180) + "...";
        }
        return stripped_text + "...";
    }

    public static string StripHTML(string input)
    {
        return Regex.Replace(input, "<.*?>", String.Empty);
    }
}