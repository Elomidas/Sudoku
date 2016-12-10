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
            j = new Jeu();
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
            
            if(this.GetJeu().VerifieTab())
            {
                for(int i=0;i<9;i++)
                {
                    for(int j=0;j<9;j++)
                    {
                       // ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).Enabled = false;
                    }
                }
            }
            
            SetCaseVisible(0, 0, new Chiffre(1));
            ((TextBox)tableLayoutPanel1.GetControlFromPosition(1, 1)).Text = "8";
        }
    }
}
