using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        Repeater_Forside_Text.DataSource = db.SelectAllFrom("forside");
        Repeater_Forside_Text.DataBind();

        Repeater_Forside_Billede.DataSource = db.SelectAllFrom("forside");
        Repeater_Forside_Billede.DataBind();
    }
}