using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_R_J
{
    class Jeu
    {
        private Chiffre[,] tab_jeu;

        public Jeu()
        {
            this.tab_jeu = new Chiffre[9, 9];
        }

        public Chiffre GetElement(int i, int j)// i--> ligne // j--> colonne
        {
            return this.tab_jeu[i, j];
        }

        public void SetChiffreCache(int i, int j, Chiffre c)
        {
            this.tab_jeu[i, j] = new Chiffre_Cache(c.GetValeur() );
        }

        public void SetChiffreVisible(int i, int j, Chiffre c)
        {
            this.tab_jeu[i, j] = new Chiffre_Visible(c.GetValeur());
        }

        public void VerifieTab()
        {
            for(int i=0; i<9; i++)
            {
                for(int j=0; j<9; j++)

                {
                    if( this.tab_jeu[i,j].EstValide() )
                    {
                        this.tab_jeu[i, j] = new Chiffre_Cache( this.tab_jeu[i,j].GetValeur() );
                    }
                }
            }
        }
    }
}
