using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vonage;
using Vonage.Request;
using Vonage.Messaging;
using Vonage.Messages.Sms;



namespace Projet_Gestion_Ecole
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }













































        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void InscriptionPage_Click(object sender, EventArgs e)
        {
            FormManager.showInscriptionForm();
        }

        private async void btnConnexion_Click(object sender, EventArgs e)
        {
            string nomUser = txtUser.Text;
            string password = txtPassword.Text;

            // Vérification que les champs ne sont pas vides
            if (string.IsNullOrEmpty(nomUser) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Veuillez remplir tous les champs", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var db = new DBconnect())
            {
                var user = db.Utilisateurs.FirstOrDefault(u => u.NomUtilisateur == nomUser);
                if (user != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(password, user.MotDePasse))
                    {
                        // Générer un code de vérification
                        string verificationCode = GenerateVerificationCode();

                        // Envoyer le code par SMS
                        await SendSmsAsync(user.Telephone, verificationCode);

                        // Demander à l'utilisateur de saisir le code de vérification
                        string inputCode = PromptForCode();

                        if (inputCode == verificationCode)
                        {
                            // Connexion réussie
                            NavigateToUserRole(user.Role);
                        }
                        else
                        {
                            MessageBox.Show("Code de vérification incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mot de passe incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Nom d'utilisateur invalide", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private string PromptForCode()
        {
            // Création d'une boîte de dialogue personnalisée pour demander le code
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                Text = "Vérification A2F"
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = "Entrez le code de vérification:" };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 240 };
            Button confirmation = new Button() { Text = "Ok", Left = 200, Width = 60, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.ShowDialog();

            return textBox.Text;
        }

        private async Task SendSmsAsync(string to, string code)
        {
            var credentials = Credentials.FromApiKeyAndSecret("8209fdeb", "P3HsylzqxPE1TjPh");
            var client = new VonageClient(credentials);

            var smsRequest = new SendSmsRequest
            {
                To = to,
                From = to, // Remplacez par un numéro enregistré chez Vonage
                Text = $"Votre code de vérification est : {code}"
            };

            var response = await client.SmsClient.SendAnSmsAsync(smsRequest);

            if (response.Messages[0].Status != "0")
            {
                MessageBox.Show($"Erreur lors de l'envoi du SMS: {response.Messages[0].ErrorText}");
            }
        }


        private string GenerateVerificationCode(int length = 6)
        {
            Random random = new Random();
            return random.Next(0, (int)Math.Pow(10, length)).ToString("D" + length);
        }

        private void NavigateToUserRole(string role)
        {
            switch (role)
            {
                case "ADMIN":
                    FormManager.showAdminForm();
                    break;
                case "DE":
                    FormManager.showFormDE();
                    break;
                case "Agent":
                    FormManager.showAgentForm();
                    break;
                case "Professeur":
                    FormManager.showProfForm();
                    break;
                default:
                    MessageBox.Show("Rôle inconnu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
