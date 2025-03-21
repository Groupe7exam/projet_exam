using System;
using System.Linq;
using System.Windows.Forms;
using Projet_Gestion_Ecole.DAO;

namespace Projet_Gestion_Ecole.userControl
{
    public partial class UserControlCours : UserControl
    {
        public UserControlCours()
        {
            InitializeComponent();
        }

        private void UserControlCours_Load(object sender, EventArgs e)
        {
            RefreshCours();
            LoadCheckedListBoxes(); // Charger les classes et matières disponibles
        }

        private void btnAddCours_Click(object sender, EventArgs e)
        {
            using (var db = new DBconnect())
            {
                string nom = txtCours.Text.Trim();
                string description = txtDescription.Text.Trim();

                if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(description))
                {
                    MessageBox.Show("Veuillez remplir tous les champs", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Créer un nouveau cours
                Cours c = new Cours { NomCours = nom, Description = description };
                db.Cours.Add(c);
                db.SaveChanges(); // Sauvegarder le cours dans la base

                // Vérifier et associer les classes sélectionnées
                var selectedClasses = checkedListBoxClasses.CheckedItems.Cast<ComboBoxItem>().Select(b => b.Id).ToList();
                if (selectedClasses.Any())
                {
                    foreach (var classeId in selectedClasses)
                    {
                        db.ClasseCours.Add(new ClasseCours { CoursId = c.Id, ClasseId = classeId });
                    }
                }

                var selectedMatieres = checkedListBoxMatiere.CheckedItems.Cast<ComboBoxItem>().Select(b => b.Id).ToList();
                if (selectedMatieres.Any())
                {
                    foreach (var matieresId in selectedMatieres)
                    {
                        db.CoursMatieres.Add(new CoursMatiere { CoursId = c.Id, MatiereId = matieresId });
                    }
                }
                db.SaveChanges();

                MessageBox.Show("Cours ajouté avec succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshCours();
                Clear();
            }
        }


        private void btnUpdateCours_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("Veuillez sélectionner un cours à modifier.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridView2.CurrentRow.Cells["Id"].Value;

            using (var db = new DBconnect())
            {
                var cours = db.Cours.Find(id);
                if (cours == null)
                {
                    MessageBox.Show("Cours introuvable.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nom = txtCours.Text.Trim();
                string description = txtDescription.Text.Trim();

                if (string.IsNullOrEmpty(nom) || string.IsNullOrEmpty(description))
                {
                    MessageBox.Show("Veuillez remplir tous les champs.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mise à jour des informations du cours
                cours.NomCours = nom;
                cours.Description = description;

                // 🔹 Suppression des anciennes associations
                db.ClasseCours.RemoveRange(db.ClasseCours.Where(cc => cc.CoursId == cours.Id));
                db.CoursMatieres.RemoveRange(db.CoursMatieres.Where(cm => cm.CoursId == cours.Id));

                // 🔹 Ajouter les nouvelles associations

                // Associer les classes sélectionnées
                var selectedClasses = checkedListBoxClasses.CheckedItems.Cast<ComboBoxItem>().Select(c => c.Id).ToList();
                if (selectedClasses.Any())
                {
                    foreach (var classeId in selectedClasses)
                    {
                        db.ClasseCours.Add(new ClasseCours { CoursId = cours.Id, ClasseId = classeId });
                    }
                }

                var selectedMatieres = checkedListBoxMatiere.CheckedItems.Cast<ComboBoxItem>().Select(b => b.Id).ToList();
                if (selectedMatieres.Any())
                {
                    foreach (var matieresId in selectedMatieres)
                    {
                        db.CoursMatieres.Add(new CoursMatiere { CoursId = cours.Id, MatiereId = matieresId });
                    }
                }

                // Sauvegarder les modifications
                db.SaveChanges();

                MessageBox.Show("Cours modifié avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshCours();
                Clear();
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("Veuillez sélectionner un cours à supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = (int)dataGridView2.CurrentRow.Cells["Id"].Value;

            DialogResult dialogResult = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce cours ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                using (var db = new DBconnect())
                {
                    var cours = db.Cours.Find(id);
                    if (cours != null)
                    {
                        db.Cours.Remove(cours);
                        db.SaveChanges();
                        MessageBox.Show("Cours supprimé avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshCours();
                    }
                    else
                    {
                        MessageBox.Show("Cours non trouvé.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnAssocierCours_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("Veuillez sélectionner un cours.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int coursId = (int)dataGridView2.CurrentRow.Cells["Id"].Value;
            

            using (var db = new DBconnect())
            {
                var cours = db.Cours.Find(coursId);
                if (cours == null)
                {
                    MessageBox.Show("Cours introuvable.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 🔹 Supprimer les anciennes associations et ajouter les nouvelles
                db.ClasseCours.RemoveRange(db.ClasseCours.Where(cc => cc.CoursId == coursId));
                db.CoursMatieres.RemoveRange(db.CoursMatieres.Where(cm => cm.CoursId == coursId));

                // 🔹 Associer les nouvelles classes
                var selectedClasses = checkedListBoxClasses.CheckedItems.Cast<ComboBoxItem>().Select(c => c.Id).ToList();
                foreach (var classeId in selectedClasses)
                {
                    db.ClasseCours.Add(new ClasseCours { CoursId = coursId, ClasseId = classeId });
                }

                // 🔹 Associer la matière
                var selectedMatieres = checkedListBoxMatiere.CheckedItems.Cast<ComboBoxItem>().Select(b => b.Id).ToList();
                if (selectedMatieres.Any())
                {
                    foreach (var matieresId in selectedMatieres)
                    {
                        db.CoursMatieres.Add(new CoursMatiere { CoursId = cours.Id, MatiereId = matieresId });
                    }
                }

                db.SaveChanges();
                MessageBox.Show("Cours associé à la matière et aux classes avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshCours();
            }
        }


        private void RefreshCours()
        {
            using (var db = new DBconnect())
            {
                // Charger les cours avec leurs classes et matières associés
                var coursList = db.Cours
                    .Select(c => new
                    {
                        c.Id,
                        c.NomCours,
                        c.Description,
                        ClasseCours = c.ClasseCours.Select(cc => cc.Classe.NomClasse).ToList(), // Charger en mémoire
                        CoursMatieres = c.CoursMatieres.Select(cm => cm.Matiere.NomMatiere).ToList() // Charger en mémoire
                    })
                    .ToList(); // Charger en mémoire uniquement ce dont on a besoin

                // Appliquer string.Join après récupération des données en mémoire
                var result = coursList.Select(c => new
                {
                    c.Id,
                    c.NomCours,
                    c.Description,
                    // Utiliser string.Join pour combiner les noms des classes et des matières
                    NomClasses = string.Join(", ", c.ClasseCours),
                    Matiere = c.CoursMatieres.FirstOrDefault() != null ? string.Join(", ", c.CoursMatieres) : "Aucune matière"
                }).ToList();

                // Mettre à jour le DataGridView avec la liste des cours
                dataGridView2.DataSource = result;
            }
        }




        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("Veuillez sélectionner une ligne valide", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int coursId = (int)dataGridView2.CurrentRow.Cells["Id"].Value;

            using (var db = new DBconnect())
            {
                var cours = db.Cours.Find(coursId);
                if (cours != null)
                {
                    txtCours.Text = cours.NomCours;
                    txtDescription.Text = cours.Description;

                    // Cocher les classes associées
                    var selectedClasses = db.ClasseCours.Where(cc => cc.CoursId == coursId).Select(cc => cc.ClasseId).ToList();
                    for (int i = 0; i < checkedListBoxClasses.Items.Count; i++)
                    {
                        var item = (ComboBoxItem)checkedListBoxClasses.Items[i];
                        checkedListBoxClasses.SetItemChecked(i, selectedClasses.Contains(item.Id));
                    }
                }
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            // Effacer les champs de texte
            txtCours.Clear();
            txtDescription.Clear();

            // Décocher les éléments des CheckedListBox
            for (int i = 0; i < checkedListBoxClasses.Items.Count; i++)
            {
                checkedListBoxClasses.SetItemChecked(i, false);
            }

            for (int i = 0; i < checkedListBoxMatiere.Items.Count; i++)
            {
                checkedListBoxMatiere.SetItemChecked(i, false);
            }
        }

        

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
