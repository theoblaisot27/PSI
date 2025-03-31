using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_PSI
{
    internal class Noeud<T>
    {
        public int Id { get; }
        public double Longitude { get; }
        public double Latitude { get; }
        public T Donnees { get; set; } 

        public Noeud(int id, double longitude, double latitude, T donnees)
        {
            Id = id;
            Longitude = longitude;
            Latitude = latitude;
            Donnees = donnees;
        }
    }
}
