namespace Projet_Gestion_Ecole.userControl
{
    partial class UserRapports
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbEtudiant = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.btnExporterPDF = new System.Windows.Forms.Button();
            this.cmbClasse = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMeilleursEtudiants = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbEtudiant
            // 
            this.cmbEtudiant.FormattingEnabled = true;
            this.cmbEtudiant.Location = new System.Drawing.Point(73, 98);
            this.cmbEtudiant.Name = "cmbEtudiant";
            this.cmbEtudiant.Size = new System.Drawing.Size(207, 28);
            this.cmbEtudiant.TabIndex = 0;
            this.cmbEtudiant.SelectedIndexChanged += new System.EventHandler(this.cmbEtudiant_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Etudiant";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(73, 152);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1090, 432);
            this.dataGridView1.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1009, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Exporter PDF";
            // 
            // btnExporterPDF
            // 
            this.btnExporterPDF.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnExporterPDF.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExporterPDF.ForeColor = System.Drawing.Color.Red;
            this.btnExporterPDF.Location = new System.Drawing.Point(1120, 14);
            this.btnExporterPDF.Name = "btnExporterPDF";
            this.btnExporterPDF.Size = new System.Drawing.Size(110, 41);
            this.btnExporterPDF.TabIndex = 13;
            this.btnExporterPDF.Text = "PDF";
            this.btnExporterPDF.UseVisualStyleBackColor = false;
            this.btnExporterPDF.Click += new System.EventHandler(this.btnExporterPDF_Click);
            // 
            // cmbClasse
            // 
            this.cmbClasse.FormattingEnabled = true;
            this.cmbClasse.Location = new System.Drawing.Point(401, 98);
            this.cmbClasse.Name = "cmbClasse";
            this.cmbClasse.Size = new System.Drawing.Size(207, 28);
            this.cmbClasse.TabIndex = 15;
            this.cmbClasse.SelectedIndexChanged += new System.EventHandler(this.cmbClasse_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(397, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Classe";
            // 
            // btnMeilleursEtudiants
            // 
            this.btnMeilleursEtudiants.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMeilleursEtudiants.Location = new System.Drawing.Point(750, 75);
            this.btnMeilleursEtudiants.Name = "btnMeilleursEtudiants";
            this.btnMeilleursEtudiants.Size = new System.Drawing.Size(184, 51);
            this.btnMeilleursEtudiants.TabIndex = 17;
            this.btnMeilleursEtudiants.Text = "meilleur etudiant";
            this.btnMeilleursEtudiants.UseVisualStyleBackColor = true;
            this.btnMeilleursEtudiants.Click += new System.EventHandler(this.btnMeilleursEtudiants_Click);
            // 
            // UserRapports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnMeilleursEtudiants);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbClasse);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnExporterPDF);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbEtudiant);
            this.Name = "UserRapports";
            this.Size = new System.Drawing.Size(1245, 644);
            this.Load += new System.EventHandler(this.UserRapports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbEtudiant;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnExporterPDF;
        private System.Windows.Forms.ComboBox cmbClasse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMeilleursEtudiants;
    }
}
