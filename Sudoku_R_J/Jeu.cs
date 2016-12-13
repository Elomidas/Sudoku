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

        protected int Valeur(int i, int j)
        {
            return IndexOK(i, j) ? m_tab_jeu[i, j].GetValeur() : 0;
        }

        protected bool IndexOK(int i, int j)
        {
            return ((i >= 0) && (i < 9) && (j >= 0) && (j < 9));
        }

        public bool Cache(int i, int j)
        {
            return IndexOK(i, j) && (m_tab_jeu[i, j] is Chiffre_Cache);
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
            if (OK(x) && OK(y))
            {
                m_tab_jeu[x, y].SetValeur(valeur);
                if (valeur != 0)
                {
                    //On informe les cases de la ligne et de la colonne que la valeur n'est plus dispo
                    for (int k = 0; k < 9; k++)
                    {
                        if (m_tab_jeu[x, k].Dispo(valeur))
                            m_tab_jeu[x, k].SetNonDispo(valeur);
                        if (m_tab_jeu[k, y].Dispo(valeur))
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
                            if (m_tab_jeu[pX, pY].Dispo(valeur))
                                m_tab_jeu[pX, pY].SetNonDispo(valeur);
                        }
                    }
                }
            }
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
                    if (m_tab_jeu[i, j].EstValide())
                    {
                        m_tab_jeu[i, j] = new Chiffre_Cache(this.m_tab_jeu[i, j].GetValeur());
                    }
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
                    if (Valeur(i, j) != 0)
                    {
                        test = dispoL[Valeur(i, j) - 1];
                        dispoL[Valeur(i, j) - 1] = false;
                    }
                    if (Valeur(j, i) != 0)
                    {
                        test = dispoC[Valeur(j, i) - 1];
                        dispoC[Valeur(j, i) - 1] = false;
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
                            if (Valeur((3 * i) + k, (3 * j) + l) != 0)
                            {
                                test = dispo[Valeur((3 * i) + k, (3 * j) + l) - 1];
                                dispo[Valeur((3 * i) + k, (3 * j) + l) - 1] = false;
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
                    if (Valeur(i, j) == 0)
                        return false;
                }
            }
            return true;
        }

        //Retourne la liste des solutions possibles
        protected Jeu[] Solve(Random rand)
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
                    return new Jeu[] { this };
                //Si elle n'est pas cohérente, on retourne 0
                //Une grille est incohérente si on a une case vide avec aucune possibilité ou plusieurs fois le même chiffre dans des carrés, colonnes ou lignes
                if (!Possible())
                    return new Jeu[0];
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
                for(int k = 0; valeur == 0; k++)
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
                res = temp.Solve(rand);
                if ((taille > 1) && (res.Length == 0))
                {
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

        //Retourne un Jeu avec une grille de sudoku générée
        static public Jeu Generer()
        {
            Random rand = new Random();
            Jeu res = new Jeu();
            Jeu[] tab = res.Solve(rand);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    Console.Write(tab[0].Valeur(i, j) + " ");
                Console.WriteLine();
            }
            return tab[0];
        }
    }
}
