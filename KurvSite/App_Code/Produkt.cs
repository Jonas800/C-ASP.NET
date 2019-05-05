using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Produkt
/// </summary>
public class Produkt
{
    public int Id { get; set; }
    public string Navn { get; set; }
    public decimal Pris { get; set; }
    public int Antal { get; set; }
    public decimal SamletPris
    {
        get
        {
            return (Pris * Convert.ToDecimal(Antal));
        }
    }
	public Produkt(int id, string navn, decimal pris, int antal)
	{
        this.Id = id;
        this.Navn = navn;
        this.Pris = pris;
        this.Antal = antal;
	}
}