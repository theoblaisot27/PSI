using projet_algo_2;

namespace Projet_PSI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fichier = "MetroParisNoeud.csv";

            string fichier2 = "MetroParisArc.csv";

            Graph<int> g = new Graph<int>(s => int.Parse(s), fichier, fichier2);
            g.AfficherListeAdjacence();


        }
    }
}
