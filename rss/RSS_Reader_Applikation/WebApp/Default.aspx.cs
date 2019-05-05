using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Xml.XPath;

public partial class WebApp_Default : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.PopulatePage();
        }
    }
    private void PopulatePage()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT TOP 1 * FROM(SELECT *, LAG(feed_titel) OVER (ORDER BY feed_id) AS PreviousValue, LEAD(feed_titel) OVER (ORDER BY feed_id) AS NextValue FROM feeds) M", conn);

        //SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM feeds WHERE feed_titel = @pagename", conn);
        

        DataTable feeds = new DataTable();
        ad.Fill(feeds);

        Repeater_Feeds.DataSource = feeds;
        Repeater_Feeds.DataBind();
    }
    protected void Repeater_Feeds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;
        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
        {
            XmlDataSource feed = new XmlDataSource();
            DataRowView drv = (DataRowView)item.DataItem;

            feed.DataFile = drv.Row.ItemArray[2].ToString();

            Repeater Channels = (Repeater)item.FindControl("Repeater_Channels");
            feed.XPath = "rss/channel";

            Channels.DataSource = feed;
            Channels.DataBind();
        }
    }
    protected void Repeater_Channels_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;
        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater items = (Repeater)item.FindControl("Repeater_Item");

            XPathNavigator data = ((IXPathNavigable)item.DataItem).CreateNavigator();

            items.DataSource = data.Select("./item");
            items.DataBind();
        }
    }
}