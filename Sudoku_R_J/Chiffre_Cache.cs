using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_R_J
{
    public class Chiffre_Cache : Chiffre
    {
        //Chiffre qui ne sera pas affiché au début du jeu

        private int C_valeur_tapee;//Valeur saisie par l'utilisateur

        public Chiffre_Cache() : base()
        {
            C_valeur_tapee = 0;
        }

        public Chiffre_Cache(int n) : base(n)
        {
            C_valeur_tapee = 0;
        }

        public int GetValTapee()
        {
            return this.C_valeur_tapee;
        }

        public void SetValTapee(int v)
        {
            this.C_valeur_tapee = v;
        }

        public override bool EstValide()
        {
            if (this.GetValTapee() == this.GetValeur())
                return true;
            else
                return false;
        }

        public void Affichage()
        {
            
        }
    }
}
