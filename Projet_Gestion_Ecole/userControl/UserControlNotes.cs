using System;
using System.Linq;
using System.Windows.Forms;
using Projet_Gestion_Ecole.DAO;
using System.Data.Entity;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Projet_Gestion_Ecole.userControl
{
    public partial class UserControlNotes : UserControl
    {
        private int selectedNoteId = -1; // Variable pour stocker l'ID de la note sélectionnée

        public UserControlNotes()
        {
            InitializeComponent();
        }

        // Charger les données des classes, étudiants et matières
        private void UserControlNotes_Load(object sender, EventArgs e)
        {
            // Remplir le ComboBox des classes et autres ComboBox
            refreshAll();
            LoadNotes(); // Charger les notes dans le DataGridView
        }


        private void refreshAll()
        {
            using (var db = new DBconnect())
            {
                // Remplir le ComboBox des classes
                cmbClasse.DataSource = db.Classes.ToList();
                cmbClasse.DisplayMember = "NomClasse";
                cmbClasse.ValueMember = "Id";

                // Remplir le ComboBox des matières
                cmbMatiere.DataSource = db.Matieres.ToList();
                cmbMatiere.DisplayMember = "NomMatiere";
                cmbMatiere.ValueMember = "Id";

                // Si une classe est sélectionnée, remplir les étudiants en fonction de cette classe
                if (cmbClasse.SelectedValue != null)
                {
                    int classeId = (int)cmbClasse.SelectedValue;
                    var etudiants = db.Etudiants.Where(e => e.IdClasse == classeId).ToList();
                    cmbEtudiant.DataSource = etudiants;
                    cmbEtudiant.DisplayMember = "Nom";  // Afficher le nom de l'étudiant
                    cmbEtudiant.ValueMember = "Id";
                }
                else
                {
                    cmbEtudiant.DataSource = null; // Si aucune classe sélectionnée, vider les étudiants
                }

                // Charger les données de notes dans le DataGridView
                var notes = db.Notes
                              .Include(n => n.Etudiant)  // Charger l'étudiant
                              .Include(n => n.Matiere)   // Charger la matière
                              .ToList();

                // Affichage des données dans le DataGridView
                dataGridView1.DataSource = notes.Select(n => new
                {
                    n.Id,
                    EtudiantNom = n.Etudiant != null ? n.Etudiant.Nom : "Non spécifié",  // Vérification de nullité
                    EtudiantPrenom = n.Etudiant != null ? n.Etudiant.Prenom : "Non spécifié",
                    MatiereNom = n.Matiere != null ? n.Matiere.NomMatiere : "Non spécifiée",
                    n.note
                }).ToList();
            }
        }



        // Lorsque la classe est changée, remplir le ComboBox des étudiants
        private void cmbClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshAll();  // Mettre à jour les ComboBox en fonction de la classe sélectionnée
        }

        // Ajouter une note pour l'étudiant sélectionné
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Vérification de la note
            if (string.IsNullOrEmpty(txtNote.Text) || !float.TryParse(txtNote.Text, out float note))
            {
                MessageBox.Show("Veuillez entrer une note valide (un nombre réel)", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Récupérer les valeurs des contrôles
            int etudiantId = (int)cmbEtudiant.SelectedValue;
            int matiereId = (int)cmbMatiere.SelectedValue;

            // Vérifier si la note existe déjà pour cet étudiant et cette matière
            using (var db = new DBconnect())
            {
                var noteExist = db.Notes.FirstOrDefault(n => n.IdEtudiant == etudiantId && n.IdMatiere == matiereId);
                if (noteExist != null)
                {
                    MessageBox.Show("Une note existe déjà pour cet étudiant et cette matière", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ajouter la nouvelle note
                var newNote = new Note
                {
                    IdEtudiant = etudiantId,
                    IdMatiere = matiereId,
                    note = note // La note est de type float
                };

                db.Notes.Add(newNote);
                db.SaveChanges();

                MessageBox.Show("Note ajoutée avec succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
                LoadNotes(); // Rafraîchir la liste des notes dans le DataGridView
            }
        }

        // Mettre à jour une note
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedNoteId == -1)
            {
                MessageBox.Show("Veuillez sélectionner une note à mettre à jour", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtNote.Text) || !float.TryParse(txtNote.Text, out float updatedNote))
            {
                MessageBox.Show("Veuillez entrer une note valide (un nombre réel)", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Récupérer l'ID de l'étudiant et de la matière
            int etudiantId = (int)cmbEtudiant.SelectedValue;
            int matiereId = (int)cmbMatiere.SelectedValue;

            using (var db = new DBconnect())
            {
                // Vérifier si la note existe
                var existingNote = db.Notes.FirstOrDefault(n => n.Id == selectedNoteId);
                if (existingNote == null)
                {
                    MessageBox.Show("Note introuvable", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mettre à jour la note
                existingNote.note = updatedNote; // Mise à jour de la note
                db.SaveChanges();

                MessageBox.Show("Note mise à jour avec succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
                selectedNoteId = -1;  // Réinitialiser l'ID de la note sélectionnée
                LoadNotes(); // Rafraîchir la liste des notes dans le DataGridView
            }
        }

        // Supprimer une note
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedNoteId == -1)
            {
                MessageBox.Show("Veuillez sélectionner une note à supprimer", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var db = new DBconnect())
            {
                var noteToDelete = db.Notes.FirstOrDefault(n => n.Id == selectedNoteId);
                if (noteToDelete != null)
                {
                    db.Notes.Remove(noteToDelete);
                    db.SaveChanges();

                    MessageBox.Show("Note supprimée avec succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearFields();
                    selectedNoteId = -1;  // Réinitialiser l'ID de la note sélectionnée
                    LoadNotes(); // Rafraîchir la liste des notes dans le DataGridView
                }
                else
                {
                    MessageBox.Show("Note introuvable", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Effacer les champs
        private void clearFields()
        {
            txtNote.Text = string.Empty;
            cmbEtudiant.SelectedIndex = -1;
            cmbMatiere.SelectedIndex = -1;
            cmbClasse.SelectedIndex = -1;
        }

        // Réinitialiser les champs à la base
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        // Lorsque l'on double-clique sur une ligne du DataGridView
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Veuillez sélectionner une ligne valide", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            selectedNoteId = (int)dataGridView1.CurrentRow.Cells["Id"].Value;

            using (var db = new DBconnect())
            {
                var note = db.Notes.Find(selectedNoteId);
                if (note != null)
                {
                    txtNote.Text = note.note.ToString("F2"); // Afficher la note avec 2 décimales

                    // Prendre les ID de l'étudiant et de la matière associés à la note pour remplir les ComboBox
                    cmbEtudiant.SelectedValue = note.IdEtudiant;
                    cmbMatiere.SelectedValue = note.IdMatiere;
                }
            }
        }

        // Charger les notes dans le DataGridView
        private void LoadNotes()
        {
            using (var db = new DBconnect())
            {
                var notesQuery = db.Notes
                                   .Include(n => n.Etudiant)  // Charger l'étudiant
                                   .Include(n => n.Matiere)   // Charger la matière
                                   .AsQueryable();

                // Filtrer selon le rôle de l'utilisateur
                var currentUserRole = SessionUtilisateur.Role; // Récupérer le rôle de l'utilisateur actuel
                if (currentUserRole == "Professeur") // Si l'utilisateur est un professeur
                {
                    int professorId = (int)SessionUtilisateur.UtilisateurId;
                    notesQuery = notesQuery.Where(n => n.Matiere.ProfesseurMatieres
                                      .Any(pm => pm.ProfesseurId == professorId));  // Filtrer les notes de ce professeur
                }

                // Charger les données de notes
                var notes = notesQuery.ToList();

                // Affichage des données dans le DataGridView
                dataGridView1.DataSource = notes.Select(n => new
                {
                    n.Id,
                    EtudiantNom = n.Etudiant != null ? n.Etudiant.Nom : "Non spécifié",
                    EtudiantPrenom = n.Etudiant != null ? n.Etudiant.Prenom : "Non spécifié",
                    MatiereNom = n.Matiere != null ? n.Matiere.NomMatiere : "Non spécifiée",
                    n.note
                }).ToList();
            }
        }




        private void UserControlNotes_Load_1(object sender, EventArgs e)
        {
            refreshAll();
            LoadNotes();
        }

        // Calculer la moyenne des notes pour un étudiant
        private float CalculerMoyenne(int etudiantId)
        {
            using (var db = new DBconnect())
            {
                var notes = db.Notes.Where(n => n.IdEtudiant == etudiantId).ToList();
                if (notes.Count == 0) return 0;
                return notes.Average(n => n.note);
            }
        }

        private void btnAfficherReleve_Click(object sender, EventArgs e)
        {
            if (cmbEtudiant.SelectedValue == null)
            {
                MessageBox.Show("Veuillez sélectionner un étudiant.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int etudiantId = (int)cmbEtudiant.SelectedValue;
            using (var db = new DBconnect())
            {
                var relevé = db.Notes
                    .Where(n => n.IdEtudiant == etudiantId)
                    .Select(n => new
                    {
                        Matiere = n.Matiere.NomMatiere,
                        Note = n.note
                    }).ToList();

                // Calcul de la moyenne générale
                float moyenne = CalculerMoyenne(etudiantId);

                // Affichage dans le DataGridView
                dataGridView1.DataSource = relevé;

                // Afficher la moyenne dans un label
                lblMoyenne.Text = $"Moyenne Générale: {moyenne:F2}";
            }
        }

        private void AfficherMoyenneParMatiere()
        {
            using (var db = new DBconnect())
            {
                var moyennes = db.Notes
                    .GroupBy(n => n.Matiere.NomMatiere)
                    .Select(g => new
                    {
                        Matiere = g.Key,
                        Moyenne = g.Average(n => n.note)
                    }).ToList();

                dataGridView1.DataSource = moyennes;
            }
        }

        private void GenererReleveNotesPDF(int etudiantId)
        {
            using (var db = new DBconnect())
            {
                var etudiant = db.Etudiants.Find(etudiantId);
                var notes = db.Notes.Where(n => n.IdEtudiant == etudiantId)
                                    .Select(n => new { n.Matiere.NomMatiere, n.note })
                                    .ToList();

                if (etudiant == null || notes.Count == 0)
                {
                    MessageBox.Show("Aucune donnée à exporter.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.FileName = $"ReleveNotes_{etudiant.Nom}_{etudiant.Prenom}.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Document doc = new Document();
                    PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                    doc.Open();

                    // Titre du relevé
                    doc.Add(new Paragraph($"Relevé de Notes - {etudiant.Nom} {etudiant.Prenom}"));
                    doc.Add(new Paragraph("\n"));

                    // Table des notes
                    PdfPTable table = new PdfPTable(2);
                    table.AddCell("Matière");
                    table.AddCell("Note");

                    foreach (var note in notes)
                    {
                        table.AddCell(note.NomMatiere);
                        table.AddCell(note.note.ToString("F2"));
                    }

                    doc.Add(table);
                    doc.Close();

                    MessageBox.Show("Relevé de notes généré avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

       
            private void btnExporterPDF_Click(object sender, EventArgs e)
        {
            if (cmbEtudiant.SelectedValue != null)
            {
                int etudiantId = (int)cmbEtudiant.SelectedValue;
                GenererReleveNotesPDF(etudiantId);
            }
        }

    }
}

