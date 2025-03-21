namespace Projet_Gestion_Ecole
{
    partial class FormEtudiants
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewEtudiants = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEtudiants)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewEtudiants
            // 
            this.dataGridViewEtudiants.AllowUserToAddRows = false;
            this.dataGridViewEtudiants.AllowUserToDeleteRows = false;
            this.dataGridViewEtudiants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEtudiants.Location = new System.Drawing.Point(153, 64);
            this.dataGridViewEtudiants.Name = "dataGridViewEtudiants";
            this.dataGridViewEtudiants.RowHeadersWidth = 62;
            this.dataGridViewEtudiants.RowTemplate.Height = 28;
            this.dataGridViewEtudiants.Size = new System.Drawing.Size(901, 406);
            this.dataGridViewEtudiants.TabIndex = 0;
            // 
            // FormEtudiants
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 531);
            this.Controls.Add(this.dataGridViewEtudiants);
            this.Name = "FormEtudiants";
            this.Text = "FormEtudiant";
            this.Load += new System.EventHandler(this.FormEtudiants_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEtudiants)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewEtudiants;
    }
}