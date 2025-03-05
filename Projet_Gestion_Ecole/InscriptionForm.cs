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
    public partial class InscriptionForm : Form
    {
        public InscriptionForm()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnInscription_Click(object sender, EventArgs e)
        {

            using (var db = new DBconnect())
            {
                string nom = txtUser.Text.Trim();
                string telephone = txtTel.Text.Trim();
                string motDePasse = txtPassword.Text.Trim();

                // Vérification des champs
                if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(telephone) || string.IsNullOrEmpty(motDePasse))
                {
                    MessageBox.Show("Veuillez remplir tous les champs", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Vérifier si l'utilisateur ou le téléphone existent déjà
                if (db.Utilisateurs.Any(u => u.NomUtilisateur == nom))
                {
                    MessageBox.Show("Ce nom d'utilisateur est déjà pris", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (db.Utilisateurs.Any(u => u.Telephone == telephone))
                {
                    MessageBox.Show("Ce numéro de téléphone est déjà utilisé", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    // Hacher le mot de passe avant de l'enregistrer
                    string motDePasseHache = BCrypt.Net.BCrypt.HashPassword(motDePasse);

                    Utilisateur u = new Utilisateur
                    {
                        NomUtilisateur = nom,
                        Telephone = telephone,
                        MotDePasse = motDePasseHache // On stocke le mot de passe sécurisé
                    };

                    db.Utilisateurs.Add(u);
                    db.SaveChanges();

                    MessageBox.Show("Utilisateur enregistré avec succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de l'inscription : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ConnexionPage_Click(object sender, EventArgs e)
        {
            FormManager.showLoginForm();
        }

        private void InscriptionForm_Load(object sender, EventArgs e)
        {

        }
    }
}
