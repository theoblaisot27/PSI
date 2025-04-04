namespace Projet_PSI
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
        public Graph(Func<string, T> conversion, string fichierNoeud, string fichierArc)
        {
            noeuds = new Dictionary<T, Noeud<T>>();
            listeAdjacence = new Dictionary<T, List<T>>();
            liens = new List<Lien<T>>();



            LireFichierNoeud(fichierNoeud, conversion);
            LireFichierArc(fichierArc, conversion);
        }

        /// <summary>Permet de lire le fichier Noeud
        /// On utilise un streamReader pour lire chaque ligne du fichier excel un utilisant un tableau 
        /// de string pour chaque case du tableau pour construire chaque noeud.
        /// </summary>
        /// <param name="fichier"></param> Nom du fichier
        /// <param name="conversion"></param>Convertion pour le type générique T
        public void LireFichierNoeud(string fichier, Func<string, T> conversion)
        {
            try
            {
                using (StreamReader flux = new StreamReader(fichier))
                {

                    string ligne = flux.ReadLine();
                    ligne = flux.ReadLine();

                    while (ligne != null)
                    {

                        if (!string.IsNullOrWhiteSpace(ligne))
                        {
                            string[] valeurs = ligne.Split(';');
                            int id = int.Parse(valeurs[0]);
                            string libelle = valeurs[2];
                            double longitude = double.Parse(valeurs[3]);
                            double latitude = double.Parse(valeurs[4]);
                            T noeudId = conversion(id.ToString());
                            Noeud<T> noeud = new Noeud<T>(id, libelle, longitude, latitude, noeudId);
                            if (!this.noeuds.ContainsKey(noeudId))
                            {
                                this.noeuds.Add(noeudId, noeud);
                            }
                            ligne = flux.ReadLine();



                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("1Erreur lors de la lecture du fichier : " + e.Message);
            }
        }

        public void LireFichierArc(string fichier, Func<string, T> conversion)
        {
            try
            {
                using (StreamReader flux = new StreamReader(fichier))
                {

                    string ligne = flux.ReadLine();
                    ligne = flux.ReadLine();
                    while (ligne != null)
                    {

                        if (!string.IsNullOrWhiteSpace(ligne))
                        {
                            string[] valeurs = ligne.Split(';');
                            T n1 = conversion(valeurs[0]);
                            string libelle = valeurs[1];
                            T n2 = conversion(valeurs[2]);
                            T n3 = conversion(valeurs[3]);
                            if (!n2.Equals(0))
                            {
                                AjouterLien(n1, n2);
                            }
                            if (!n3.Equals(0))
                            {
                                AjouterLien(n1, n3);
                            }






                        }
                        ligne = flux.ReadLine();
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
        public void AjouterNoeud(int id, string libelle, double longitude, double latitude, T donnees)
        {
            if (!noeuds.ContainsKey(donnees))
            {
                Noeud<T> nouveauNoeud = new Noeud<T>(id, libelle, longitude, latitude, donnees);
                this.noeuds.Add(donnees, nouveauNoeud);
                listeAdjacence[donnees] = new List<T>();
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
            int k = 0;
            Console.WriteLine("Liste d'adjacence :");
            foreach (var kvp in listeAdjacence)
            {
                string lib = "";
                List<T> libe = new List<T>();

                foreach (KeyValuePair<T, Noeud<T>> n in noeuds)
                {
                    if (n.Key.Equals(kvp.Key))
                    {
                        lib = n.Value.Libelle;
                    }

                }
                Console.Write(lib + " -> ");
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    foreach (KeyValuePair<T, Noeud<T>> n in noeuds)
                    {
                        if (n.Value.Id.Equals(kvp.Value[i]))
                        {
                            Console.Write(n.Value.Libelle + " ; ");
                        }
                    }


                }

                Console.WriteLine();

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



        public List<T> Dijkstra(T depart, T arrivee)
        {
            var distances = new Dictionary<T, double>();
            var precedent = new Dictionary<T, T>();
            var nonVisites = new HashSet<T>();

            // Initialisation
            foreach (var noeud in noeuds.Keys)
            {
                distances[noeud] = double.PositiveInfinity;
                nonVisites.Add(noeud);
            }

            distances[depart] = 0;

            while (nonVisites.Count > 0)
            {
                // Noeud avec la plus petite distance non encore visité
                T courant = nonVisites.OrderBy(n => distances[n]).First();
                nonVisites.Remove(courant);

                if (courant.Equals(arrivee))
                    break;

                foreach (var voisin in listeAdjacence[courant])
                {
                    if (!nonVisites.Contains(voisin)) continue;

                    // Trouver le lien entre le noeud courant et son voisin
                    var lien = liens.Find(l =>
                        (l.Noeud1.Donnees.Equals(courant) && l.Noeud2.Donnees.Equals(voisin)) ||
                        (l.Noeud2.Donnees.Equals(courant) && l.Noeud1.Donnees.Equals(voisin)));

                    double distanceLien = lien != null ? lien.Distance : double.PositiveInfinity;

                    double nouvelleDistance = distances[courant] + distanceLien;

                    if (nouvelleDistance < distances[voisin])
                    {
                        distances[voisin] = nouvelleDistance;
                        precedent[voisin] = courant;
                    }
                }
            }

            // Reconstruction du chemin
            List<T> chemin = new List<T>();
            T actuel = arrivee;

            while (!actuel.Equals(depart))
            {
                if (!precedent.ContainsKey(actuel))
                {
                    Console.WriteLine("Aucun chemin trouvé.");
                    return new List<T>();
                }

                chemin.Insert(0, actuel);
                actuel = precedent[actuel];
            }

            chemin.Insert(0, depart);
            return chemin;
        }



        public void AfficherCheminAvecLibelles(List<T> chemin)
        {
            Console.WriteLine("Chemin avec libellés :");

            foreach (T id in chemin)
            {
                if (noeuds.TryGetValue(id, out Noeud<T> noeud))
                {
                    Console.Write(noeud.Libelle);
                }
                else
                {
                    Console.Write($"[Inconnu: {id}]");
                }

                if (!id.Equals(chemin.Last()))
                {
                    Console.Write(" -> ");
                }
            }

            Console.WriteLine();
        }

        public List<T> BellmanFord(T source, T destination)
        {
            var distances = new Dictionary<T, double>();
            var precedent = new Dictionary<T, T>();

            // Initialisation
            foreach (var noeud in noeuds.Keys)
            {
                distances[noeud] = double.PositiveInfinity;
            }
            distances[source] = 0;

            int n = noeuds.Count;

            // Détente des arêtes
            for (int i = 0; i < n - 1; i++)
            {
                foreach (var lien in liens)
                {
                    T u = lien.Noeud1.Donnees;
                    T v = lien.Noeud2.Donnees;
                    double poids = lien.Distance;

                    if (distances[u] + poids < distances[v])
                    {
                        distances[v] = distances[u] + poids;
                        precedent[v] = u;
                    }

                    // Comme c'est non orienté
                    if (distances[v] + poids < distances[u])
                    {
                        distances[u] = distances[v] + poids;
                        precedent[u] = v;
                    }
                }
            }

            // Vérification des cycles négatifs (pas obligatoire ici sauf si les poids peuvent être négatifs)
            foreach (var lien in liens)
            {
                T u = lien.Noeud1.Donnees;
                T v = lien.Noeud2.Donnees;
                if (distances[u] + lien.Distance < distances[v])
                {
                    Console.WriteLine("Le graphe contient un cycle de poids négatif.");
                    return new List<T>();
                }
            }

            // Reconstruction du chemin
            List<T> chemin = new List<T>();
            T courant = destination;

            while (!courant.Equals(source))
            {
                if (!precedent.ContainsKey(courant))
                {
                    Console.WriteLine("Aucun chemin trouvé.");
                    return new List<T>();
                }
                chemin.Insert(0, courant);
                courant = precedent[courant];
            }

            chemin.Insert(0, source);
            return chemin;
        }


        public Dictionary<(T, T), List<T>> FloydWarshall()
        {
            var sommets = noeuds.Keys.ToList();
            int n = sommets.Count;

            var distance = new Dictionary<(T, T), double>();
            var suivant = new Dictionary<(T, T), T>();

            // Initialisation
            foreach (T i in sommets)
            {
                foreach (T j in sommets)
                {
                    if (i.Equals(j))
                    {
                        distance[(i, j)] = 0;
                    }
                    else if (listeAdjacence[i].Contains(j))
                    {
                        var lien = liens.Find(l =>
                            (l.Noeud1.Donnees.Equals(i) && l.Noeud2.Donnees.Equals(j)) ||
                            (l.Noeud2.Donnees.Equals(i) && l.Noeud1.Donnees.Equals(j)));

                        distance[(i, j)] = lien?.Distance ?? double.PositiveInfinity;
                        suivant[(i, j)] = j;
                    }
                    else
                    {
                        distance[(i, j)] = double.PositiveInfinity;
                    }
                }
            }

            // Algorithme de Floyd-Warshall
            foreach (T k in sommets)
            {
                foreach (T i in sommets)
                {
                    foreach (T j in sommets)
                    {
                        if (distance[(i, k)] + distance[(k, j)] < distance[(i, j)])
                        {
                            distance[(i, j)] = distance[(i, k)] + distance[(k, j)];
                            suivant[(i, j)] = suivant[(i, k)];
                        }
                    }
                }
            }

            // Reconstruire les chemins
            var chemins = new Dictionary<(T, T), List<T>>();
            foreach (T i in sommets)
            {
                foreach (T j in sommets)
                {
                    if (distance[(i, j)] == double.PositiveInfinity || i.Equals(j)) continue;

                    List<T> chemin = new List<T> { i };
                    T courant = i;
                    while (!courant.Equals(j))
                    {
                        courant = suivant[(courant, j)];
                        chemin.Add(courant);
                    }
                    chemins[(i, j)] = chemin;
                }
            }

            return chemins;
        }




    }
}
