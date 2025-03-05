using System;
using System.Windows.Forms;

namespace Projet_Gestion_Ecole
{
    internal static class FormManager
    {
        public static Form1 form1 { get; set; }
        public static InscriptionForm InscriptionForm { get; set; }
        public static AdminForm AdminForm { get; set; }
        public static ProfForm ProfForm { get; set; }
        public static AgentForm AgentForm { get; set; }
        public static FormDE FormDE { get; set; }

        public static void showLoginForm()
        {
            if (form1 == null || form1.IsDisposed)
            {
                form1 = new Form1();
            }
            if (InscriptionForm != null && !InscriptionForm.IsDisposed)
            {
                InscriptionForm.Hide();
            }
            if (AdminForm != null && !AdminForm.IsDisposed)
            {
                AdminForm.Hide();
            }
            if (ProfForm != null && !ProfForm.IsDisposed)
            {
                ProfForm.Hide();
            }
            if (AgentForm != null && !AgentForm.IsDisposed)
            {
                AgentForm.Hide();
            }
            if (FormDE != null && !FormDE.IsDisposed)
            {
                FormDE.Hide();
            }
            form1.Show();
        }

        public static void showInscriptionForm()
        {
            if (InscriptionForm == null || InscriptionForm.IsDisposed)
            {
                InscriptionForm = new InscriptionForm();
            }
            if (form1 != null && !form1.IsDisposed)
            {
                form1.Hide();
            }
            if (AdminForm != null && !AdminForm.IsDisposed)
            {
                AdminForm.Hide();
            }
            if (ProfForm != null && !ProfForm.IsDisposed)
            {
                ProfForm.Hide();
            }
            if (AgentForm != null && !AgentForm.IsDisposed)
            {
                AgentForm.Hide();
            }
            if (FormDE != null && !FormDE.IsDisposed)
            {
                FormDE.Hide();
            }
            InscriptionForm.Show();
        }

        public static void showAdminForm()
        {
            if (AdminForm == null || AdminForm.IsDisposed)
            {
                AdminForm = new AdminForm();
            }

            // Cache les autres formulaires si ils sont visibles
            if (form1 != null && !form1.IsDisposed)
            {
                form1.Hide();
            }
            if (InscriptionForm != null && !InscriptionForm.IsDisposed)
            {
                InscriptionForm.Hide();
            }
            if (ProfForm != null && !ProfForm.IsDisposed)
            {
                ProfForm.Hide();
            }
            if (AgentForm != null && !AgentForm.IsDisposed)
            {
                AgentForm.Hide();
            }
            if (FormDE != null && !FormDE.IsDisposed)
            {
                FormDE.Hide();
            }
            AdminForm.Show();

        }

        public static void showAdminForm2(Panel panelContent)
        {
            if (AdminForm == null || AdminForm.IsDisposed)
            {
                AdminForm = new AdminForm();
            }

            // Cache les autres formulaires si ils sont visibles
            if (form1 != null && !form1.IsDisposed)
            {
                form1.Hide();
            }
            if (InscriptionForm != null && !InscriptionForm.IsDisposed)
            {
                InscriptionForm.Hide();
            }
            if (ProfForm != null && !ProfForm.IsDisposed)
            {
                ProfForm.Hide();
            }
            if (AgentForm != null && !AgentForm.IsDisposed)
            {
                AgentForm.Hide();
            }
            if (FormDE != null && !FormDE.IsDisposed)
            {
                FormDE.Hide();
            }

            // Assurez-vous de vider le panel avant d'ajouter un contrôle
            panelContent.Controls.Clear();

            // Ajoute un UserControl ou le formulaire désiré dans le Panel
            userControl.UserControlAdminPage userControlAdmin = new userControl.UserControlAdminPage();
            userControlAdmin.Dock = DockStyle.Fill;  // Assure que le UserControl occupe tout l'espace
            panelContent.Controls.Add(userControlAdmin);
        }
        public static void showProfForm()
        {
            if (ProfForm == null || ProfForm.IsDisposed)
            {
                ProfForm = new ProfForm();
            }
            if (form1 != null && !form1.IsDisposed)
            {
                form1.Hide();
            }
            if (InscriptionForm != null && !InscriptionForm.IsDisposed)
            {
                InscriptionForm.Hide();
            }
            if (AdminForm != null && !AdminForm.IsDisposed)
            {
                AdminForm.Hide();
            }
            if (AgentForm != null && !AgentForm.IsDisposed)
            {
                AgentForm.Hide();
            }
            if (FormDE != null && !FormDE.IsDisposed)
            {
                FormDE.Hide();
            }
            ProfForm.Show();
        }

        public static void showAgentForm()
        {
            if (AgentForm == null || AgentForm.IsDisposed)
            {
                AgentForm = new AgentForm();
            }
            if (form1 != null && !form1.IsDisposed)
            {
                form1.Hide();
            }
            if (InscriptionForm != null && !InscriptionForm.IsDisposed)
            {
                InscriptionForm.Hide();
            }
            if (AdminForm != null && !AdminForm.IsDisposed)
            {
                AdminForm.Hide();
            }
            if (ProfForm != null && !ProfForm.IsDisposed)
            {
                ProfForm.Hide();
            }
            if (FormDE != null && !FormDE.IsDisposed)
            {
                FormDE.Hide();
            }
            AgentForm.Show();
        }

        public static void showFormDE()
        {
            if (FormDE == null || FormDE.IsDisposed)
            {
                FormDE = new FormDE();
            }
            if (form1 != null && !form1.IsDisposed)
            {
                form1.Hide();
            }
            if (InscriptionForm != null && !InscriptionForm.IsDisposed)
            {
                InscriptionForm.Hide();
            }
            if (AdminForm != null && !AdminForm.IsDisposed)
            {
                AdminForm.Hide();
            }
            if (ProfForm != null && !ProfForm.IsDisposed)
            {
                ProfForm.Hide();
            }
            if (AgentForm != null && !AgentForm.IsDisposed)
            {
                AgentForm.Hide();
            }
            FormDE.Show();
        }

       
    }
}
