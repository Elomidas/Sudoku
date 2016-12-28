using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_R_J
{
    public partial class FJeu : Form
    {
        private Jeu jeu;

        public FJeu()
        {
            InitializeComponent();
        }

        public Jeu GetJeu()
        {
            return this.jeu;
        }


        public void SetCaseCache(int i, int j, Chiffre c)
        {
            this.GetJeu().SetChiffreCache(i,j,c);

        }

        public void SetCaseVisible(int i, int j, Chiffre c)
        {
            this.GetJeu().SetChiffreVisible(i,j,c);
            ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).Enabled=false;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            TextBox tb = (TextBox)sender;
            String text = tb.Text;
            int pos = tb.TabIndex;
            int i = (pos -1) / 9;
            int j = (pos -1) % 9;
            int nbr;
            switch (text)
            {
                case "1" : nbr = 1;
                    break;
                case "2" : nbr = 2;
                    break;
                case "3" : nbr = 3;
                    break;
                case "4" : nbr = 4;
                    break;
                case "5" : nbr = 5;
                    break;
                case "6" : nbr = 6;
                    break;
                case "7" : nbr = 7;
                    break;
                case "8" : nbr = 8;
                    break;
                case "9" : nbr = 9;
                    break;
                default : nbr = 0;
                    break;
            }
            //Console.WriteLine(pos + " " + i + " " + j + " " + nbr);
            if(!GetJeu().GetElement(i,j).EstVisible())
                this.GetJeu().SetChiffreCache(i, j, new Chiffre(nbr));
            
        }

        private void niveau1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            jeu = Jeu.Generer(1);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    String s = this.GetJeu().ValeurString(i, j);
                    ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).Text = s;

                }
            }
        }

        private void niveau2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            jeu = Jeu.Generer(2);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    String s = this.GetJeu().ValeurString(i, j);
                    ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).Text = s;

                }
            }
        }

        private void niveau3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            jeu = Jeu.Generer(3);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    String s = this.GetJeu().ValeurString(i, j);
                    ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).Text = s;

                }
            }
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void testerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            if (!jeu.Remplie())
            {
                try
                {
                    //On affiche un message indiquant que la grille n'est pas remplie 
                    MessageBox.Show("Veuillez la remplir avant de tester votre solution.", "La grille n'est pas entièrement remplie !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("\n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else if (jeu.Possible())
            {
                try
                {
                    //On affiche un message indiquant que la grille est juste 
                    MessageBox.Show("Bravo, votre grille est correcte !", "Grille correcte !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("\n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

            }
            else
            {
                try
                {
                    //On affiche un message indiquant que la grille est juste 
                    MessageBox.Show("Veuillez recommencer.", "Votre grille est incorrecte !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("\n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            */

            /*
            for(int i = 0;i<9;i++)
            {
                for(int j = 0; j<9; j++)
                {
                    Console.Write(jeu.Valeur(i, j) + " ");
                }
                Console.WriteLine();
            }
            */
            
            Chiffre y = new Chiffre(2);
            this.GetJeu().SetChiffreCache(0, 1, y);
            Console.WriteLine(GetJeu().GetElement(0,1).GetValeurString());
            
        }
    }
}
