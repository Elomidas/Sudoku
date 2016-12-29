using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_R_J
{
    public class Jeu
    {
        private Chiffre[,] m_tab_jeu;

        public Jeu()
        {
            m_tab_jeu = new Chiffre[9, 9];
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    m_tab_jeu[i, j] = new Chiffre();
            }
        }

        public Jeu(Jeu copie)
        {
            m_tab_jeu = new Chiffre[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    m_tab_jeu[i, j] = new Chiffre(copie.Valeur(i, j));
            }
        }

        protected bool OK(int index)
        {
            return (index >= 0) && (index < 9);
        }

        //Retourne la valeur attendue pour la case visée
        public int Valeur(int i, int j)
        {
            return IndexOK(i, j) ? m_tab_jeu[i, j].GetValeur() : 0;
        }

        //Retourne la valeur visible pour la case visée
        public int ValeurVisible(int i, int j)
        {
            return IndexOK(i, j) ? m_tab_jeu[i, j].GetValeurVisible() : 0;
        }

        public String ValeurString(int i, int j)
        {
            return IndexOK(i, j) ? m_tab_jeu[i, j].GetValeurString() : "";
        }

        protected bool IndexOK(int i, int j)
        {
            return ((i >= 0) && (i < 9) && (j >= 0) && (j < 9));
        }

        public bool Cache(int i, int j)
        {
            return IndexOK(i, j) ? m_tab_jeu[i, j].EstCache() : false;
        }

        public Chiffre GetElement(int i, int j)// i--> ligne // j--> colonne
        {
            return IndexOK(i, j) ? m_tab_jeu[i, j] : new Chiffre();
        }
        
        public void SetChiffreCache(int i, int j, Chiffre c)
        {
            if (!IndexOK(i, j))
                return;
            m_tab_jeu[i, j] = new Chiffre_Cache(c.GetValeur());
        }

        public void SetChiffreVisible(int i, int j, Chiffre c)
        {
            if (!IndexOK(i, j))
                return;
            m_tab_jeu[i, j] = new Chiffre_Visible(c.GetValeur());
        }
        
        //Setter utilisé pour générer la grille de sudoku aléatoirement
        protected void Set(int x, int y, int valeur)
        {
            if (IndexOK(x, y))
            {
                m_tab_jeu[x, y].SetValeur(valeur);
                if (valeur != 0)
                {
                    //On informe les cases de la ligne et de la colonne que la valeur n'est plus dispo
                    for (int k = 0; k < 9; k++)
                    {
                        if (m_tab_jeu[x, k].Dispo(valeur) && (k != y))
                            m_tab_jeu[x, k].SetNonDispo(valeur);
                        if (m_tab_jeu[k, y].Dispo(valeur) && (k != x))
                            m_tab_jeu[k, y].SetNonDispo(valeur);
                    }
                    //On informe les cases du carré que la valeur n'est plus dispo
                    int i = x / 3;
                    int j = y / 3;
                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            int pX = (3 * i) + k;
                            int pY = (3 * j) + l;
                            if (m_tab_jeu[pX, pY].Dispo(valeur) && (pX != x) && (pY != y))
                                m_tab_jeu[pX, pY].SetNonDispo(valeur);
                        }
                    }
                }
            }
        }

        public void SetVisible(int i, int j, int v)
        {
            if (Cache(i, j))
                ((Chiffre_Cache)m_tab_jeu[i, j]).SetValTapee(v);
        }
        
        public void SetChiffreCache(int i, int j, int valeur)
        {
            if (!IndexOK(i, j))
                return;
            m_tab_jeu[i, j] = new Chiffre_Cache(Chiffre.Valeur(valeur));
        }

        public void SetChiffreVisible(int i, int j, int valeur)
        {
            if (!IndexOK(i, j))
                return;
            m_tab_jeu[i, j] = new Chiffre_Visible(Chiffre.Valeur(valeur));
        }
        
        public bool VerifieTab()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)

                {
                    if (! m_tab_jeu[i, j].EstValide())
                        return false;
                }
            }
            return true;
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
                    //On teste les lignes
                    if (ValeurVisible(i, j) != 0)
                    {
                        test = dispoL[ValeurVisible(i, j) - 1];
                        dispoL[ValeurVisible(i, j) - 1] = false;
                    }
                    //On teste les colonnes
                    if (test && (ValeurVisible(j, i) != 0))
                    {
                        test = dispoC[ValeurVisible(j, i) - 1];
                        dispoC[ValeurVisible(j, i) - 1] = false;
                    }
                }
            }
            //On teste tous les carrés
            for (int i = 0; (i < 3) && test; i++)
            {
                for (int j = 0; (j < 3) && test; j++)
                {
                    bool[] dispo = new bool[9];
                    for (int k = 0; k < 9; k++)
                        dispo[k] = true;
                    for (int k = 0; (k < 3) && test; k++)
                    {
                        for (int l = 0; (l < 3) && test; l++)
                        {
                            if (ValeurVisible((3 * i) + k, (3 * j) + l) != 0)
                            {
                                test = dispo[ValeurVisible((3 * i) + k, (3 * j) + l) - 1];
                                dispo[ValeurVisible((3 * i) + k, (3 * j) + l) - 1] = false;
                            }
                        }
                    }
                }
            }
            //On retourne enfin le résultat
            return test;
        }

        //Regarde si la grille est remplie
        public bool Remplie()
        {
            //Regarde si une grille est remplie
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (ValeurVisible(i, j) == 0)
                        return false;
                }
            }
            return true;
        }

        //Recalcule toutes les valeurs disponnibles pour chaque case non remplies
        protected void CalcDispo()
        {
            for(int i = 0; i  < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    if(ValeurVisible(i, j) == 0)
                    {
                        bool[] dispo = new bool[9];
                        for (int k = 0; k < 9; k++)
                            dispo[k] = true;
                        //On teste la ligne et la colonne
                        for (int k = 0; k < 9; k++)
                        {
                            if (ValeurVisible(k, j) != 0)
                                dispo[ValeurVisible(k, j) - 1] = false;
                            if (ValeurVisible(i, k) != 0)
                                dispo[ValeurVisible(i, k) - 1] = false;
                        }
                        //On teste le carré
                        for (int k = 0; k < 3; k++)
                        {
                            for (int l = 0; l < 3; l++)
                            {
                                if (ValeurVisible((3 * (i / 3)) + k, (3 * (j / 3)) + l) != 0)
                                    dispo[ValeurVisible((3 * (i / 3)) + k, (3 * (j / 3)) + l) - 1] = false;
                            }
                        }
                        m_tab_jeu[i, j].SetDispo(dispo);
                    }
                }
            }
        }

        //Retourne le nombre de grilles possibles avec les valeurs actuelles
        protected int Solve()
        {
            //On charche pour quelle case on a le moins de possibilités
            //Pour optimiser l'algorithme on remplie toutes les cases avec une seule possibilité
            //Si on a rempli au moins une case, les possibilités sont diminuées donc on recommence la recherche
            //Enfin s'il n'y a plus que des cases avec plusieurs possibilités, on selectionne celle qui en a le moins
            //On créé une nouvelle grille pour chaque possibilité de la case choisie et on réappelle cette fonction pour essayer de resoudre la grille
            int i, j;
            bool valeursSimples;
            do
            {
                //On fait les vérifications à chaque itération car une valeur a pu être entrée alors qu'elle est erronnée
                //Si la grille est pleine et sans incohérance, on retourne 1
                if (Possible() && Remplie())
                    return 1;
                //Si elle n'est pas cohérente, on retourne 0
                //Une grille est incohérente si on a une case vide avec aucune possibilité ou plusieurs fois le même chiffre dans des carrés, colonnes ou lignes
                if (!Possible())
                    return 0;
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
                        if (m_tab_jeu[x, y].NbDispo() == 0)
                            return 0;
                        if (m_tab_jeu[x, y].NbDispo() == 1)
                        {
                            //Si on trouve une case avec une seule valeur possible, on rentre cette valeur
                            bool[] temp = m_tab_jeu[x, y].Dispo();
                            //L'ajout d'une valeur a modifié les possibilités des cases sur ses ligne, colonne et carré
                            //Il faut donc revérifier les cases au dessus de celle-ci, c'est pourquoi on recommence la boucle
                            valeursSimples = true;
                            for (int z = 0; z < 9; z++)
                            {
                                if (temp[z])
                                {
                                    //Set(x, y, (z + 1));
                                    SetChiffreVisible(x, y, (z + 1));
                                }
                            }
                        }
                        else if ((!valeursSimples) && (m_tab_jeu[x, y].NbDispo() < m_tab_jeu[i, j].NbDispo()))
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
            bool[] dispo = m_tab_jeu[i, j].Dispo();
            int taille = m_tab_jeu[i, j].NbDispo();
            int res = 0;
            //On teste toutes les valeurs
            for (int k = 0; (k < 9) && (res < 2); k++)
            {
                if (dispo[k])
                {
                    //On regarde combien de grilles sont possibles avec cette valeur
                    Jeu temp = new Jeu(this);
                    temp.SetChiffreVisible(i, j, k+1);
                    res += temp.Solve();
                }
            }
            //res contient le nombre de grilles possibles avec les chiffres de départ (2 s'il y en a 2 ou plus)
            return res;
        }

        //Remplie aléatoirement une grille
        protected Jeu[] RempAlea(Random rand)
        {
            //On charche pour quelle case on a le moins de possibilités
            //Pour optimiser l'algorithme on remplie toutes les cases avec une seule possibilité
            //Si on a rempli au moins une case, les possibilités sont diminuées donc on recommence la recherche
            //Enfin s'il n'y a plus que des cases avec plusieurs possibilités, on selectionne celle qui en a le moins
            //On créé une nouvelle grille pour chaque possibilité de la case choisie et on réappelle cette fonction pour essayer de resoudre la grille
            int i, j;
            bool valeursSimples;
            do
            {
                //On fait les vérifications à chaque itération car une valeur a pu être entrée alors qu'elle est erronnée
                //Si la grille est pleine et sans incohérance, on retourne 1
                if (Possible() && Remplie())
                {
                    //Console.WriteLine("Fin normale");
                    return new Jeu[] { this };
                }
                //Si elle n'est pas cohérente, on retourne 0
                //Une grille est incohérente si on a une case vide avec aucune possibilité ou plusieurs fois le même chiffre dans des carrés, colonnes ou lignes
                if (!Possible())
                {
                    //Console.WriteLine("Fin impossible");
                    return new Jeu[0];
                }
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
                        if (m_tab_jeu[x, y].NbDispo() == 0)
                        {
                            return new Jeu[0];
                        }
                        if (m_tab_jeu[x, y].NbDispo() == 1)
                        {
                            //Si on trouve une case avec une seule valeur possible, on rentre cette valeur
                            bool[] temp = m_tab_jeu[x, y].Dispo();
                            //L'ajout d'une valeur a modifié les possibilités des cases sur ses ligne, colonne et carré
                            //Il faut donc revérifier les cases au dessus de celle-ci, c'est pourquoi on recommence la boucle
                            valeursSimples = true;
                            for (int z = 0; z < 9; z++)
                            {
                                if (temp[z])
                                    Set(x, y, (z + 1));
                            }
                        }
                        else if ((!valeursSimples) && (m_tab_jeu[x, y].NbDispo() < m_tab_jeu[i, j].NbDispo()))
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
            bool[] dispo = m_tab_jeu[i, j].Dispo();
            int taille = m_tab_jeu[i, j].NbDispo();
            bool continuer;
            Jeu[] res;
            do
            {
                //On choisie une valeur aléatoire parmi celles disponnibles
                continuer = false;
                int valRand = rand.Next(taille);
                int valeur = 0;
                for (int k = 0; valeur == 0; k++)
                {
                    if (dispo[k])
                    {
                        if (valRand == 0)
                            valeur = k + 1;
                        else valRand--;
                    }
                }
                //On regarde si on peut faire une grille avec cette valeur
                Jeu temp = new Jeu(this);
                temp.Set(i, j, valeur);
                res = temp.RempAlea(rand);
                if ((taille > 1) && (res.Length == 0))
                {
                    //Si aucune grille n'a pu être remplie avec cette valeur, on la supprime des possibilités et on recommence
                    taille--;
                    dispo[valeur - 1] = false;
                    continuer = true;
                }
            } while (continuer);
            //Lorsqu'on arrive là, on a deux possibilités
            // > res est un tableau d'une case contenant la grille aléatoire terminée
            // > res est un tableau de 0 case et le problème sera traité par la fonction précédente dans la récursion
            return res;
        }

        protected void Masquer(int difficulte, Random rand)
        {
            bool[] tab = new bool[81];
            for (int i = 0; i < 81; i++)
                tab[i] = true;
            int pos = 81;
            while(pos > 0)
            {
                //On choisi une case au hasard
                int c = rand.Next(pos);
                int x = -1, y = 0;
                for(int n = 0; x == -1; n++)
                {
                    if(tab[n])
                    {
                        if (c == 0)
                        {
                            x = n / 9;
                            y = n % 9;
                        }
                        else c--;
                    }
                }
                //On regarde si on peut retirer ce nombre
                bool[] dispo = new bool[9];
                for (int n = 0; n < 9; n++)
                    dispo[n] = true;
                //On regarde les valeurs sur la colonne ainsi que celles sur la ligne
                for(int n = 0; n < 9; n++)
                {
                    int v = ValeurVisible(x, n);
                    if ((v != 0) && (n != y))
                        dispo[v - 1] = false;
                    v = ValeurVisible(n, y);
                    if ((v != 0) && (n != x))
                        dispo[v - 1] = false;
                }
                //On regarde ensuite sur le carré
                int i = x / 3;
                int j = y / 3;
                for (int k = 0; (k < 3); k++)
                {
                    for (int l = 0; (l < 3); l++)
                    {
                        if (ValeurVisible((3 * i) + k, (3 * j) + l) != 0)
                            dispo[ValeurVisible((3 * i) + k, (3 * j) + l) - 1] = false;
                    }
                }
                //On compte le nombre de possibilités
                int res = 0;
                for (int n = 0; n < 9; n++)
                    res += dispo[n] ? 1 : 0;
                //Si on n'a pas plus de possibilités que le niveau de difficulté, on essaye d'enlever le chiffre
                if(res <= difficulte)
                {
                    //On créé une copie pour tester
                    Jeu temp = new Jeu(this);
                    //On enlève la valeur
                    temp.Set(x, y, 0);
                    temp.CalcDispo();
                    if (1 == temp.Solve())
                    {
                        //Si on a toujours une seule grille possible, on enlève la valeur
                        SetChiffreCache(x, y, Valeur(x, y));
                    }
                    else
                    {
                        //Sinon elle est necessaires : on la fixe
                        SetChiffreVisible(x, y, Valeur(x, y));
                    }
                }
                else
                {
                    //Sinon la valeur ne doit pas être modifiée : on la garde
                    SetChiffreVisible(x, y, Valeur(x, y));
                }
                tab[(9 * x) + y] = false;
                pos--;
            }
        }

        //Retourne un Jeu avec une grille de sudoku générée
        //Difficulté (d) comprise entre 1 et 8 (1 très simple, 8 très compliqué)
        static public Jeu Generer(int d)
        {
            Random rand = new Random();
            Jeu res = new Jeu();
            Jeu[] tab = res.RempAlea(rand);
            res = tab[0];
            //Arrivé ici, res contient une grille complète
            //On détermine les chiffres que l'ont peut enlever
            //Si la difficulté n'est pas une valeur correcte, on choisi 1 par défaut
            res.Masquer(((d > 0) && (d <= 9)) ? d : 1, rand);
            return res;
        }
    }
}
