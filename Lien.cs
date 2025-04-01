using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_PSI
{
    internal class Lien<T>
{
    public Noeud<T> Noeud1 { get; }
    public Noeud<T> Noeud2 { get; }
    //public double Distance { get; } // distance en Km



    public Lien(Noeud<T> n1, Noeud<T> n2)
    {
        Noeud1 = n1;
        Noeud2 = n2;
        this.Distance = CalculerDistance(n1, n2);
    }

    static double CalculerDistance(Noeud<T> n1, Noeud<T> n2)
    {
        double n = 0;
        // int Dlat = n2.latitude - n1.latitude;
        // if (Dlat < 0) { Dlat = -Dlat; }
        // int Dlong = n2.longitude - n1.longitude;
        // if (Dlong < 0) { Dlong = -Dlong; }
        // n = 2 * 6371 * Math.Asin(Math.Sqrt(Math.Sin(Dlat / 2) * Math.Sin(Dlat / 2) +
        //     Math.Cos(n1.latitude) * Math.Cos(n2.latitude) * Math.Sin(Dlong / 2) * Math.Sin(Dlong / 2)));
        return n;
    }
}
