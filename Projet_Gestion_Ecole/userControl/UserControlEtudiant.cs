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
    public partial class UserControlEtudiant: UserControl
    {
        public UserControlEtudiant()
        {
            InitializeComponent();
        }

        private void UserControlEtudiant_Load(object sender, EventArgs e)
        {
            refreshTab();
            refreshCmb();
            cmbFilterClasse.SelectedIndexChanged += cmbFilterClasse_SelectedIndexChanged;
           

            // Désactiver les boutons si l'utilisateur est un professeur
            if (SessionUtilisateur.Role == "Professeur")
            {
                btnAdd.Enabled = false;  // Désactiver le bouton "Ajouter"
                btnUpdate.Enabled = false;  // Désactiver le bouton "Mettre à jour"
                delete.Enabled = false;  // Désactiver le bouton "Supprimer"
                btnAdd.Visible = false;  // Masquer le bouton "Ajouter"
                btnUpdate.Visible = false;  // Masquer le bouton "Mettre à jour"
                delete.Visible = false;  // Masquer le bouton "Supprimer"
                groupBox1.Enabled = false;  // Désactiver le groupBox
                groupBox1.Visible = false;  // Masquer le groupBox
            }


            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var db = new DBconnect()) {
                string nom = txtNom.Text;
                string prenom = txtPrenom.Text;
                string matricule = txtMatricule.Text;
                string adresse = txtAdresse.Text;
                string email = txtEmail.Text;
                string telephone = txtTelephone.Text;
                int classe_id = (int)cmbClasse.SelectedValue;
                DateTime DateN = dateNaiss.Value;
                string sexe;
                if (ckfemme.Checked)
                {
                    sexe = "femme";
                } else if (ckhomme.Checked)
                {
                    sexe = "homme";
                }
                else
                {
                    MessageBox.Show("veuiller selectionner un sexe");
                    return;
                }


                if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenom) || string.IsNullOrEmpty(matricule) ||
                    string.IsNullOrEmpty(adresse) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(telephone) ||
                    string.IsNullOrEmpty(sexe))
                {
                    MessageBox.Show("veuillez remplir tous les champs", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               var cl = db.Classes.Find(classe_id);

            Etudiant b = new Etudiant();
                b.Nom = nom;
                b.Prenom = prenom;
                b.Matricule = matricule;
                b.Adresse = adresse;
                b.Email = email;
                b.Telephone = telephone;
                b.Classe = cl;
                b.DateNaissance = DateN;
                db.Etudiants.Add(b);
                db.SaveChanges();
                MessageBox.Show("etudiant ajouter avec succes", "succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshTab();
                refreshCmb();
                clear();
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Veuillez sélectionner un étudiant à mettre à jour", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;

            using (var db = new DBconnect())
            {
                // Récupérer l'étudiant existant
                var etudiant = db.Etudiants.Find(id);
                if (etudiant == null)
                {
                    MessageBox.Show("Etudiant non trouvé", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Récupérer les valeurs des contrôles
                string nom = txtNom.Text;
                string prenom = txtPrenom.Text;
                string matricule = txtMatricule.Text;
                string adresse = txtAdresse.Text;
                string email = txtEmail.Text;
                string telephone = txtTelephone.Text;
                int classe_id = (int)cmbClasse.SelectedValue;
                DateTime dateN = dateNaiss.Value;
                string sexe;

                if (ckfemme.Checked)
                {
                    sexe = "femme";
                }
                else if (ckhomme.Checked)
                {
                    sexe = "homme";
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner un sexe", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validation des champs
                if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(prenom) || string.IsNullOrEmpty(matricule) ||
                    string.IsNullOrEmpty(adresse) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(telephone))
                {
                    MessageBox.Show("Veuillez remplir tous les champs", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mettre à jour les propriétés de l'étudiant
                etudiant.Nom = nom;
                etudiant.Prenom = prenom;
                etudiant.Matricule = matricule;
                etudiant.Adresse = adresse;
                etudiant.Email = email;
                etudiant.Telephone = telephone;
                etudiant.DateNaissance = dateN;
                etudiant.Sexe = sexe;

                // Récupérer la classe et l'assigner
                var cl = db.Classes.Find(classe_id);
                etudiant.Classe = cl;

                db.SaveChanges(); // Enregistrer les modifications
                MessageBox.Show("Étudiant mis à jour avec succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshTab(); // Rafraîchir le tableau
                clear(); // Effacer les champs
            }
        }



        private void delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Veuillez sélectionner un étudiant à supprimer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;

            // Confirmation de la suppression
            DialogResult dialogResult = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet étudiant ?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                using (var db = new DBconnect())
                {
                    var etudiant = db.Etudiants.Find(id);
                    if (etudiant != null)
                    {
                        db.Etudiants.Remove(etudiant); // Supprimer l'étudiant
                        db.SaveChanges(); // Enregistrer les modifications
                        MessageBox.Show("Étudiant supprimé avec succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        refreshTab(); // Rafraîchir le tableau
                    }
                    else
                    {
                        MessageBox.Show("Étudiant non trouvé", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

















































   public void refreshCmb()
{
    // Remplir cmbClasse avec toutes les classes
    cmbClasse.DataSource = null;
    using (var db = new DBconnect())
    {
        cmbClasse.DataSource = db.Classes.ToList();
        cmbClasse.DisplayMember = "NomClasse";
        cmbClasse.ValueMember = "Id";
    }

    // Remplir cmbFilterClasse mais le désactiver par défaut
    cmbFilterClasse.DataSource = null;
    using (var db = new DBconnect())
    {
        cmbFilterClasse.DataSource = db.Classes.ToList();
        cmbFilterClasse.DisplayMember = "NomClasse";
        cmbFilterClasse.ValueMember = "Id";
    }
   
}

// Gestionnaire d'événements pour activer le filtre lors de la sélection d'une classe
private void cmbClasse_SelectedIndexChanged(object sender, EventArgs e)
{
    if (cmbClasse.SelectedIndex != -1)
    {
        cmbFilterClasse.Enabled = true;
    }
    else
    {
        cmbFilterClasse.Enabled = false;
    }
}


        public void refreshTab()
        {
            using (var db = new DBconnect())
            {
                // Vérifier le rôle de l'utilisateur connecté
                if (SessionUtilisateur.Role == "Professeur")
                {
                    // Récupérer les classes associées au professeur connecté
                    var classeIdsAssociees = db.ProfesseurMatieres
                        .Where(pm => pm.ProfesseurId == SessionUtilisateur.UtilisateurId)
                        .Select(pm => pm.ClasseId)
                        .ToList();

                    // Récupérer les étudiants qui sont dans ces classes
                    var etudiantsQuery = db.Etudiants
                        .Where(e => classeIdsAssociees.Contains(e.IdClasse));

                    var etudiants = etudiantsQuery
                        .Select(e => new
                        {
                            e.Id,
                            Nom_Complet = e.Nom + " " + e.Prenom,
                            e.Matricule,
                            e.Adresse,
                            e.Email,
                            e.Telephone,
                            e.DateNaissance,
                            Sexe = e.Sexe,
                            Classe = e.Classe != null ? e.Classe.NomClasse : "Non attribuée"
                        })
                        .ToList()
                        .Select(e => new
                        {
                            e.Id,
                            e.Nom_Complet,
                            e.Matricule,
                            e.Adresse,
                            e.Email,
                            e.Telephone,
                            Date_Naissance = e.DateNaissance.ToString("dd/MM/yyyy"),
                            e.Sexe,
                            e.Classe
                        })
                        .ToList();

                    dataGridView1.DataSource = etudiants;
                }
                else
                {
                    // Si l'utilisateur n'est pas un professeur, afficher tous les étudiants
                    var etudiants = db.Etudiants
                        .Select(e => new
                        {
                            e.Id,
                            Nom_Complet = e.Nom + " " + e.Prenom,
                            e.Matricule,
                            e.Adresse,
                            e.Email,
                            e.Telephone,
                            e.DateNaissance,
                            Sexe = e.Sexe,
                            Classe = e.Classe != null ? e.Classe.NomClasse : "Non attribuée"
                        })
                        .ToList()
                        .Select(e => new
                        {
                            e.Id,
                            e.Nom_Complet,
                            e.Matricule,
                            e.Adresse,
                            e.Email,
                            e.Telephone,
                            Date_Naissance = e.DateNaissance.ToString("dd/MM/yyyy"),
                            e.Sexe,
                            e.Classe
                        })
                        .ToList();

                    dataGridView1.DataSource = etudiants;
                }
            }
        }






        public void clear()
        {
       
            // Effacer les champs de texte
            txtNom.Text = string.Empty;
            txtPrenom.Text = string.Empty;
            txtMatricule.Text = string.Empty;
            txtAdresse.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelephone.Text = string.Empty;

            // Réinitialiser le ComboBox
            cmbClasse.SelectedIndex = -1; // Ou mettez-le à une valeur par défaut si nécessaire

            // Réinitialiser le DateTimePicker
            dateNaiss.Value = DateTime.Now; // Ou une autre date par défaut

            // Réinitialiser les CheckBox
            ckfemme.Checked = false;
            ckhomme.Checked = false;
        
    }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
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
                var c = db.Etudiants.Find(id);
                if (c != null)
                {
                    txtNom.Text = c.Nom;
                    txtPrenom.Text = c.Prenom;
                    txtTelephone.Text = c.Telephone;
                    txtEmail.Text = c.Email;
                    txtAdresse.Text = c.Adresse;
                    txtMatricule.Text = c.Matricule;
                    if(c.Sexe == "homme")
                    {
                        ckfemme.Checked = false;
                        ckhomme.Checked = true;
                    }else if(c.Sexe == "femme")
                    {
                        ckfemme.Checked = true;
                        ckhomme.Checked = false;
                    }
                    dateNaiss.Value = c.DateNaissance;
                }
                else
                {
                    MessageBox.Show("Etudiant non trouvé", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbFilterClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshTab();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            using (var db = new DBconnect())
            {
                var etudiantsQuery = db.Etudiants.AsQueryable();

                // Appliquer le filtre de recherche
                if (!string.IsNullOrEmpty(searchText))
                {
                    etudiantsQuery = etudiantsQuery.Where(b =>
                        b.Nom.ToLower().Contains(searchText) ||
                        b.Prenom.ToLower().Contains(searchText) ||
                        b.Matricule.ToLower().Contains(searchText) ||
                        (b.Classe != null && b.Classe.NomClasse.ToLower().Contains(searchText)));
                }

                // Exécuter la requête SQL d'abord
                var etudiantsData = etudiantsQuery.ToList();

                // Ensuite, effectuer les transformations en mémoire
                var etudiants = etudiantsData.Select(b => new
                {
                    b.Id,
                    Nom_Complet = b.Nom + " " + b.Prenom,
                    b.Matricule,
                    b.Adresse,
                    b.Email,
                    b.Telephone,
                    Date_Naissance = b.DateNaissance.ToString("dd/MM/yyyy"),
                    Sexe = b.Sexe,
                    Classe = b.Classe != null ? b.Classe.NomClasse : "Non attribuée"
                }).ToList();

                dataGridView1.DataSource = etudiants;
            }
        }


    }
}
