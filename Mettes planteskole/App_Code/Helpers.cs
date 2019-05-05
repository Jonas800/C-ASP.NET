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
    /// <summary>
    /// Afkorter et tekststykke
    /// </summary>
    /// <param name="text"></param>
    /// <param name="længde"></param>
    /// <returns></returns>
    public static string Afkort(string text, int længde)
    {
        if (text.Length > 10)
        {
            return text.Substring(0, længde) + "...";
        }
        else
        {
            return text;
        }
    }
    /// <summary>
    /// Henter en int fra url
    /// </summary>
    /// <param name="querystring"></param>
    /// <returns></returns>
    public static int HentIntFraUrl(string querystring)
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
    /// Resizer og uploader et billede på en valgt placering med den bredde og højde der er valgt
    /// </summary>
    /// <param name="NewFileName"></param>
    /// <param name="placering"></param>
    /// <param name="bredde"></param>
    /// <param name="højde"></param>
    public static void Resizing(string NewFileName, string placering, int bredde, int højde)
    {
        //skalering

        //thumbnail filmliste
        ResizeSettings settings = new ResizeSettings();
        settings.Width = bredde;
        settings.Height = højde;
        settings.Scale = ScaleMode.Both;

        ImageBuilder.Current.Build("~/billeder/alle/" + NewFileName, "~/billeder/" + placering + "/" + NewFileName, settings);
    }
    /// <summary>
    /// Resizer og uploader et billede på en valgt placering med den bredde og højde der er valgt
    /// </summary>
    /// <param name="NewFileName"></param>
    /// <param name="placering"></param>
    /// <param name="bredde"></param>
    public static void Resizing(string NewFileName, string placering, int bredde)
    {
        //skalering

        //thumbnail filmliste
        ResizeSettings settings = new ResizeSettings();
        settings.Width = bredde;
        settings.Scale = ScaleMode.Both;

        ImageBuilder.Current.Build("~/billeder/alle/" + NewFileName, "~/billeder/" + placering + "/" + NewFileName, settings);
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
}