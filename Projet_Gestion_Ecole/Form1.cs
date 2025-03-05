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
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }













































        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void InscriptionPage_Click(object sender, EventArgs e)
        {
            FormManager.showInscriptionForm();
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            string nomUser = txtUser.Text;
            string password = txtPassword.Text;

            // Vérification que les champs ne sont pas vides
            if (string.IsNullOrEmpty(nomUser) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Veuillez remplir tous les champs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var db = new DBconnect())
            {
                var user = db.Utilisateurs.FirstOrDefault(u => u.NomUtilisateur == nomUser);
                if (user != null)
                {
                    // Affichage d'un message pour vérifier que l'utilisateur est trouvé
                    MessageBox.Show("Utilisateur trouvé, vérification du mot de passe...");

                    // Comparaison du mot de passe avec celui haché dans la base de données
                    if (BCrypt.Net.BCrypt.Verify(password, user.MotDePasse))
                    {
                        // Affichage d'un message de succès pour vérifier si la connexion est réussie
                        MessageBox.Show("Mot de passe correct, redirection...");

                        // Vérification du rôle et ouverture du formulaire correspondant
                        if (user.Role == "ADMIN")
                        {
                            MessageBox.Show("Redirection vers Admin");
                            FormManager.showAdminForm();
                        }
                        else if (user.Role == "DE")
                        {
                            MessageBox.Show("Redirection vers DE");
                            FormManager.showFormDE();
                        }
                        else if (user.Role == "Agent")
                        {
                            MessageBox.Show("Redirection vers Agent");
                            FormManager.showAgentForm();
                        }
                        else if (user.Role == "Professeur")
                        {
                            MessageBox.Show("Redirection vers Professeur");
                            FormManager.showProfForm();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mot de passe incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Nom d'utilisateur invalide", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}
