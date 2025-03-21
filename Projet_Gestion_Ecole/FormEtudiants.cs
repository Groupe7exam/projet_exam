using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Projet_Gestion_Ecole
{
    public partial class FormEtudiants : Form
    {
        private int _classeId; // Stocke l'ID de la classe

        public FormEtudiants(int classeId)
        {
            InitializeComponent();
            _classeId = classeId; // Récupère l'ID
        }

        private void FormEtudiants_Load(object sender, EventArgs e)
        {
            ChargerEtudiants();
        }

        private void ChargerEtudiants()
        {
            using (var db = new DBconnect())
            {
                var etudiants = db.Etudiants
                    .Where(e => e.IdClasse == _classeId) // 🔹 Filtre par classe
                    .Select(e => new
                    {
                        e.Id,
                        Nom_Complet = e.Nom + " " + e.Prenom, // 🔹 Nom complet
                        e.Matricule,
                        e.Email,
                        e.Telephone
                    })
                    .ToList();

                dataGridViewEtudiants.DataSource = etudiants;
                dataGridViewEtudiants.Columns["Id"].Visible = false; // 🔹 Cache l'ID
            }
        }
    }
}
