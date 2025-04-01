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
    
    
}
}
