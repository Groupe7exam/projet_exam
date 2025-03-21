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
using System.Data.Entity;

namespace Projet_Gestion_Ecole.userControl
{
    public partial class UserControl1Matiere: UserControl
    {
        public UserControl1Matiere()
        {
            InitializeComponent();
            Refresh();
            CustomizeDataGridView();
        }
       

        public void refresh()
        {
            using (var db = new DBconnect())
            {
                var matieres = db.Matieres
                    .Select(matiere => new
                    {
                        matiere.Id,
                        matiere.NomMatiere,
                        CoursAssocies = matiere.CoursMatieres
                            .Select(cm => cm.Cours.NomCours)
                            .Distinct()
                            .ToList(),
                        ProfesseursAssocies = matiere.ProfesseurMatieres
                            .Select(pm => pm.Professeur.Nom)
                            .Distinct()
                            .ToList()
                    })
                    .ToList();

                var result = matieres.Select(m => new
                {
                    m.Id,
                    m.NomMatiere,
                    CoursAssocies = m.CoursAssocies.Any()
                        ? string.Join(", ", m.CoursAssocies)
                        : "Aucun cours associé",
                    ProfesseursAssocies = m.ProfesseursAssocies.Any()
                        ? string.Join(", ", m.ProfesseursAssocies)
                        : "Aucun professeur associé"
                }).ToList();

                dataGridView1.DataSource = result; // Met à jour le DataGridView
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var nouvelleMatiere = new Matiere
            {
                NomMatiere = txtNomMat.Text,
              
            };

            using (var db = new DBconnect())
            {
                db.Matieres.Add(nouvelleMatiere);
                db.SaveChanges();
            }

            refresh();
            clear();
            MessageBox.Show("matiere ajoute avec succes", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedMatiere = (Matiere)dataGridView1.SelectedRows[0].DataBoundItem;
                selectedMatiere.NomMatiere = txtNomMat.Text;
                
                using (var db = new DBconnect())
                {
                    db.Entry(selectedMatiere).State = EntityState.Modified;
                    db.SaveChanges();
                }

                refresh();
                clear();
                MessageBox.Show("matiere modifier avec succes", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        public void clear()
        {
            txtNomMat.Text = string.Empty;
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
                    txtNomMat.Text = c.Nom;
                  

                }
                else
                {
                    MessageBox.Show("matiere non trouvé", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UserControl1Matiere_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une matière à supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Récupérer l'ID de la matière sélectionnée
            var selectedMatiere = (dynamic)dataGridView1.SelectedRows[0].DataBoundItem;
            int matiereId = selectedMatiere.Id;

            DialogResult dialogResult = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette matière ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                using (var db = new DBconnect())
                {
                    var matiere = db.Matieres.Find(matiereId);
                    if (matiere != null)
                    {
                        // Supprimer les associations de cours et professeurs
                        db.CoursMatieres.RemoveRange(db.CoursMatieres.Where(cm => cm.MatiereId == matiereId));
                        db.ProfesseurMatieres.RemoveRange(db.ProfesseurMatieres.Where(pm => pm.MatiereId == matiereId));

                        // Supprimer la matière elle-même
                        db.Matieres.Remove(matiere);

                        // Sauvegarder les changements dans la base de données
                        db.SaveChanges();
                        MessageBox.Show("Matière supprimée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Rafraîchir l'affichage après la suppression
                        refresh();
                    }
                    else
                    {
                        MessageBox.Show("Matière non trouvée.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim().ToLower();  // Récupère le terme de recherche et le met en minuscules

            using (var db = new DBconnect())
            {
                // Recherche des matières où le nom contient le terme de recherche (insensible à la casse)
                var matieres = db.Matieres
                    .Where(m => m.NomMatiere.ToLower().Contains(searchTerm))
                    .Select(m => new
                    {
                        m.Id,
                        m.NomMatiere
                    })
                    .ToList();

                dataGridView1.DataSource = matieres;  // Met à jour le DataGridView avec les résultats
            }
        }














































        private void CustomizeDataGridView()
        {
            dataGridView1.EnableHeadersVisualStyles = false;  // Désactive les styles visuels par défaut
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateGray;  // Couleur des en-têtes de colonne
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;  // Texte en blanc dans les en-têtes

            // Modifier l’apparence des lignes
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;  // Couleur des lignes alternées

            // Auto-ajustement de la taille des colonnes
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

    }
}
