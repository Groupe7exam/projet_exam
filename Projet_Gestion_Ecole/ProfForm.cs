using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projet_Gestion_Ecole.DAO;

namespace Projet_Gestion_Ecole
{
    public partial class ProfForm: Form
    {
        public ProfForm()
        {
            InitializeComponent();
        }

        private void btnEtudiants_Click(object sender, EventArgs e)
        {
            FormManager.showEtudiantForm(ShowPanel);
        }

        private void btnClasses_Click(object sender, EventArgs e)
        {
            FormManager.showClasseForm(ShowPanel);
        }

        private void btnNotes_Click(object sender, EventArgs e)
        {
            FormManager.showNoteForm(ShowPanel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Réinitialiser la session utilisateur
            SessionUtilisateur.Deconnecter();  // Assurez-vous que vous avez cette méthode pour nettoyer la session

            // Rediriger vers le formulaire de connexion
            FormManager.showLoginForm();  // Si vous avez une méthode qui gère l'affichage du formulaire de connexion
            this.Close();
        }
    }
}
