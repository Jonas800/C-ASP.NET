using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Kurv
/// </summary>
public static class Kurv
{
    /// <summary>
    /// Navnet på den session variabel der indeholder kurven
    /// </summary>
    private static string KurvensNavnISession = "YoloSwag420";

    /// <summary>
    /// Særger for at der findes en session variabel som kan holde på kurven
    /// </summary>
    public static void OpretKurven()
    {
        // session tjek - findes den ikke endnu, oprettes kurven
        if (HttpContext.Current.Session[KurvensNavnISession] == null)
        {
            HttpContext.Current.Session[KurvensNavnISession] = new List<Produkt>();
        }
    }
    public static List<Produkt> HentKurven()
    {
        OpretKurven();

        return HttpContext.Current.Session[KurvensNavnISession] as List<Produkt>;
    }
    public static void FjernKurven()
    {
        // session tjek - findes den, fjernes kurven
        if (HttpContext.Current.Session[KurvensNavnISession] != null)
        {
            HttpContext.Current.Session.Remove(KurvensNavnISession);
        }
    }
    public static void FjernEtProduktFraKurven(int id)
    {
        List<Produkt> kurv = Kurv.HentKurven();

        foreach (Produkt produkt in kurv)
        {
            if (produkt.Id == id)
            {
                kurv.Remove(produkt);

                break;
            }
        }
        Kurv.GemKurven(kurv);
    }
    public static void PutVareIKurv(int id, string navn, decimal pris, int antal)
    {
        List<Produkt> kurv = Kurv.HentKurven();

        bool produktetSkalOprettes = true;

        foreach (Produkt produkt in kurv)
        {
            if (produkt.Id == id)
            {
                produkt.Antal = antal;

                produktetSkalOprettes = false;

                if (produkt.Antal < 1)
                {
                    kurv.Remove(produkt);

                    break;
                }
            }
        }
        if (produktetSkalOprettes)
        {
            if (antal > 1)
            {
                kurv.Add(new Produkt(id, navn, pris, antal));
            }
        }
        Kurv.GemKurven(kurv);
    }
    /// <summary>
    /// Tilføjer en vare oveni det der allerede ligger i kurven.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="navn"></param>
    /// <param name="pris"></param>
    /// <param name="antal"></param>
    public static void PutFlereVareIKurv(int id, string navn, decimal pris, int antal)
    {
        List<Produkt> kurv = Kurv.HentKurven();

        bool produktetSkalOprettes = true;

        foreach (Produkt produkt in kurv)
        {
            if (produkt.Id == id)
            {
                produkt.Antal += antal;

                produktetSkalOprettes = false;

                if (produkt.Antal < 1)
                {
                    kurv.Remove(produkt);

                    break;
                }
            }
        }
        if (produktetSkalOprettes)
        {
            if (antal >= 1)
            {
                kurv.Add(new Produkt(id, navn, pris, antal));
            }
        }
        Kurv.GemKurven(kurv);
    }
    private static void GemKurven(List<Produkt> kurv)
    {
        HttpContext.Current.Session[KurvensNavnISession] = kurv;
    }
    //public static void UpdaterEnVareIKurv(int id, int antal)
    //{
    //    List<Produkt> produkter = HttpContext.Current.Session[KurvensNavnISession] as List<Produkt>;

    //    foreach (var item in produkter)
    //    {
    //        if (item.Id == id)
    //        {
    //            item.Antal = item.Antal + antal;
    //        }
    //    }
    //    HttpContext.Current.Session[KurvensNavnISession] = produkter;
    //}
    public static void VisKurv()
    {
        List<Produkt> kurv = Kurv.HentKurven();


        foreach (Produkt produkt in kurv)
        {
            HttpContext.Current.Response.Write(produkt.Id + " Navn: " + produkt.Navn + " Pris: " + produkt.Pris + " Antal: " + produkt.Antal + " Samlet pris: " + produkt.SamletPris);
        }

    }
    /// <summary>
    /// Udregner den samlede pris for hele kurven
    /// </summary>
    /// <returns>Den samlede pris for kurven</returns>
    public static decimal KurvensSamledePris()
    {
        decimal samletPris = 0;
        List<Produkt> kurv = Kurv.HentKurven();
        foreach (Produkt produkt in kurv)
        {
            samletPris += produkt.SamletPris;
        }
        return samletPris;
    }
    /// <summary>
    /// Udregner den samlede pr vare
    /// </summary>
    /// <returns>Samlede pris for én vare</returns>
    public static decimal SamletPrisForEnVare(int antal, decimal pris)
    {
        decimal samletPris = 0;
        List<Produkt> kurv = Kurv.HentKurven();

        samletPris = antal * pris;
        return samletPris;


    }
    public static int AntalIKurv(int produktID)
    {
        int antal = 0;
        List<Produkt> kurv = Kurv.HentKurven();
        foreach (Produkt produkt in kurv)
        {
            if (produkt.Id == produktID)
            {
                antal = produkt.Antal;
            }
        }
        return antal;
    }
    public static void OpdaterVareIKurv(int id, int antal)
    {
        List<Produkt> kurv = Kurv.HentKurven();

        foreach (Produkt produkt in kurv)
        {
            if (produkt.Id == id)
            {
                produkt.Antal = antal;

                if (produkt.Antal < 1)
                {
                    kurv.Remove(produkt);

                    break;
                }
            }
        }
        GemKurven(kurv);
    }
}