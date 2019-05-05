using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using ImageResizer;

/// <summary>
/// Summary description for Helpers
/// </summary>
public static class Helpers
{
    public static string Afkort(string text)
    {
        if (text.Length > 10)
        {
            return text.Substring(0, 10) + "...";
        }
        else
        {
            return text;
        }
    }
    public static int Hent_int_fra_url(string querystring)
    {
        if (HttpContext.Current.Request.QueryString[querystring] != null)
        {
            int id = 0;
            if (int.TryParse(HttpContext.Current.Request.QueryString[querystring], out id))
            {
                return id;
            }
        }
        return 0;
    }
    public static void DeletePictures(object file_name)
    {
        if (file_name != DBNull.Value)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("~/billeder/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/billeder/") + file_name.ToString());
            }
            if (File.Exists(HttpContext.Current.Server.MapPath("~/billeder/thumbs/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/billeder/thumbs/") + file_name.ToString());
            }
            if (File.Exists(HttpContext.Current.Server.MapPath("~/billeder/resized/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/billeder/resized/") + file_name.ToString());
            }
        }
    }
    private static void Resizing(string NewFileName)
    {

        //skalering

        //thumbnail cropped
        ResizeSettings settings = new ResizeSettings();
        settings.Height = 175;
        settings.Width = 175;
        settings.Mode = FitMode.Crop;
        settings.Anchor = System.Drawing.ContentAlignment.MiddleCenter;
        settings.Scale = ScaleMode.Both;

        ImageBuilder.Current.Build("~/billeder/" + NewFileName, "~/billeder/thumbs/" + NewFileName, settings);

        //til detajler
        settings = new ResizeSettings();
        settings.Height = 300;
        settings.Scale = ScaleMode.Both;

        ImageBuilder.Current.Build("~/billeder/" + NewFileName, "~/billeder/resized/" + NewFileName, settings);
    }
    public static void Return()
    {
        if (HttpContext.Current.Request.UrlReferrer != null)
        {
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.UrlReferrer.ToString());
        }
        else
        {
            HttpContext.Current.Response.Redirect("Default.aspx");
        }
    }
}