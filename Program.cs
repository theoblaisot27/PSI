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

            List<int> chemin = g.Dijkstra(54, 126);

            Console.WriteLine("Chemin le plus court : " + string.Join(" -> ", chemin));

            g.AfficherCheminAvecLibelles(chemin);

        }
    }
}
