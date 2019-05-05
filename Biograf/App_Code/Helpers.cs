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
    /// <summary>
    /// Sletter filer på serveren
    /// </summary>
    /// <param name="file_name"></param>
    public static void DeletePictures(object file_name)
    {
        if (file_name != DBNull.Value)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("~/billeder/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/billeder/") + file_name.ToString());
            }
            if (File.Exists(HttpContext.Current.Server.MapPath("~/billeder/filmliste/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/billeder/filmliste/") + file_name.ToString());
            }
            if (File.Exists(HttpContext.Current.Server.MapPath("~/billeder/filmbeskrivelse/") + file_name.ToString()))
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/billeder/filmbeskrivelse/") + file_name.ToString());
            }
        }
    }
    /// <summary>
    /// Resizer og uploader billeder til serveren
    /// </summary>
    /// <param name="NewFileName"></param>
    public static void Resizing(string NewFileName)
    {

        //skalering

        //thumbnail filmliste
        ResizeSettings settings = new ResizeSettings();
        settings.Width = 130;
        settings.Scale = ScaleMode.Both;

        ImageBuilder.Current.Build("~/billeder/" + NewFileName, "~/billeder/filmbeskrivelse/" + NewFileName, settings);

        //til filmbeskrivelse
        settings = new ResizeSettings();
        settings.Width = 65;
        settings.Scale = ScaleMode.Both;

        ImageBuilder.Current.Build("~/billeder/" + NewFileName, "~/billeder/filmliste/" + NewFileName, settings);
    }
    /// <summary>
    /// Gå tilbage til tidligere url
    /// </summary>
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
    /// <summary>
    /// Resizer og uploader billeder til serveren
    /// </summary>
    /// <param name="NewFileName"></param>
    public static void ResizingTilbud(string NewFileName)
    {

        //skalering
        ResizeSettings settings = new ResizeSettings();
        settings.Width = 400;
        settings.Scale = ScaleMode.Both;

        ImageBuilder.Current.Build("~/billeder/" + NewFileName, "~/billeder/tilbud/" + NewFileName, settings);
    }
}