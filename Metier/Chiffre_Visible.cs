using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Chiffre_Visible : Chiffre
    {
        //Chiffre qui sera affiché au début du jeu
        //Valeur fixée

        //Constructeur de base
        public Chiffre_Visible() : base()
        {

        }

        //Constructeur surchargé
        public Chiffre_Visible(int n) : base(n)
        {

        }

        //Retourne la valeur du chiffre, 0 si elle n'est pas valide sous forme de chaîne de caractères
        public override String GetValeurString()
        {
            return this.m_valeur.ToString();
        }

        //booléen vérifiant si le chiffre est caché
        public override bool EstCache()
        {
            return base.EstCache();
        }

        //booléen vérifiant si le chiffre est visible
        public override bool EstVisible()
        {
            return true;
        }

        //Indique si un chiffre est valide dans la grille
        public override bool EstValide()
        {
            return true;
        }

    }
}
