using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolve
{
    public class Grille
    {

        //Variables
        protected Case[,] m_cases;

        protected bool OK(int v)
        {
            return (v >= 0) && (v < 9);
        }

        protected void Afficher()
        {
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    Console.Write(((m_cases[i, j].Valeur() != 0) ? "" + m_cases[i, j].Valeur() : " ") + " ");
                Console.WriteLine();
            }
            Console.Read();
        }

        protected void Init()
        {

            m_cases = new Case[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    m_cases[i, j] = new Case();
            }
        }

        public Grille()
        {
            Init();
        }

        //Créé une copie de la grille en paramètre
        public Grille(Grille g)
        {
            Init();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    m_cases[i, j].Set(g.Get(i, j));
                }
            }
        }

        //Utiliser le tableau en paramètre
        public Grille(int[,] tab)
        {
            Init();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    m_cases[i, j].Set(tab[i, j]);
                }
            }
        }

        //Getter
        public int Get(int x, int y)
        {
            if(OK(x) && OK(y))
                return m_cases[x, y].Valeur();
            return -1;
        }

        //Setter
        public void Set(int x, int y, int valeur)
        {
            if (OK(x) && OK(y))
            {
                m_cases[x, y].Set(valeur);
                if(valeur != 0)
                {
                    //On informe les cases de la ligne et de la colonne que la valeur n'est plus dispo
                    for(int k = 0; k < 9; k++)
                    {
                        if (m_cases[x, k].Dispo(valeur))
                        {
                            if(m_cases[x, k].SetNonDispo(valeur) && (x == 4) && (k == 4))
                                Console.WriteLine("Valeur " + valeur + " supprimée");
                        }
                        if (m_cases[k, y].Dispo(valeur))
                        {
                            if (m_cases[k, y].SetNonDispo(valeur) && (y == 4) && (k == 4))
                                Console.WriteLine("Valeur " + valeur + " supprimée");
                        }
                    }
                    //On informe les cases du carré que la valeur n'est plus dispo
                    int i = x / 3;
                    int j = y / 3;
                    for(int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            int pX = (3 * i) + k;
                            int pY = (3 * j) + l;
                            if (m_cases[pX, pY].Dispo(valeur))
                            {
                                if (m_cases[pX, pY].SetNonDispo(valeur) && (pX == 4) && (pY == 4))
                                    Console.WriteLine("Valeur " + valeur + " supprimée");
                            }
                        }
                    }
                }
            }
        }

        public void Reset()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    m_cases[i, j] = new Case();
            }
        }

        public void Set(int[,] tab)
        {
            Reset();
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    Set(i, j, tab[i, j]);
            }
        }

        //Vérifie qu'il n'y a pas d'incohérence dans la grille.
        public bool Possible()
        {
            //On vérifie que la grille peut avoir une solution
            //On commence par supposer que oui
            bool test = true;
            //On teste toutes les lignes et toutes les colonnes
            for (int i = 0; (i < 9) && test; i++)
            {
                bool[] dispoL = new bool[9];
                bool[] dispoC = new bool[9];
                for (int j = 0; j < 9; j++)
                {
                    dispoL[j] = true;
                    dispoC[j] = true;
                }
                for (int j = 0; (j < 9) && test; j++)
                {
                    if (Get(i, j) != 0)
                    {
                        test = dispoL[Get(i, j) - 1];
                        dispoL[Get(i, j) - 1] = false;
                    }
                    if (Get(j, i) != 0)
                    {
                        test = dispoC[Get(j, i) - 1];
                        dispoC[Get(j, i) - 1] = false;
                    }
                }
            }
            //On teste tous les carrés
            for(int i = 0; (i < 3) && test; i++)
            {
                for(int j = 0; (j < 3) && test; j++)
                {
                    bool[] dispo = new bool[9];
                    for (int k = 0; k < 9; k++)
                        dispo[k] = true;
                    for(int k = 0; (k < 3) && test; k++)
                    {
                        for(int l = 0; (l < 3) && test; l++)
                        {
                            if (Get((3 * i) + k, (3 * j) + l) != 0)
                            {
                                test = dispo[Get((3 * i) + k, (3 * j) + l) - 1];
                                dispo[Get((3 * i) + k, (3 * j) + l) - 1] = false;
                            }
                        }
                    }
                }
            }
            //On retourne enfin le résultat
            return test;
        }

        //Retourne la liste des solutions possibles
        public Grille[] Solve()
        {
            //On charche pour quelle case on a le moins de possibilités
            //Pour optimiser l'algorithme on remplie toutes les cases avec une seule possibilité
            //Si on a rempli au moins une case, les possibilités sont diminuées donc on recommence la recherche
            //Enfin s'il n'y a plus que des cases avec plusieurs possibilités, on selectionne celle qui en a le moins
            //On créé une nouvelle grille pour chaque possibilité de la case choisie et on réappelle cette fonction pour essayer de resoudre la grille
            int i, j;
            bool valeursSimples;
            Console.WriteLine("Nb possibles : " + m_cases[4, 4].NbDispo());
            do
            {
                //On fait les vérifications à chaque itération car une valeur a pu être entrée alors qu'elle est erronnée
                //Si la grille est pleine et sans incohérance, on retourne 1
                if (Possible() && Remplie())
                    return new Grille[0];
                //Si elle n'est pas cohérente, on retourne 0
                //Une grille est incohérente si on a une case vide avec aucune possibilité ou plusieurs fois le même chiffre dans des carrés, colonnes ou lignes
                if (!Possible())
                    return new Grille[0];
                //On regarde les possibilités
                i = 0;
                j = 0;
                //On réinitialise notre valeur de test de while() pour ne pas avoir de boucle infinie
                valeursSimples = false;
                for (int x = 0; (x < 9); x++)
                {
                    for (int y = 0; (y < 9); y++)
                    {
                        //Si on a une case vide sans valeurs possible la grille est incohérente 
                        if (m_cases[x, y].NbDispo() == 0)
                            return new Grille[0];
                        if (m_cases[x, y].NbDispo() == 1)
                        {
                            //Si on trouve une case avec une seule valeur possible, on rentre cette valeur
                            bool[] temp = Possibilites(x, y);
                            //L'ajout d'une valeur a modifié les possibilités des cases sur ses ligne, colonne et carré
                            //Il faut donc revérifier les cases au dessus de celle-ci, c'est pourquoi on recommence la boucle
                            valeursSimples = true;
                            for (int z = 0; z < 9; z++)
                            {
                                if (temp[z])
                                    Set(x, y, (z + 1));
                            }
                        }
                        else if ((!valeursSimples) && (m_cases[x, y].NbDispo() < m_cases[i, j].NbDispo()))
                        {
                            //On ne continue de chercher la case avec le moins de possibilité que si aucune case ayant une seule possibilité n'a été trouvée
                            //En effet, dans ce cas les probabilités ont changé donc la recherche est faussée pour cette boucle
                            i = x;
                            j = y;
                        }
                    }
                }
            } while (valeursSimples);
            //On fait en fonction des possibilités disponnibles
            bool[] dispo = Possibilites(i, j);
            Grille[] res = new Grille[0];
            for (int k = 0; k < 9; k++)
            {
                if (dispo[k])
                {
                    Grille temp = new Grille(this);
                    temp.Set(i, j, (k + 1));
                    Grille[] rTemp = temp.Solve();
                    int taille = res.Length + rTemp.Length;
                    Grille[] resTemp = new Grille[taille];
                    for(int a = 0; a < taille; a++)
                        resTemp[a] = (a < res.Length) ? res[a] : rTemp[a - res.Length];
                    res = resTemp;
                }
            }
            return res;
        }

        //Retourne la liste des solutions possibles
        public int NbSolve()
        {
            //On regarde pour quelle case on a le moins de possibilités
            //On en profite pour remplir toutes les cases avec une seule possibilites
            //Si on a rempli au moins une case, les possibilités sont diminuées donc on recommence
            int i, j;
            bool valeursSimples;
            Console.WriteLine("Nb possibles : " + m_cases[4, 4].NbDispo());
            do
            {
                //On fait les vérifications à chaque itération qui a pu changer une valeur
                //Si la grille est pleine et sans incohérance, on retourne 1
                if (Possible() && Remplie())
                    return 1;
                //Si elle n'est pas cohérente, on retourne 0
                if (!Possible())
                    return 0;
                //On regarde les possibilités
                i = 0;
                j = 0;
                //On réinitialise notre valeur pour ne pas avoir de boucle infinie
                valeursSimples = false;
                for (int x = 0; (x < 9); x++)
                {
                    for (int y = 0; (y < 9); y++)
                    {
                        if (m_cases[x, y].NbDispo() == 0)
                            return 0;
                        if (m_cases[x, y].NbDispo() == 1)
                        {
                            bool[] temp = Possibilites(x, y);
                            valeursSimples = true;
                            for (int z = 0; z < 9; z++)
                            {
                                if (temp[z])
                                    Set(x, y, (z + 1));
                            }
                        }
                        else if ((!valeursSimples) && (m_cases[x, y].NbDispo() < m_cases[i, j].NbDispo()))
                        {
                            i = x;
                            j = y;
                        }
                    }
                }
            } while (valeursSimples);
            //On fait en fonction des possibilités disponnibles
            bool[] dispo = Possibilites(i, j);
            int res = 0;
            for (int k = 0; k < 9; k++)
            {
                Console.WriteLine((1 + k) + " " + (dispo[k] ? "dispo" : "pas dispo"));
                if (dispo[k])
                {
                    Console.WriteLine("Supposition : " + (k+1));
                    Grille temp = new Grille(this);
                    temp.Set(i, j, (k + 1));
                    res += temp.NbSolve();
                }
            }
            return res;
        }

        //Regarde si la grille est remplie
        public bool Remplie()
        {
            //Regarde si une grille est remplie
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if (Get(i, j) == 0)
                        return false;
                }
            }
            return true;
        }

        //Calcule le nombre de possibilité pour chaque case (10 pour les case déjà remplies pour faciliter les calculs)
        private int[,] Possibilites()
        {
            int[,] tab = new int[9, 9];
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    tab[i, j] = (m_cases[i, j].Valeur() == 0) ? m_cases[i, j].NbDispo() : 10;
                }
            }
            return tab;
        }

        //Calcule les nombres possibles pour une case donnée
        private bool[] Possibilites(int i, int j)
        {
            return m_cases[i, j].Dispo();
        }
    }
}
