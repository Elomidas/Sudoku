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
        private Jeu j;

        public FJeu()
        {
            InitializeComponent();
            j = Jeu.Generer(1);
        }

        public Jeu GetJeu()
        {
            return this.j;
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

        }

        private void test_Click(object sender, EventArgs e)
        {

            
            for(int i=0;i<9;i++)
            {
                for(int j=0;j<9;j++)
                {
                    Type t = this.GetJeu().GetElement(i, j).GetType();
                    if (t.Equals(typeof(Sudoku_R_J.Chiffre_Visible)))
                    {
                        ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).Text = "1";
                        
                    }
                    if (t.Equals(typeof(Sudoku_R_J.Chiffre_Cache)))
                    {
                        ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).Text = "0";

                    }

                }
            }
            
            /*
            this.GetJeu().SetChiffreCache(0, 0, new Chiffre(1));
            this.GetJeu().SetChiffreVisible(0, 1, new Chiffre(2));
            String s = this.GetJeu().ValeurString(0, 0);
            ((TextBox)tableLayoutPanel1.GetControlFromPosition(0, 0)).Text = s;
            String s1 = this.GetJeu().ValeurString(0, 1);
            ((TextBox)tableLayoutPanel1.GetControlFromPosition(1, 0)).Text = s1;
            */




        }


    }
}
