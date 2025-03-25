using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_PSI
{
    internal class Lien
    {

        public Noeud Noeud1 { get; }
        public Noeud Noeud2 { get; }
        public int distance { get; } //distance en Km

        public Lien(Noeud n1, Noeud n2)
        {
            Noeud1 = n1;
            Noeud2 = n2;
            this.distance = distance(n1, n2);
        }

        static int distance(Noeud n1, Noeud n2)
        {
            int n;
            int Dlat = n2.Latitude - n1.Latitude;
            if (Dlat <0)
            {
                Dlat = -Dlat;
            }
            int Dlong = n2.Longitude - n1.Longitude;
            if (Dlong <0)
            {
                Dlong = -Dlong;
            }
            n = 2*6371*Math.Asin(Math.Sqrt(Math.Sin(Dlat/2)*Math.Sin(Dlat/2)+Math.Cos(n1.Latitude)*Math.Cos(n2.Latitude)*Math.Sin(Dlong/2)*Math.Sin(Dlong/2)));
            return n;
        }
    }
}
