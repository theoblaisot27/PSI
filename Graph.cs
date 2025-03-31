using Projet_PSI;

namespace projet_algo_2
{
    internal class Graph
    {
        /// <summary>
        /// Dictionnaire contenant les nœuds du graphe.
        /// </summary>
        private Dictionary<int, Noeud> noeuds;

        /// <summary>
        /// Liste d'adjacence représentant les connexions entre les nœuds.
        /// </summary>
        private Dictionary<int, List<int>> listeAdjacence;

        /// <summary>
        /// Liste des liens entre les nœuds.
        /// </summary>
        private List<Lien> liens;

        /// <summary>
        /// Matrice d'adjacence représentant le graphe sous forme matricielle.
        /// </summary>
        private int[,] matriceAdjacence;

        /// <summary>
        /// Nombre total de nœuds dans le graphe.
        /// </summary>
        private int nombreNoeuds = 34;

        /// <summary>
        /// Nombre total de liens dans le graphe.
        /// </summary>
        private int nombreLiens = 78;

        public Dictionary<int, List<int>> ListeAdjacence => listeAdjacence;
        public int[,] MatriceAdjacence => matriceAdjacence;
        public Dictionary<int, Noeud> Noeuds => noeuds;

        /// <summary>
        /// Constructeur de la classe Graph. Initialise les structures de données.
        /// </summary>
        public Graph()
        {
            noeuds = new Dictionary<int, Noeud>();
            listeAdjacence = new Dictionary<int, List<int>>();
            liens = new List<Lien>();
            matriceAdjacence = new int[nombreNoeuds + 1, nombreNoeuds + 1];

            for (int i = 1; i <= nombreNoeuds; i++)
            {
                noeuds.Add(i, new Noeud(i));
                listeAdjacence.Add(i, new List<int>());
            }
        }

        /// <summary>
        /// Lit un fichier contenant la structure du graphe et construit le graphe.
        /// </summary>
        /// <param name="fichier">Chemin du fichier à lire.</param>
        public void LireFichier(string fichier)
        {
            try
            {
                using (StreamReader flux = new StreamReader(fichier))
                {
                    for (int i = 0; i < 23; i++)
                        flux.ReadLine();

                    for (int i = 0; i < nombreLiens; i++)
                    {
                        string ligne = flux.ReadLine();
                        if (!string.IsNullOrWhiteSpace(ligne))
                        {
                            string[] valeurs = ligne.Split(' ');
                            int noeud1 = int.Parse(valeurs[0]);
                            int noeud2 = int.Parse(valeurs[1]);

                            AjouterLien(noeud1, noeud2);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de la lecture du fichier : " + e.Message);
            }
        }

        /// <summary>
        /// Ajoute un lien entre deux nœuds.
        /// </summary>
        public void AjouterLien(int n1, int n2)
        {
            if (!listeAdjacence.ContainsKey(n1)) listeAdjacence[n1] = new List<int>();
            if (!listeAdjacence.ContainsKey(n2)) listeAdjacence[n2] = new List<int>();

            if (!listeAdjacence[n1].Contains(n2))
            {
                listeAdjacence[n1].Add(n2);
                listeAdjacence[n2].Add(n1);
                liens.Add(new Lien(noeuds[n1], noeuds[n2]));

                matriceAdjacence[n1, n2] = 1;
                matriceAdjacence[n2, n1] = 1;
            }
        }

        /// <summary>
        /// Affiche la liste d'adjacence du graphe.
        /// </summary>
        public void AfficherListeAdjacence()
        {
            Console.WriteLine("Liste d'adjacence :");
            foreach (var kvp in listeAdjacence)
            {
                Console.Write(kvp.Key + " -> ");
                Console.WriteLine(string.Join(", ", kvp.Value));
            }
        }

        /// <summary>
        /// Affiche la matrice d'adjacence du graphe.
        /// </summary>
        public void AfficherMatriceAdjacence()
        {
            Console.WriteLine("Matrice d'adjacence :");
            for (int i = 1; i <= nombreNoeuds; i++)
            {
                for (int j = 1; j <= nombreNoeuds; j++)
                    Console.Write(matriceAdjacence[i, j] + " ");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Effectue un parcours en largeur du graphe (BFS).
        /// </summary>
        public void ParcoursLargeur(int depart)
        {
            if (!noeuds.ContainsKey(depart))
            {
                Console.WriteLine("Le nœud de départ n'existe pas !");
                return;
            }

            Queue<int> file = new Queue<int>();
            HashSet<int> visite = new HashSet<int>();

            file.Enqueue(depart);
            visite.Add(depart);

            Console.WriteLine("Parcours en Largeur (BFS) :");
            while (file.Count > 0)
            {
                int noeud = file.Dequeue();
                Console.Write(noeud + " ");

                foreach (int voisin in listeAdjacence[noeud])
                {
                    if (!visite.Contains(voisin))
                    {
                        file.Enqueue(voisin);
                        visite.Add(voisin);
                    }
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Effectue un parcours en profondeur du graphe (DFS).
        /// </summary>
        public void ParcoursProfondeur(int depart)
        {
            if (!noeuds.ContainsKey(depart))
            {
                Console.WriteLine("Le nœud de départ n'existe pas !");
                return;
            }

            Stack<int> pile = new Stack<int>();
            HashSet<int> visite = new HashSet<int>();

            pile.Push(depart);
            visite.Add(depart);

            Console.WriteLine("Parcours en Profondeur (DFS) :");
            while (pile.Count > 0)
            {
                int noeud = pile.Pop();
                Console.Write(noeud + " ");

                foreach (int voisin in listeAdjacence[noeud])
                {
                    if (!visite.Contains(voisin))
                    {
                        pile.Push(voisin);
                        visite.Add(voisin);
                    }
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Vérifie si le graphe est connexe.
        /// </summary>
        /// <returns>True si le graphe est connexe, sinon False.</returns>
        public bool EstConnexe()
        {
            if (noeuds.Count == 0) return false;

            int premierNoeud = noeuds.Keys.First();
            HashSet<int> visite = new HashSet<int>();
            Queue<int> file = new Queue<int>();

            file.Enqueue(premierNoeud);
            visite.Add(premierNoeud);

            while (file.Count > 0)
            {
                int noeud = file.Dequeue();
                foreach (int voisin in listeAdjacence[noeud])
                {
                    if (!visite.Contains(voisin))
                    {
                        file.Enqueue(voisin);
                        visite.Add(voisin);
                    }
                }
            }
            return visite.Count == nombreNoeuds;
        }

        /// <summary>
        /// Vérifie si le graphe contient un cycle.
        /// </summary>
        /// <returns>True si un cycle est détecté, sinon False.</returns>
        public bool ContientCycle()
        {
            bool[] visite = new bool[nombreNoeuds + 1];

            for (int i = 1; i <= nombreNoeuds; i++)
            {
                if (!visite[i] && Parcours_Cycle(i, -1, visite))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Méthode auxiliaire pour détecter un cycle via DFS.
        /// </summary>
        private bool Parcours_Cycle(int noeud, int parent, bool[] visite)
        {
            visite[noeud] = true;

            foreach (int voisin in listeAdjacence[noeud])
            {
                if (!visite[voisin])
                {
                    if (Parcours_Cycle(voisin, noeud, visite)) return true;
                }
                else if (voisin != parent) return true;
            }
            return false;
        }
    }
}
