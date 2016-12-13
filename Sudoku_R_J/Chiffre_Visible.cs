using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_R_J
{
    public class Chiffre_Visible : Chiffre
    {
        //Chiffre qui sera affiché au début du jeu
        //Valeur fixée

        public Chiffre_Visible() : base()
        {

        }

        public Chiffre_Visible(int n) : base(n)
        {

        }

        public override bool EstValide()
        {
            return true;
        }

    }
}
