
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
        public double Distance { get; }



        public Lien(Noeud<T> n1, Noeud<T> n2)
        {
            Noeud1 = n1;
            Noeud2 = n2;
            this.Distance = CalculerDistance(n1, n2);
        }

        public static double CalculerDistance(Noeud<T> n1, Noeud<T> n2)
        {
            const double R = 6371;
            double lat1 = n1.Latitude * Math.PI / 180.0;
            double lon1 = n1.Longitude * Math.PI / 180.0;
            double lat2 = n2.Latitude * Math.PI / 180.0;
            double lon2 = n2.Longitude * Math.PI / 180.0;

            double dlat = lat2 - lat1;
            double dlon = lon2 - lon1;

            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dlon / 2), 2);
            double c = 2 * Math.Asin(Math.Sqrt(a));

            return R * c;
        }
    }
}
