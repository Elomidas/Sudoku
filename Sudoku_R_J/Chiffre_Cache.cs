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

        //Constructeur de base
        public Chiffre_Cache() : base()
        {
            m_valeur_tapee = 0;
        }

        public Chiffre_Cache(int n) : base(n)
        {
            m_valeur_tapee = 0;
        }

        //Retourne la valeur entrée par l'utilisateur
        public int GetValTapee()
        {
            return m_valeur_tapee;
        }

        //Renseigne la valeur entrée par l'utilisateur
        public void SetValTapee(int v)
        {
            m_valeur_tapee = Valeur(v);
        }

        //Indique si la valeur tapée correspond à la valeur attendue
        public override bool EstValide()
        {
            return (GetValTapee() == m_valeur);
        }

        //Retourne la valeur tapée sous la forme d'une chaine de caractère
        public override String GetValeurString()
        {
            if (this.m_valeur_tapee == 0)
            {
                return "";            
            }
            else return this.m_valeur_tapee.ToString();
        }

        //Indique que le chiffre est caché
        public override bool EstCache()
        {
            return true;
        }

        //Indique que le chiffre n'est pas visible
        public override bool EstVisible()
        {
            return base.EstVisible();
        }

        //Retourne la valeur devant être affichée à l'écran par ce chiffre
        public override int GetValeurVisible()
        {
            return GetValTapee();
        }
    }
}
