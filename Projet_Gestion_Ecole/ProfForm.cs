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
    public partial class ProfForm: Form
    {
        public ProfForm()
        {
            InitializeComponent();
        }

        private void btnEtudiants_Click(object sender, EventArgs e)
        {
            FormManager.showEtudiantForm(ShowPanel);
        }

        private void btnClasses_Click(object sender, EventArgs e)
        {
            FormManager.showClasseForm(ShowPanel);
        }

        private void btnNotes_Click(object sender, EventArgs e)
        {
            FormManager.showNoteForm(ShowPanel);
        }
    }
}
