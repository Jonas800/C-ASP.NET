using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Symbol
/// </summary>
public class Symbol
{
	public Symbol(int sandsynlighed, int gevinst, string billede)
	{
        this.Sandsynlighed = sandsynlighed;
        this.Gevinst = gevinst;
        this.Billede = billede;
	}
    public int Sandsynlighed { get; set; }
    public int Gevinst { get; set; }
    public string Billede { get; set; }
}