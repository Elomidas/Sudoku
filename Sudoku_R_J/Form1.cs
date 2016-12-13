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
    public partial class Form1 : Form
    {
        Jeu m_jeu;

        public Form1()
        {
            InitializeComponent();
            m_jeu = new Jeu();
            Jeu res = Jeu.Generer();
        }
    }
}
