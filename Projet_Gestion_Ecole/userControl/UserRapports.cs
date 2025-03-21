using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Jose.netstandard1_4;

namespace Projet_Gestion_Ecole.userControl
{
    public partial class UserRapports : UserControl
    {
        public UserRapports()
        {
            InitializeComponent();
        }

        private void btnExporterPDF_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Aucune donnée à exporter !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Fichier PDF (*.pdf)|*.pdf",
                Title = "Enregistrer le rapport en PDF"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    {
                        Document document = new Document(PageSize.A4);
                        PdfWriter.GetInstance(document, stream);
                        document.Open();

                        Font titleFont = FontFactory.GetFont("HELVETICA", 16);
                        Font headerFont = FontFactory.GetFont("HELVETICA", 12);
                        Font cellFont = FontFactory.GetFont("HELVETICA", 10);

                        // Ajouter un titre au document
                        Paragraph title = new Paragraph("Rapport des étudiants\n\n", titleFont)
                        {
                            Alignment = Element.ALIGN_CENTER
                        };
                        document.Add(title);

                        // Ajouter les informations de l'étudiant (si un étudiant est sélectionné)
                        if (cmbEtudiant.SelectedValue != null)
                        {
                            int etudiantId = (int)cmbEtudiant.SelectedValue;
                            using (var db = new DBconnect())
                            {
                                var etudiant = db.Etudiants.FirstOrDefault(b => b.Id == etudiantId);
                                if (etudiant != null)
                                {
                                    // Afficher les informations de l'étudiant (prénom et nom)
                                    Paragraph etudiantInfo = new Paragraph(
                                        $"Nom de l'étudiant : {etudiant.Prenom} {etudiant.Nom}\n", cellFont);
                                    document.Add(etudiantInfo);
                                }
                            }
                        }

                        if (cmbClasse.SelectedValue != null)
                        {
                            int classeId = (int)cmbClasse.SelectedValue;
                            using (var db = new DBconnect())
                            {
                                var cl = db.Classes.FirstOrDefault(b => b.Id == classeId);
                                if (cl != null)
                                {
                                    // Afficher les informations de l'étudiant (prénom et nom)
                                    Paragraph etudiantInfo = new Paragraph(
                                        $"classe : {cl.NomClasse}\n", cellFont);
                                    document.Add(etudiantInfo);
                                }
                            }
                        }

                        // Création du tableau PDF
                        PdfPTable table = new PdfPTable(dataGridView1.Columns.Count)
                        {
                            WidthPercentage = 100
                        };

                        // Ajout des en-têtes de colonne
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            PdfPCell headerCell = new PdfPCell(new Phrase(column.HeaderText, headerFont))
                            {
                                BackgroundColor = new BaseColor(200, 200, 200),
                                HorizontalAlignment = Element.ALIGN_CENTER
                            };
                            table.AddCell(headerCell);
                        }

                        // Ajout des données du DataGridView
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!row.IsNewRow) // Vérifie si ce n'est pas la dernière ligne vide
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    table.AddCell(new Phrase(cell.Value?.ToString() ?? "", cellFont));
                                }
                            }
                        }

                        // Ajouter le tableau au document
                        document.Add(table);
                        document.Close();
                    }

                    MessageBox.Show("Exportation réussie !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'exportation : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        public void refresh()
        {
            // Désactiver temporairement les événements
            cmbEtudiant.SelectedIndexChanged -= cmbEtudiant_SelectedIndexChanged;
            cmbClasse.SelectedIndexChanged -= cmbClasse_SelectedIndexChanged;

            cmbEtudiant.Items.Clear();
            cmbClasse.Items.Clear();

            using (var db = new DBconnect())
            {
                cmbEtudiant.DataSource = db.Etudiants.ToList();
                cmbEtudiant.DisplayMember = "Nom";
                cmbEtudiant.ValueMember = "Id";

                cmbClasse.DataSource = db.Classes.ToList();
                cmbClasse.DisplayMember = "NomClasse";
                cmbClasse.ValueMember = "Id";
            }

            // Réinitialiser les `ComboBox` sans déclencher `SelectedIndexChanged`
            cmbClasse.SelectedIndex = -1;
            cmbEtudiant.SelectedIndex = -1;

            // Vider le DataGridView
            dataGridView1.DataSource = null;

            // Réactiver les événements après chargement des données
            cmbEtudiant.SelectedIndexChanged += cmbEtudiant_SelectedIndexChanged;
            cmbClasse.SelectedIndexChanged += cmbClasse_SelectedIndexChanged;
        }


        private float CalculerMoyenne(int etudiantId)
        {
            using (var db = new DBconnect())
            {
                var notes = db.Notes.Where(n => n.IdEtudiant == etudiantId).ToList();
                if (notes.Count == 0) return 0;
                return notes.Average(n => n.note);
            }
        }

        private void UserRapports_Load(object sender, EventArgs e)
        {
            refresh();
            cmbEtudiant.SelectedIndexChanged += new EventHandler(cmbEtudiant_SelectedIndexChanged);
            cmbClasse.SelectedIndexChanged += new EventHandler(cmbClasse_SelectedIndexChanged);
        }

        private void cmbEtudiant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEtudiant.SelectedIndex == -1 || cmbEtudiant.SelectedValue == null)
                return; // Empêche l'erreur lors du chargement initial

            if (int.TryParse(cmbEtudiant.SelectedValue.ToString(), out int etudiantId))
            {
                FiltrerNotesParEtudiant(etudiantId);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un étudiant valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FiltrerNotesParEtudiant(int etudiantId)
        {
            using (var db = new DBconnect())
            {
                var notes = db.Notes
                              .Where(n => n.IdEtudiant == etudiantId)
                              .Select(n => new
                              {
                                  MatiereNom = n.Matiere.NomMatiere,
                                  n.note
                              }).ToList();

                dataGridView1.DataSource = notes;
            }
        }

        private void cmbClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbClasse.SelectedIndex == -1 || cmbClasse.SelectedValue == null)
                return; // Empêche l'erreur lors du chargement initial

            if (int.TryParse(cmbClasse.SelectedValue.ToString(), out int classeId))
            {
                FiltrerEtudiantsParClasse(classeId);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une classe valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FiltrerEtudiantsParClasse(int classeId)
        {
            using (var db = new DBconnect())
            {
                var etudiants = db.Etudiants
                                  .Where(e => e.IdClasse == classeId) // Filtrer les étudiants par classe
                                  .Select(e => new
                                  {
                                      e.Id,
                                      e.Prenom,
                                      e.Nom,
                                      e.Email
                                  }).ToList();

                // Afficher les étudiants dans le DataGridView
                dataGridView1.DataSource = etudiants;
            }
        }

        private void GenererMeilleursEtudiants(int classeId)
        {
            using (var db = new DBconnect())
            {
                var meilleursEtudiants = db.Etudiants
                    .Where(e => e.IdClasse == classeId) // Filtrer par classe
                    .Select(e => new
                    {
                        e.Id,
                        e.Prenom,
                        e.Nom,
                        Moyenne = db.Notes
                            .Where(n => n.IdEtudiant == e.Id)
                            .Average(n => (double?)n.note) ?? 0 // Calculer la moyenne, 0 si pas de notes
                    })
                    .OrderByDescending(e => e.Moyenne) // Trier par moyenne décroissante
                    .Take(3) // Prendre les 3 meilleurs
                    .ToList();

                // Afficher dans le DataGridView
                dataGridView1.DataSource = meilleursEtudiants;
            }
        }

        private void btnMeilleursEtudiants_Click(object sender, EventArgs e)
        {
            if (cmbClasse.SelectedIndex == -1 || cmbClasse.SelectedValue == null)
            {
                MessageBox.Show("Veuillez sélectionner une classe.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (int.TryParse(cmbClasse.SelectedValue.ToString(), out int classeId))
            {
                GenererMeilleursEtudiants(classeId);
            }
            else
            {
                MessageBox.Show("Classe invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
