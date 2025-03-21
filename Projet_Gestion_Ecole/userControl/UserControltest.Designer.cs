namespace Projet_Gestion_Ecole.userControl
{
    partial class UserControltest
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
            this.bonjour = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bonjour
            // 
            this.bonjour.AutoSize = true;
            this.bonjour.Location = new System.Drawing.Point(294, 106);
            this.bonjour.Name = "bonjour";
            this.bonjour.Size = new System.Drawing.Size(51, 20);
            this.bonjour.TabIndex = 0;
            this.bonjour.Text = "label1";
            // 
            // UserControltest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bonjour);
            this.Name = "UserControltest";
            this.Size = new System.Drawing.Size(840, 501);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label bonjour;
    }
}
