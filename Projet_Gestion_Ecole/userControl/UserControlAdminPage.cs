using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Projet_Gestion_Ecole.DAO;

namespace Projet_Gestion_Ecole.userControl
{
    public partial class UserControlAdminPage : UserControl
    {
        public UserControlAdminPage()
        {
            InitializeComponent();
        }

        private void UserControlAdminPage_Load(object sender, EventArgs e)
        {
            refresh();
        }

        public void refresh()
        {
            dataGridView1.DataSource = null;
            using (var db = new DBconnect())
            {
                dataGridView1.DataSource = db.Utilisateurs.ToList();
            }
            CustomiserDataGridView();
        }
        private void CustomiserDataGridView()
        {
            dataGridView1.EnableHeadersVisualStyles = false;

            // 🔹 Style de l'en-tête
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(23, 24, 29); // Couleur personnalisée (RGB(23, 24, 29))
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);

            // 🔹 Style des lignes (effet zébré)
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.DefaultCellStyle.BackColor = Color.White;

            // 🔹 Bordures et style de grille
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.GridColor = Color.Gray;

            // 🔹 Alignement du texte
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // 🔹 Taille automatique des colonnes
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 🔹 Espacement des cellules
            dataGridView1.DefaultCellStyle.Padding = new Padding(5);

            // 🔹 Changement de couleur au survol
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(23, 24, 29); // Couleur personnalisée (RGB(23, 24, 29))
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            // 🔹 Désactiver la sélection multiple
            dataGridView1.MultiSelect = false;

            // 🔹 Désactiver l'ajout de nouvelles lignes
            dataGridView1.AllowUserToAddRows = false;

            // 🔹 Ajuster la hauteur des lignes
            dataGridView1.RowTemplate.Height = 30;
        }
        public void clear()
        {
            textNom.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var db = new DBconnect())
            {
                string nom = textNom.Text;
                string telephone = txtTel.Text.Trim();
                string motDePasse = txtPassword.Text.Trim();
                string Role = cmbRole.Text;

                // Vérification des champs
                if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(Role) || string.IsNullOrEmpty(telephone) || string.IsNullOrEmpty(motDePasse))
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
                        MotDePasse = motDePasseHache, // On stocke le mot de passe sécurisé
                        Role = cmbRole.Text
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

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                // Récupère l'ID de la personne sélectionnée
                int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;

                using (var db = new DBconnect())
                {
                    // Recherche la personne dans la base de données
                    var user = db.Utilisateurs.Find(id);

                    if (user != null)
                    {
                        // Remplit les champs du formulaire avec les données de la personne
                        textNom.Text = user.NomUtilisateur;
                        txtTel.Text = user.Telephone;
                        
                    }
                    else
                    {
                        MessageBox.Show("utilisateur non trouvée.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne valide.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (var db = new DBconnect())
            {
                string nom = textNom.Text;
                string telephone = txtTel.Text.Trim();
                string motDePasse = txtPassword.Text.Trim();
                string Role = cmbRole.Text;

                if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(telephone) || string.IsNullOrEmpty(motDePasse) )
                    {
                        MessageBox.Show("veuillez remplir tous les champs", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                    string motDePasseHache = BCrypt.Net.BCrypt.HashPassword(motDePasse);
                    int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
                        var p1 = db.Utilisateurs.Find(id);
                        p1.NomUtilisateur = nom;
                        p1.Telephone = telephone;
                        p1.MotDePasse = motDePasseHache;
                        p1.Role = Role;


                    db.SaveChanges();
                        MessageBox.Show("Personne Modifier avec succes", "succés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        refresh();
                        clear();
                    }
                }
              
            }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (var db = new DBconnect())
            {
                int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;
                var personne = db.Utilisateurs.Find(id);

                db.Utilisateurs.Remove(personne);
                MessageBox.Show("utilisateur supprimer avec succés", "succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refresh();
                clear();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (var db = new DBconnect())
            {
                string searchValue = txtSearch.Text.Trim();

                // Rechercher dans la base
                var results = db.Utilisateurs
                    .Where(p => p.NomUtilisateur.Contains(searchValue) ||
                                p.NomUtilisateur.Contains(searchValue))
                    .ToList();

                // Afficher les résultats dans le DataGridView
                dataGridView1.DataSource = results;

                if (results.Count == 0)
                {
                    MessageBox.Show("Aucun résultat trouvé pour la recherche.");
                }
            }

        }
    }
    }









