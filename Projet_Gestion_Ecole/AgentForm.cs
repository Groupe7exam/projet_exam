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
    public partial class AgentForm: Form
    {
        public AgentForm()
        {
            InitializeComponent();
        }

        private void btnProfesseur_Click(object sender, EventArgs e)
        {

        }

        private void btnEtudiants_Click(object sender, EventArgs e)
        {
            FormManager.showEtudiantForm(ShowPanel);
        }

        private void btnNotes_Click(object sender, EventArgs e)
        {
            FormManager.showNoteForm(ShowPanel);
        }

        private void btnRapport_Click(object sender, EventArgs e)
        {
            FormManager.showRapportForm(ShowPanel);
        }
    }
}
