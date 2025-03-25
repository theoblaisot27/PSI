using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_PSI
{
    internal class Noeud
    {
        public int Id { get; }
        public int longitude { get; }
        public int latitude { get; }
        public Noeud(int id, int Long, int Lat)
        {
            this.Id = id;
            this.longitude = Long;
            this.latitude = Lat;
            
        }
        
    }
}
