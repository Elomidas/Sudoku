using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Metier;

namespace Sudoku_R_J
{
    public partial class FJeu : Form
    {
        //Grille de jeu utilisée
        private Jeu jeu;
        //Booléen pour savoir si le jeu est terminé ou non
        private bool m_fin;
        //Couleur des valeurs fixées
        private static Color m_cVisib = Color.FromKnownColor(KnownColor.ControlDarkDark);
        //Couleur des valeurs rentrées par l'utilisateur
        private static Color m_cCache = Color.FromKnownColor(KnownColor.Black);

        //Constructeur par défaut
        public FJeu()
        {
            InitializeComponent();
            m_fin = true;
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).BorderStyle = BorderStyle.None;
                }
            }
        }

        //Accesseur
        public Jeu GetJeu()
        {
            return this.jeu;
        }

        //Fixe un chiffre comme étant caché (à remplir par l'utilisateur)
        public void SetCaseCache(int i, int j, Chiffre c)
        {
            this.GetJeu().SetChiffreCache(i,j,c);
            ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).ReadOnly = false;

        }

        //Fixe un chiffre comme étant visible (non modifiable)
        public void SetCaseVisible(int i, int j, Chiffre c)
        {
            this.GetJeu().SetChiffreVisible(i,j,c);
            ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).ReadOnly = true;
        }

        //Action déclenchée par le changement de la valeur dans une TextBox
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (jeu != null)
            {
                TextBox tb = (TextBox)sender;
                String text = tb.Text;
                int pos = tb.TabIndex;
                int i = pos / 9;
                int j = pos % 9;
                int nbr;
                switch (text)
                {
                    case "1":
                        nbr = 1;
                        break;

                    case "2":
                        nbr = 2;
                        break;

                    case "3":
                        nbr = 3;
                        break;

                    case "4":
                        nbr = 4;
                        break;

                    case "5":
                        nbr = 5;
                        break;

                    case "6":
                        nbr = 6;
                        break;

                    case "7":
                        nbr = 7;
                        break;

                    case "8":
                        nbr = 8;
                        break;

                    case "9":
                        nbr = 9;
                        break;

                    case "":
                        nbr = 0;
                        break;

                    default:
                        nbr = 0;
                        tb.Text = "";
                        break;
                }
                //Sauvegarde la valeur saisie dans m_val_tapee
                GetJeu().GetElement(i,j).SetValTapee(nbr);
            }
        }

        //Permet de générer un nouveau jeu, en précisant la difficulté voulue
        private void GenererJeu(int difficulte)
        {
            bool choix = true;
            if(!m_fin)
            {
                try
                {
                    //On affiche un message demandant si le joueur veut abandonner la partie actuelle
                    DialogResult res = MessageBox.Show("La grille actuelle n'est pas terminée ! Voulez-vous vraiment l'abandonner et en commencer une nouvelle ?", "Abandon !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    choix = res == System.Windows.Forms.DialogResult.Yes;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("\n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (choix)
            {
                jeu = Jeu.Generer(difficulte);
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        String s = this.GetJeu().ValeurString(i, j);
                        //On fait en sorte que la TextBox affiche la valeur en mémoire
                        ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).Text = s;
                        //Seule les cases à remplir par l'utilisateur sont modifiables
                        ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).ReadOnly = !GetJeu().Cache(i, j);
                        if (!GetJeu().Cache(i, j))
                        {//Chiffre visible, fixé
                            //On fixe la police en gras
                            ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            //On change la couleur du chiffre
                            ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).ForeColor = m_cVisib;
                        }
                        else
                        {//Chiffre caché, à remplir par l'utilisateur
                            //On fixe la police à la normale
                            ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
                            //On change la couleur du chiffre
                            ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).ForeColor = m_cCache;
                        }
                        ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).BackColor = Color.FromKnownColor(KnownColor.Window);
                    }
                }
                m_fin = false;
            }
        }

        //Permet de créer un jeu de difficulté 1
        private void niveau1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenererJeu(1);
        }

        //Permet de créer un jeu de difficulté 2
        private void niveau2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenererJeu(2);
        }

        //Permet de créer un jeu de difficulté 3
        private void niveau3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenererJeu(3);
        }

        //Permet de quitter l'application
        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool choix = true;
            if (!m_fin)
            {
                try
                {
                    //On affiche un message demandant si le joueur veut abandonner la partie actuelle
                    DialogResult res = MessageBox.Show("La grille actuelle n'est pas terminée ! Voulez-vous vraiment l'abandonner ?\nLa fermeture de l'application entrainera la perte de toute votre progression.", "Quitter", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    choix = res == DialogResult.Yes;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("\n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if(choix)
                this.Close();
        }

        //Permet de tester la grille, et savoir si on a réussi le sudoku
        private void testerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //On vérifie d'abbord que l'utilisateur n'utilise pas cette option avant de créer un jeu
            if (jeu != null)
            {
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
                else if (jeu.VerifieTab())
                {
                    try
                    {
                        //On affiche un message indiquant que la grille est juste 
                        MessageBox.Show("Bravo, votre grille est correcte !", "Grille correcte !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        m_fin = true;
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
                        //On affiche un message indiquant que la grille n'est pas juste 
                        MessageBox.Show("Veuillez recommencer.", "Votre grille est incorrecte !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //On remet les valeurs tapées (contenues dans Chiffre_Cache) à 0
                        GetJeu().ReInit();
                        for(int i=0;i<9;i++)
                        {
                            for(int j =0;j<9;j++)
                            {
                                if(GetJeu().GetElement(i,j).EstCache())
                                {
                                    //On réactualise les textbox afin d'enlever les valeurs saisies préalablement
                                    String s = this.GetJeu().ValeurString(i, j);
                                    ((TextBox)tableLayoutPanel1.GetControlFromPosition(j, i)).Text = s;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("\n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
