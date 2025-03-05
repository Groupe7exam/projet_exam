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
    public partial class AdminForm: Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        bool SideBarTransition = true;
        private void Transition_Tick(object sender, EventArgs e)
        {
            if (SideBarTransition)
            {
                SideBar.Width -= 10;
                if (SideBar.Width <= 54)
                {
                    SideBarTransition = false;
                    Transition.Stop();
                }
            }
            else
            {
                SideBar.Width += 10;
                if (SideBar.Width >= 273)
                {
                    SideBarTransition = true;
                    Transition.Stop();
                }

            }
        }

        private void btnHam_Click(object sender, EventArgs e)
        {
            Transition.Start();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnUser_Click(object sender, EventArgs e)
        {

        }

        private void btnEtudiants_Click(object sender, EventArgs e)
        {

        }

        private void btnClasses_Click(object sender, EventArgs e)
        {

        }

        private void btnProfesseur_Click(object sender, EventArgs e)
        {

        }

        private void btnMatieres_Click(object sender, EventArgs e)
        {

        }

        private void btnNotes_Click(object sender, EventArgs e)
        {

        }
    }
}
