using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_R_J
{
    public class Chiffre
    {
        //un chiffre comprend une valeur comprise entre 1 et 10
        protected int C_valeur;

        public Chiffre()
        {

        }

        public Chiffre(int n)
        {
            if (n >= 1 && n <= 9)
                this.C_valeur = n;        
        }

        public int GetValeur()
        {
            return this.C_valeur;
        }

        public void SetValeur(int n)
        {
            this.C_valeur = n;
        }

        public virtual bool EstValide()
        {
            return true;
        }
        
    }
}
