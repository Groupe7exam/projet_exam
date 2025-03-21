using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Projet_Gestion_Ecole.DAO;

namespace Projet_Gestion_Ecole.userControl
{
    public partial class UserControlClassePage : UserControl
    {
        public UserControlClassePage()
        {
            InitializeComponent();
        }

        private void UserControlClassePage_Load(object sender, EventArgs e)
        {
            clear();
            refresh();

        }

        public void clear()
        {
            txtNomClasse.Clear(); // Réinitialise le champ de texte du nom de la classe
        }
        public void refresh()
        {
            using (var db = new DBconnect())
            {
                if (SessionUtilisateur.Role == "Professeur")
                {
                    // Récupération des classes et cours associés pour le professeur connecté
                    var classes = db.Classes
                        .Select(classe => new
                        {
                            classe.Id,
                            classe.NomClasse,
                            // Récupération des cours associés
                            CoursAssocies = classe.ClasseCours
                                .Select(cc => cc.Cours.NomCours)
                                .Distinct() // Évite les doublons
                                .ToList()
                        })
                        .ToList();

                    // Mise en forme des résultats pour l'affichage dans le DataGridView
                    var result = classes.Select(c => new
                    {
                        c.Id,
                        c.NomClasse,
                        CoursAssocies = c.CoursAssocies.Any()
                            ? string.Join(", ", c.CoursAssocies)
                            : "Aucun cours associé"
                    }).ToList();

                    dataGridView1.DataSource = result; // Met à jour le DataGridView
                }
                else
                {
                    // Récupération des classes, cours et professeurs associés
                    var classes = db.Classes
                        .Select(classe => new
                        {
                            classe.Id,
                            classe.NomClasse,
                            // Récupération des cours associés
                            CoursAssocies = classe.ClasseCours
                                .Select(cc => cc.Cours.NomCours)
                                .Distinct() // Évite les doublons
                                .ToList(),
                            // Récupération des professeurs associés via ProfesseurMatiere
                            ProfesseursAssocies = db.ProfesseurMatieres
                                .Where(pm => pm.ClasseId == classe.Id)
                                .Select(pm => pm.Professeur.Nom)
                                .Distinct() // Évite les doublons
                                .ToList()
                        })
                        .ToList();

                    // Mise en forme des résultats pour l'affichage dans le DataGridView
                    var result = classes.Select(c => new
                    {
                        c.Id,
                        c.NomClasse,
                        CoursAssocies = c.CoursAssocies.Any()
                            ? string.Join(", ", c.CoursAssocies)
                            : "Aucun cours associé",
                        ProfesseursAssocies = c.ProfesseursAssocies.Any()
                            ? string.Join(", ", c.ProfesseursAssocies)
                            : "Aucun professeur associé"
                    }).ToList();

                    dataGridView1.DataSource = result; // Met à jour le DataGridView
                }
            }
        }






        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var db = new DBconnect())
            {
                var nouvelleClasse = new Classe
                {
                    NomClasse = txtNomClasse.Text.Trim()
                };

                db.Classes.Add(nouvelleClasse);
                db.SaveChanges(); // Enregistre les changements dans la base de données
                MessageBox.Show("Classe ajoutée avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear(); // Réinitialise le formulaire
                refresh(); // Met à jour le DataGridView
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                // Récupère l'ID de la personne sélectionnée
                int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;

                using (var db = new DBconnect())
                {
                    // Recherche la personne dans la base de données
                    var classe = db.Classes.Find(id);

                    if (classe != null)
                    {
                        // Remplit les champs du formulaire avec les données de la personne
                        txtNomClasse.Text = classe.NomClasse;
                    }
                    else
                    {
                        MessageBox.Show("classe non trouvée.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;

                using (var db = new DBconnect())
                {
                    var classe = db.Classes.Find(id);
                    if (classe != null)
                    {
                        classe.NomClasse = txtNomClasse.Text.Trim();
                        db.SaveChanges(); // Enregistre les changements
                        MessageBox.Show("Classe mise à jour avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear(); // Réinitialise le formulaire
                        refresh(); // Met à jour le DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Classe non trouvée.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une classe à mettre à jour.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = (int)dataGridView1.CurrentRow.Cells["id"].Value;

                using (var db = new DBconnect())
                {
                    var classe = db.Classes.Find(id);
                    if (classe != null)
                    {
                        db.Classes.Remove(classe);
                        db.SaveChanges(); // Enregistre les changements
                        MessageBox.Show("Classe supprimée avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear(); // Réinitialise le formulaire
                        refresh(); // Met à jour le DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Classe non trouvée.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une classe à supprimer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // 🔹 Vérifie qu'une ligne est bien sélectionnée
            {
                int classeId = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value; // 🔹 Récupère l'ID de la classe
                FormEtudiants form = new FormEtudiants(classeId); // 🔹 Crée la fenêtre popup
                form.ShowDialog(); // 🔹 Affiche la fenêtre en mode bloquant
            }
        }
    }
}
