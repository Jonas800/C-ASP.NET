using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Spil
/// </summary>
[Serializable]
public class Spil
{
    public int Credit { get; set; }
    public List<Symbol> symboler { get; set; }
    public List<Symbol> resultat { get; set; }
    public bool gevinst { get; set; }
    public Spil()
    {


        //gevinst.Add(0);
        //gevinst.Add(5);
        //gevinst.Add(10);
        //gevinst.Add(30);
        //gevinst.Add(60);
        //gevinst.Add(100);
        //gevinst.Add(150);
        //gevinst.Add(500);

        //this.Gevinster = gevinst;


        //List<string> billede = new List<string>();
        //billede.Add("/billeder/pikachu.png");
        //billede.Add("/billeder/meowth.png");
        //billede.Add("/billeder/marchamp.png");
        //billede.Add("/billeder/venusaur.png");
        //billede.Add("/billeder/blastoise.png");
        //billede.Add("/billeder/charizard.png");
        //billede.Add("/billeder/snorlax.png");
        //billede.Add("/billeder/dragonite.png");

        //this.Billede = billede;

        symboler = new List<Symbol>();
        symboler.Add(new Symbol(4, 500, "/billeder/dragonite.png"));
        symboler.Add(new Symbol(6, 150, "/billeder/snorlax.png"));
        symboler.Add(new Symbol(8, 100, "/billeder/charizard.png"));
        symboler.Add(new Symbol(12, 60, "/billeder/blastoise.png"));
        symboler.Add(new Symbol(15, 30, "/billeder/venusaur.png"));
        symboler.Add(new Symbol(25, 10, "/billeder/marchamp.png"));
        symboler.Add(new Symbol(30, 5, "/billeder/meowth.png"));
    }
    public void SpilNu()
    {
        if (this.Credit >= 1)
        {

            this.Credit--;
            List<Symbol> hjul = new List<Symbol>();

            foreach (Symbol item in symboler)
            {
                for (int i = 0; i < item.Sandsynlighed; i++)
                {
                    hjul.Add(item);
                }
            }
            this.resultat = new List<Symbol>();
            resultat.Add(hjul.OrderBy(h => Guid.NewGuid()).First());
            resultat.Add(hjul.OrderBy(h => Guid.NewGuid()).First());
            resultat.Add(hjul.OrderBy(h => Guid.NewGuid()).First());
            if (resultat[0] == resultat[1] && resultat[1] == resultat[2])
            {
                this.Credit += resultat[0].Gevinst;
                this.gevinst = true;

                //Fjerner gentagne billeder ved tre rigtige
                //symboler.RemoveAll(r => r.Billede.Contains(resultat[0].Billede));
            }
            else
            {
                this.gevinst = false;
            }
        }

    }
}