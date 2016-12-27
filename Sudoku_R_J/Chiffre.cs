using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_R_J
{
    public class Chiffre
    {
        //un chiffre comprend une valeur comprise entre 1 et 9
        protected int m_valeur;
        protected bool[] m_dispo;

        //Test la validité d'un chiffre
        //S'il est entre 1 et 9, retourne le chiffre, sinon retourne 0
        public static int Valeur(int valeur)
        {
            return OK(valeur) ? valeur : 0;
        }

        //Retourne vrai si la valeur est comprise entre 1 et 9
        protected static bool OK(int valeur)
        {
            return ((valeur > 0) && (valeur <= 9));
        }

        //Remet la case à zéro
        public void Reset()
        {
            m_valeur = 0;
            for (int i = 0; i < 9; i++)
                m_dispo[i] = true;
        }

        //Initialise le chiffre
        protected void Init()
        {
            m_dispo = new bool[9];
            for (int i = 0; i < 9; i++)
                m_dispo[i] = true;
            m_valeur = 0;
        }

        //Constructeur par défaut
        public Chiffre()
        {
            Init();
        }

        //Constructeur surchargé
        public Chiffre(int n)
        {
            Init();
            SetValeur(n);   
        }

         
        public virtual int GetValeur()
        {
            return Valeur(m_valeur);
        }

        //Retourne la valeur du chiffre, 0 si elle n'est pas valide
        public virtual String GetValeurString()
        {
            return Valeur(m_valeur).ToString();
        }

        //Affecte une valeur au chiffre, 0 si la valeur n'est pas correcte
        public void SetValeur(int n)
        {
            m_valeur = Valeur(n);
            if (OK(n))
            {
                for (int i = 0; i < 9; i++)
                    m_dispo[i] = false;
            }
        }

        //Indique qu'une valeur est disponnible pour ce chiffre
        public void SetDispo(int valeur)
        {
            if (OK(valeur))
                m_dispo[valeur - 1] = true;
        }

        //Modifie le tableau des disponnibilités
        public void SetDispo(bool[] dispo)
        {
            for (int i = 0; i < 9; i++)
                m_dispo[i] = dispo[i];
        }

        //Indique qu'une valeur n'est pas disponnible pour ce chiffre
        public void SetNonDispo(int valeur)
        {
            if (OK(valeur))
                m_dispo[valeur - 1] = false;
        }

        public virtual bool EstValide()
        {
            return true;
        }

        //Retourne le nombre de possibilités pour ce chiffre, 10 si le chiffre est fixé pour faciliter les calculs
        public int NbDispo()
        {
            int res = 0;
            for (int i = 0; i < 9; i++)
                res += m_dispo[i] ? 1 : 0;
            return (m_valeur == 0) ? res : 10;
        }

        //Indique si une valeur est disponnible ou non
        public bool Dispo(int valeur)
        {
            return OK(valeur) ? m_dispo[valeur - 1] : false;
        }

        //Retourne le tableau des disponnibilités
        public bool[] Dispo()
        {
            return m_dispo;
        }

    }
}
