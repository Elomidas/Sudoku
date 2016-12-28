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
        //Valeur à trouver par l'utilisateur

        //Valeur saisie par l'utilisateur
        private int m_valeur_tapee;

        public Chiffre_Cache() : base()
        {
            m_valeur_tapee = 0;
        }

        public Chiffre_Cache(int n) : base(n)
        {
            m_valeur_tapee = 0;
        }

        public int GetValTapee()
        {
            return m_valeur_tapee;
        }

        public void SetValTapee(int v)
        {
            m_valeur_tapee = v;
        }

        public override bool EstValide()
        {
            return (GetValTapee() == m_valeur);
        }

        public override String GetValeurString()
        {
            if (this.m_valeur_tapee == 0)
            {
                return "";            
            }
               
            else
                return this.m_valeur_tapee.ToString();
        }

        public override bool EstCache()
        {
            return true;
        }

        public override bool EstVisible()
        {
            return base.EstVisible();
        }


        public override int GetValeur()
        {
            return 0;
        }
    }
}
