using System;
using System.Windows.Forms;

namespace Projet_Gestion_Ecole
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        bool SideBarTransition = true;

        private void Transition_Tick(object sender, EventArgs e)
        {
            if (SideBarTransition)
            {
                SideBar.Width -= 10;
                if (SideBar.Width <= 54)
                {
                    SideBarTransition = false;
                    Transition.Stop();
                }
            }
            else
            {
                SideBar.Width += 10;
                if (SideBar.Width >= 273)
                {
                    SideBarTransition = true;
                    Transition.Stop();
                }
            }
        }

        private void btnHam_Click(object sender, EventArgs e)
        {
            Transition.Start();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            // Optionnel: Gérer le redimensionnement du formulaire
            this.Resize += AdminForm_Resize;
        }

        private void AdminForm_Resize(object sender, EventArgs e)
        {
            // Si tu veux gérer dynamiquement la taille de ton Panel ou de tes contrôles
            ShowPanel.Size = this.ClientSize; // Ajuste la taille du panel au redimensionnement du formulaire
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            FormManager.showAdminForm2(ShowPanel);
        }

        private void btnEtudiants_Click(object sender, EventArgs e)
        {
            // Logique pour afficher les étudiants
        }

        private void btnClasses_Click(object sender, EventArgs e)
        {
            // Logique pour afficher les classes
        }

        private void btnProfesseur_Click(object sender, EventArgs e)
        {
            // Logique pour afficher les professeurs
        }

        private void btnMatieres_Click(object sender, EventArgs e)
        {
            // Logique pour afficher les matières
        }

        private void btnNotes_Click(object sender, EventArgs e)
        {
            // Logique pour afficher les notes
        }
    }
}
