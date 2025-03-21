using System;
using System.Linq;
using System.Windows.Forms;
using Projet_Gestion_Ecole.DAO;
using System.Data.Entity;
using System.Collections.Generic;

namespace Projet_Gestion_Ecole.userControl
{
    public partial class UserControlProfesseur : UserControl
    {
        public UserControlProfesseur()
        {
            InitializeComponent();
            Refresh();
            refreshCmb();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Veuillez sélectionner une ligne valide", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
            using (var db = new DBconnect())
            {
                var c = db.Professeurs.Find(id);
                if (c != null)
                {
                    txtNom.Text = c.Nom;
                    Prenomtxt.Text = c.Prenom;
                    txtTelephone.Text = c.Telephone;
                    txtEmail.Text = c.Email;
                }
                else
                {
                    MessageBox.Show("Professeur non trouvé", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string motDePasse = txtPassword.Text.Trim();
            using (var db = new DBconnect())
            {
                // Créer un nouveau professeur
                var nouveauProfesseur = new Professeur
                {
                    Nom = txtNom.Text.Trim(),
                    Prenom = Prenomtxt.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Telephone = txtTelephone.Text.Trim(),
                    ProfesseurMatieres = new List<ProfesseurMatiere>()
                };
                string motDePasseHache = BCrypt.Net.BCrypt.HashPassword(motDePasse);
                var user = new Utilisateur
                {
                    NomUtilisateur = txtNom.Text.Trim(),
                    MotDePasse = motDePasseHache,
                    Role = "Professeur",
                    Telephone = txtTelephone.Text.Trim()
                };

                // Vérifier la sélection dans les CheckedListBox
                if (checkedListBoxClasses.CheckedItems.Count == 0 || checkedListBoxMatiere.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner au moins une classe et une matière.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Parcourir les classes et matières sélectionnées
                foreach (ComboBoxItem classeItem in checkedListBoxClasses.CheckedItems)
                {
                    foreach (ComboBoxItem matiereItem in checkedListBoxMatiere.CheckedItems)
                    {
                        var professeurMatiere = new ProfesseurMatiere
                        {
                            Professeur = nouveauProfesseur,
                            ClasseId = classeItem.Id,
                            MatiereId = matiereItem.Id
                        };

                        nouveauProfesseur.ProfesseurMatieres.Add(professeurMatiere);
                    }
                }

                // Ajouter le professeur et sauvegarder
                db.Professeurs.Add(nouveauProfesseur);
                db.Utilisateurs.Add(user);
                db.SaveChanges();

                MessageBox.Show("Professeur ajouté avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
                refresh();
            }
        }


        public void refresh()
        {
            using (var db = new DBconnect())
            {
                var professeurs = db.Professeurs.ToList();

                // Récupérer ProfesseurMatieres
                var professeurMatieres = db.ProfesseurMatieres.Include(pm => pm.Matiere).Include(pm => pm.Classe).ToList();

                var result = professeurs.Select(prof => new
                {
                    prof.Id,
                    prof.Nom,
                    prof.Prenom,
                    prof.Email,
                    prof.Telephone,
                    Matieres = professeurMatieres
                        .Where(pm => pm.ProfesseurId == prof.Id)
                        .Select(pm => pm.Matiere.NomMatiere) // Assurez-vous de récupérer le bon nom de matière
                        .Distinct()
                        .ToList(),
                    Classes = professeurMatieres
                        .Where(pm => pm.ProfesseurId == prof.Id)
                        .Select(pm => pm.Classe.NomClasse) // Assurez-vous de récupérer le bon nom de classe
                        .Distinct()
                        .ToList()
                }).ToList();

                // Transformation des données pour l'affichage
                var finalResult = result.Select(p => new
                {
                    p.Id,
                    p.Nom,
                    p.Prenom,
                    p.Email,
                    p.Telephone,
                    MatieresAssociees = p.Matieres.Any() ? string.Join(", ", p.Matieres) : "Aucune matière associée",
                    ClassesAssociees = p.Classes.Any() ? string.Join(", ", p.Classes) : "Aucune classe associée"
                }).ToList();

                // Mise à jour du DataGridView
                dataGridView1.DataSource = finalResult;
            }
        }

        public void refreshCmb()
        {
            using (var db = new DBconnect())
            {
               
            }
        }

        public void clear()
        {
            txtNom.Clear();
            Prenomtxt.Clear();
            txtEmail.Clear();
            txtTelephone.Clear();
          
        }

        // Méthode pour modifier un professeur
        private void btnModifier_Click(object sender, EventArgs e)
        {
            using (var db = new DBconnect())
            {
                if (dataGridView1.CurrentRow == null)
                {
                    MessageBox.Show("Veuillez sélectionner un professeur à modifier.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
                var professeur = db.Professeurs.Include(p => p.ProfesseurMatieres).FirstOrDefault(p => p.Id == id);

                if (professeur == null)
                {
                    MessageBox.Show("Professeur non trouvé.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                professeur.Nom = txtNom.Text.Trim();
                professeur.Prenom = Prenomtxt.Text.Trim();
                professeur.Email = txtEmail.Text.Trim();
                professeur.Telephone = txtTelephone.Text.Trim();

                professeur.ProfesseurMatieres.Clear();

                foreach (ComboBoxItem classeItem in checkedListBoxClasses.CheckedItems)
                {
                    foreach (ComboBoxItem matiereItem in checkedListBoxMatiere.CheckedItems)
                    {
                        professeur.ProfesseurMatieres.Add(new ProfesseurMatiere
                        {
                            ProfesseurId = professeur.Id,
                            ClasseId = classeItem.Id,
                            MatiereId = matiereItem.Id
                        });
                    }
                }

                db.SaveChanges();
                MessageBox.Show("Professeur modifié avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
                refresh();
            }
        }

        private void UserControlProfesseur_Load(object sender, EventArgs e)
        {
            refresh();
            refreshCmb();
            LoadCheckedListBoxes();
        }

   
        private void LoadCheckedListBoxes()
        {
            using (var db = new DBconnect())
            {
                checkedListBoxClasses.Items.Clear();
                var classes = db.Classes.ToList();
                foreach (var classe in classes)
                {
                    checkedListBoxClasses.Items.Add(new ComboBoxItem { Id = classe.Id, Nom = classe.NomClasse });
                }


            }
            using (var db = new DBconnect())
            {
                checkedListBoxMatiere.Items.Clear();
                var matiere = db.Matieres.ToList();
                foreach (var mat in matiere)
                {
                    checkedListBoxMatiere.Items.Add(new ComboBoxItem { Id = mat.Id, Nom = mat.NomMatiere });
                }


            }


        }

        private class ComboBoxItem
        {
            public int Id { get; set; }
            public string Nom { get; set; }
            public override string ToString() => Nom;
        }



































        private void cmbClasse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBoxClasses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}