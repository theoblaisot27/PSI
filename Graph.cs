using Projet_PSI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using.IO;

namespace Projet_PSI
{
    internal class Graph
    {
        private Dictionary<int, Noeud> noeuds;
        private Dictionary<int, List<int>> listeAdjacence;
        private int[,] matriceAdjacence;
        private int nombreNoeuds = 34;
        private int nombreLiens = 78;

        public Dictionary<int, List<int>> ListeAdjacence
        {
            get { return this.listeAdjacence; }

        }
        public int[,] MatrciceAdjacence
        {
            get { return this.matriceAdjacence; }
        }
        public Dictionary<int, Noeud> Noeuds
        {
            get { return this.noeuds; }
        }


        public Graph()
        {
            int id = 4;


            Noeud noeud1 = new Noeud(id);
            Noeud noeud2 = new Noeud(id);
            Lien lien1 = new Lien(noeud1, noeud2);
            Dictionary<int, Noeud> noeuds = new Dictionary<int, Noeud>();
            Dictionary<Noeud, List<Noeud>> listeAdjacence = new Dictionary<Noeud, List<Noeud>>();
            int[,] matrciceAdjacence = new int[nombreNoeuds + 1, nombreNoeuds + 1];
            List<Lien> liens = new List<Lien>();
        }
        public void readLine(string fichier)
        {

            try
            {
                StreamReader flux = new StreamReader(fichier);


                for (int i = 0; i < 23; i++)
                {
                    flux.ReadLine();
                }


                for (int i = 0; i < 78; i++)
                {
                    string ligne = flux.ReadLine();
                    if (string.IsNullOrWhiteSpace(ligne))
                    {
                        continue;
                    }
                    string[] valeurs = ligne.Split(' ');
                    int noeud1 = int.Parse(valeurs[0]);
                    int noeud2 = int.Parse(valeurs[1]);

                    AjouterLien(noeud1, noeud2);
                }
                flux.Close();




            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de la lecture du fichier : " + e.Message);
            }

        }

        public void AjouterLien(int n1, int n2)
        {
            if (!listeAdjacence.ContainsKey(n1))
            {
                listeAdjacence[n1] = new List<int>();
            }

            if (!listeAdjacence.ContainsKey(n2))
            {
                listeAdjacence[n2] = new List<int>();
            }

            listeAdjacence[n1].Add(n2);
            listeAdjacence[n2].Add(n1);


        }
        public void ConstruireMatriceAdjacence()
        {
            for (int i = 0; i <= nombreNoeuds; i++)
            {
                for (int j = 0; j <= nombreNoeuds; j++)
                {
                    matriceAdjacence[i, j] = 0;
                }
            }


            foreach (var noeud in listeAdjacence)
            {
                int n1 = noeud.Key;
                foreach (int n2 in noeud.Value)
                {
                    matriceAdjacence[n1, n2] = 1;
                    matriceAdjacence[n2, n1] = 1;
                }
            }
        }
    }
}
   




    
