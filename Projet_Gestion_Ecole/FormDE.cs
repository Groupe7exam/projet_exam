using DocumentFormat.OpenXml.Presentation;
using Projet_Gestion_Ecole.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_Gestion_Ecole
{
    public partial class FormDE: Form
    {
        public FormDE()
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
                    //Transition.Stop();
                }
            }
            else
            {
                SideBar.Width += 10;
                if (SideBar.Width >= 273)
                {
                    SideBarTransition = true;
                    //Transition.Stop();
                }
            }
        }

        private void btnHam_Click(object sender, EventArgs e)
        {
            //Transition.Start();
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


      

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormDE_Load(object sender, EventArgs e)
        {

        }

        private void btnEtudiants_Click_1(object sender, EventArgs e)
        {
            FormManager.showEtudiantForm(ShowPanel);
        }

        private void btnClasses_Click_1(object sender, EventArgs e)
        {
            FormManager.showClasseForm(ShowPanel);
        }

        private void btnProfesseur_Click_1(object sender, EventArgs e)
        {
            FormManager.showProfesseurForm(ShowPanel);
        }

        private void btnMatieres_Click_1(object sender, EventArgs e)
        {
            FormManager.showMatiereForm(ShowPanel);
        }

        private void btnNotes_Click_1(object sender, EventArgs e)
        {
            FormManager.showNoteForm(ShowPanel);
        }

        private void btnCours_Click_1(object sender, EventArgs e)
        {
            FormManager.showCoursForm(ShowPanel);
        }

        private void btnRapport_Click_1(object sender, EventArgs e)
        {
            FormManager.showRapportForm(ShowPanel);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Réinitialiser la session utilisateur
            SessionUtilisateur.Deconnecter();  // Assurez-vous que vous avez cette méthode pour nettoyer la session

            // Rediriger vers le formulaire de connexion
            FormManager.showLoginForm();  // Si vous avez une méthode qui gère l'affichage du formulaire de connexion
            this.Close();
        }
    }
}
