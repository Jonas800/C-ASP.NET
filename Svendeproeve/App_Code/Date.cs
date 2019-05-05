using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

/// <summary>
/// Summary description for Date
/// </summary>
public class Date
{
    public Date()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Propogate a dropdownlist with dates
    /// </summary>
    /// <param name="ddl_day"></param>
    /// <param name="ddl_month"></param>
    /// <param name="ddl_year"></param>
    public static void PopulateDate(DropDownList ddl_day, DropDownList ddl_month, DropDownList ddl_year)
    {

        for (int i = 1; i <= 31; i++)
        {
            ddl_day.Items.Add(i.ToString());
        }

        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
        for (int i = 1; i < 13; i++)
        {
            ddl_month.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));
        }

        for (int i = System.DateTime.Now.Year; i >= 1900; i--)
        {
            ddl_year.Items.Add(i.ToString());
        }
        ddl_year.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
    }
    public static void PopulateTime(DropDownList ddl_hours, DropDownList ddl_minutes)
    {
        for (int i = 1; i <= 23; i++)
        {
            ddl_hours.Items.Add(i.ToString());
        }

        for (int i = 0; i <= 59; i = i + 10)
        {
            ddl_minutes.Items.Add(i.ToString());
        }
    }
}