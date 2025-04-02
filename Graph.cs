using Projet_PSI;

namespace projet_algo_2
{
    internal class Graph<T>
    {
        /// <summary>
        /// Dictionnaire contenant les nœuds du graphe.
        /// </summary>
        private Dictionary<T, Noeud<T>> noeuds;

        /// <summary>
        /// Liste d'adjacence représentant les connexions entre les nœuds.
        /// </summary>
        private Dictionary<T, List<T>> listeAdjacence;

        /// <summary>
        /// Liste des liens entre les nœuds.
        /// </summary>
        private List<Lien<T>> liens;
        public List<Lien<T>> Liens
        {
            get { return liens; }
        }

        /// <summary>
        /// Matrice d'adjacence représentant le graphe sous forme matricielle.
        /// </summary>
        private int[,] matriceAdjacence;

        /// <summary>
        /// Nombre total de nœuds dans le graphe.
        /// </summary>
        private int nombreNoeuds = 34;

        public int NombreNoeuds
        {
            get { return nombreNoeuds; }
        }

        /// <summary>
        /// Nombre total de liens dans le graphe.
        /// </summary>
        private int nombreLiens = 78;

        public Dictionary<T, List<T>> ListeAdjacence => listeAdjacence;
        public int[,] MatriceAdjacence => matriceAdjacence;
        public Dictionary<T, Noeud<T>> Noeuds
        {
            get { return noeuds; }
        }

        /// <summary>
        /// Constructeur de la classe Graph. Initialise les structures de données.
        /// </summary>
        public Graph(Func<string, T> conversion)
        {
            noeuds = new Dictionary<T, Noeud<T>>();
            listeAdjacence = new Dictionary<T, List<T>>();
            liens = new List<Lien<T>>();
            matriceAdjacence = new int[nombreNoeuds + 1, nombreNoeuds + 1];

            string fichier = "soc-karate.mtx";

            for (int i = 1; i <= nombreNoeuds; i++)
            {
                T noeudId = conversion(i.ToString()); // Convertit `i` en `T`
                noeuds.Add(noeudId, new Noeud<T>(noeudId));
                listeAdjacence.Add(noeudId, new List<T>());
            }
            LireFichier(fichier, conversion);
        }

        public void LireFichier(string fichier, Func<string, T> conversion)
        {
            try
            {
                using (StreamReader flux = new StreamReader(fichier))
                {
                    for (int i = 0; i < 23; i++)
                        flux.ReadLine(); // Ignorer les premières lignes

                    for (int i = 0; i < nombreLiens; i++)
                    {
                        string ligne = flux.ReadLine();
                        if (!string.IsNullOrWhiteSpace(ligne))
                        {
                            string[] valeurs = ligne.Split(' ');

                            // Conversion dynamique des valeurs en `T`
                            T noeud1 = conversion(valeurs[0]);
                            T noeud2 = conversion(valeurs[1]);

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
        /// Ajoute un nœud au graphe.
        /// </summary>
        public void AjouterNoeud(T id)
        {
            if (!noeuds.ContainsKey(id))
            {
                noeuds[id] = new Noeud<T>(id);
                listeAdjacence[id] = new List<T>();
            }
        }

        /// <summary>
        /// Ajoute un lien entre deux nœuds.
        /// </summary>
        public void AjouterLien(T n1, T n2)
        {
            if (!listeAdjacence.ContainsKey(n1)) listeAdjacence[n1] = new List<T>();
            if (!listeAdjacence.ContainsKey(n2)) listeAdjacence[n2] = new List<T>();

            if (!listeAdjacence[n1].Contains(n2))
            {
                listeAdjacence[n1].Add(n2);
                listeAdjacence[n2].Add(n1);
                liens.Add(new Lien<T>(noeuds[n1], noeuds[n2]));
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
        /// Effectue un parcours en largeur du graphe (BFS).
        /// </summary>
        public void ParcoursLargeur(T depart)
        {
            if (!noeuds.ContainsKey(depart))
            {
                Console.WriteLine("Le nœud de départ n'existe pas !");
                return;
            }

            Queue<T> file = new Queue<T>();
            HashSet<T> visite = new HashSet<T>();

            file.Enqueue(depart);
            visite.Add(depart);

            Console.WriteLine("Parcours en Largeur (BFS) :");
            while (file.Count > 0)
            {
                T noeud = file.Dequeue();
                Console.Write(noeud + " ");

                foreach (T voisin in listeAdjacence[noeud])
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
        public void ParcoursProfondeur(T depart)
        {
            if (!noeuds.ContainsKey(depart))
            {
                Console.WriteLine("Le nœud de départ n'existe pas !");
                return;
            }

            Stack<T> pile = new Stack<T>();
            HashSet<T> visite = new HashSet<T>();

            pile.Push(depart);
            visite.Add(depart);

            Console.WriteLine("Parcours en Profondeur (DFS) :");
            while (pile.Count > 0)
            {
                T noeud = pile.Pop();
                Console.Write(noeud + " ");

                foreach (T voisin in listeAdjacence[noeud])
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
        public List<T> Dijkstra(T source, T destination)
        {
            var distances = new Dictionary<T, double>();
            var predecessors = new Dictionary<T, T>();
            var priorityQueue = new SortedSet<T>(Comparer<T>.Create((x, y) => distances[x].CompareTo(distances[y])));

            foreach (var noeud in noeuds.Keys)
            {
                distances[noeud] = double.MaxValue;
                predecessors[noeud] = default(T);
                priorityQueue.Add(noeud);
            }

            distances[source] = 0;

            while (priorityQueue.Count > 0)
            {
                T currentNode = priorityQueue.Min;
                priorityQueue.Remove(currentNode);

                if (EqualityComparer<T>.Default.Equals(currentNode, destination))
                {
                    return ReconstructPath(predecessors, destination);
                }

                foreach (T voisin in listeAdjacence[currentNode])
                {
                    double weight = Lien<T>.CalculerDistance(noeuds[currentNode], noeuds[voisin]);
                    double alt = distances[currentNode] + weight;

                    if (alt < distances[voisin])
                    {
                        distances[voisin] = alt;
                        predecessors[voisin] = currentNode;

                        priorityQueue.Remove(voisin);
                        priorityQueue.Add(voisin);
                    }
                }
            }

            return new List<T>();
        }

        private List<T> ReconstructPath(Dictionary<T, T> predecessors, T destination)
        {
            var path = new List<T>();
            T current = destination;

            while (!EqualityComparer<T>.Default.Equals(current, default(T)))
            {
                path.Insert(0, current);
                current = predecessors[current];
            }

            return path;
        }

        public List<T> BellmanFord(T source, T destination)
        {

            var distances = new Dictionary<T, double>();
            var predecessors = new Dictionary<T, T>();
            var path = new List<T>();


            foreach (var noeud in noeuds.Keys)
            {
                distances[noeud] = double.MaxValue;
                predecessors[noeud] = default(T);
            }

            distances[source] = 0;

            for (int i = 1; i < noeuds.Count; i++)
            {
                foreach (var lien in liens)
                {
                    T n1 = lien.Noeud1.Donnees;
                    T n2 = lien.Noeud2.Donnees;
                    double weight = lien.Distance;

                    if (distances[n1] + weight < distances[n2])
                    {
                        distances[n2] = distances[n1] + weight;
                        predecessors[n2] = n1;
                    }

                    if (distances[n2] + weight < distances[n1])
                    {
                        distances[n1] = distances[n2] + weight;
                        predecessors[n1] = n2;
                    }
                }
            }

            foreach (var lien in liens)
            {
                T n1 = lien.Noeud1.Donnees;
                T n2 = lien.Noeud2.Donnees;
                double weight = lien.Distance;

                if (distances[n1] + weight < distances[n2])
                {
                    Console.WriteLine("Le graphe contient un cycle négatif");
                    return new List<T>();
                }

                if (distances[n2] + weight < distances[n1])
                {
                    Console.WriteLine("Le graphe contient un cycle négatif");
                    return new List<T>();
                }
            }

            if (distances[destination] == double.MaxValue)
            {
                Console.WriteLine("Aucun chemin trouvé vers la destination");
                return new List<T>();
            }

            T current = destination;
            while (!EqualityComparer<T>.Default.Equals(current, default(T)))
            {
                path.Insert(0, current);
                current = predecessors[current];
            }

            return path;
        }

        public double[,] FloydWarshall()
        {
            int n = noeuds.Count;
            double[,] dist = new double[n, n];
            int[,] next = new int[n, n]; 

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        dist[i, j] = 0;
                    }
                    else
                    {
                        dist[i, j] = double.MaxValue;
                        next[i, j] = -1;
                    }
                }
            }

            foreach (var lien in liens)
            {
                T n1 = lien.Noeud1.Donnees;
                T n2 = lien.Noeud2.Donnees;
                double weight = lien.Distance;

                int i = GetNodeIndex(n1);
                int j = GetNodeIndex(n2);

                dist[i, j] = weight;
                dist[j, i] = weight;
                next[i, j] = j;
                next[j, i] = i;
            }

            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                       
                        if (dist[i, j] > dist[i, k] + dist[k, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                            next[i, j] = next[i, k]; 
                        }
                    }
                }
            }

            return dist;
        }

        private int GetNodeIndex(T noeud)
        {
            int index = 0;
            foreach (var kvp in noeuds)
            {
                if (EqualityComparer<T>.Default.Equals(kvp.Value.Donnees, noeud))
                {
                    return index;
                }
                index++;
            }
            throw new Exception("Noeud introuvable");
        }

        public List<T> ReconstructPath(int[,] next, T start, T end)
        {
            int startIndex = GetNodeIndex(start);
            int endIndex = GetNodeIndex(end);
            List<T> path = new List<T>();

            if (next[startIndex, endIndex] == -1)
            {
                Console.WriteLine("Pas de chemin disponible.");
                return path;
            }

            T current = start;
            while (!EqualityComparer<T>.Default.Equals(current, end))
            {
                int currentIndex = GetNodeIndex(current);  
                Noeud<T> currentNode = noeuds.Values.ElementAt(currentIndex); 

                current = currentNode.Donnees;
            }

            path.Add(end);
            return path;
        }
    }

}





