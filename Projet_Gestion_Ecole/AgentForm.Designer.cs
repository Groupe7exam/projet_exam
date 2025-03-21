namespace Projet_Gestion_Ecole
{
    partial class AgentForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHam = new System.Windows.Forms.PictureBox();
            this.SideBar = new System.Windows.Forms.FlowLayoutPanel();
            this.btnEtudiants = new System.Windows.Forms.Button();
            this.btnNotes = new System.Windows.Forms.Button();
            this.btnRapport = new System.Windows.Forms.Button();
            this.ShowPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHam)).BeginInit();
            this.SideBar.SuspendLayout();
            this.ShowPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkRed;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnHam);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1664, 64);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(72, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Page Agent";
            // 
            // btnHam
            // 
            this.btnHam.Image = global::Projet_Gestion_Ecole.Properties.Resources.Hamburger_icon_svg;
            this.btnHam.Location = new System.Drawing.Point(12, 11);
            this.btnHam.Name = "btnHam";
            this.btnHam.Size = new System.Drawing.Size(45, 36);
            this.btnHam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnHam.TabIndex = 1;
            this.btnHam.TabStop = false;
            // 
            // SideBar
            // 
            this.SideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.SideBar.Controls.Add(this.btnEtudiants);
            this.SideBar.Controls.Add(this.btnNotes);
            this.SideBar.Controls.Add(this.btnRapport);
            this.SideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.SideBar.Location = new System.Drawing.Point(0, 64);
            this.SideBar.Name = "SideBar";
            this.SideBar.Size = new System.Drawing.Size(273, 901);
            this.SideBar.TabIndex = 2;
            // 
            // btnEtudiants
            // 
            this.btnEtudiants.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnEtudiants.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnEtudiants.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnEtudiants.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnEtudiants.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEtudiants.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEtudiants.ForeColor = System.Drawing.Color.GhostWhite;
            this.btnEtudiants.Location = new System.Drawing.Point(3, 3);
            this.btnEtudiants.Name = "btnEtudiants";
            this.btnEtudiants.Size = new System.Drawing.Size(270, 82);
            this.btnEtudiants.TabIndex = 3;
            this.btnEtudiants.Text = "Etudiants";
            this.btnEtudiants.UseVisualStyleBackColor = false;
            this.btnEtudiants.Click += new System.EventHandler(this.btnEtudiants_Click);
            // 
            // btnNotes
            // 
            this.btnNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnNotes.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnNotes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnNotes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnNotes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotes.ForeColor = System.Drawing.Color.GhostWhite;
            this.btnNotes.Location = new System.Drawing.Point(3, 91);
            this.btnNotes.Name = "btnNotes";
            this.btnNotes.Size = new System.Drawing.Size(270, 82);
            this.btnNotes.TabIndex = 7;
            this.btnNotes.Text = "Notes";
            this.btnNotes.UseVisualStyleBackColor = true;
            this.btnNotes.Click += new System.EventHandler(this.btnNotes_Click);
            // 
            // btnRapport
            // 
            this.btnRapport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnRapport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnRapport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnRapport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.btnRapport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRapport.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRapport.ForeColor = System.Drawing.Color.GhostWhite;
            this.btnRapport.Location = new System.Drawing.Point(3, 179);
            this.btnRapport.Name = "btnRapport";
            this.btnRapport.Size = new System.Drawing.Size(270, 82);
            this.btnRapport.TabIndex = 9;
            this.btnRapport.Text = "Rapports";
            this.btnRapport.UseVisualStyleBackColor = false;
            this.btnRapport.Click += new System.EventHandler(this.btnRapport_Click);
            // 
            // ShowPanel
            // 
            this.ShowPanel.Controls.Add(this.label2);
            this.ShowPanel.Controls.Add(this.pictureBox1);
            this.ShowPanel.Location = new System.Drawing.Point(279, 70);
            this.ShowPanel.Name = "ShowPanel";
            this.ShowPanel.Size = new System.Drawing.Size(1385, 883);
            this.ShowPanel.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MV Boli", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(520, 74);
            this.label2.TabIndex = 3;
            this.label2.Text = "Bienvenue dans votre page d Agent\r\n\r\n";
            // 
            // pictureBox1
            // 
           // this.pictureBox1.Image = global::Projet_Gestion_Ecole.Properties.Resources.images;
            this.pictureBox1.Location = new System.Drawing.Point(21, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(178, 139);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(24)))), ((int)(((byte)(29)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.GhostWhite;
            this.button1.Location = new System.Drawing.Point(1156, -7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(270, 82);
            this.button1.TabIndex = 10;
            this.button1.Text = "Deconnection";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AgentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1664, 965);
            this.Controls.Add(this.ShowPanel);
            this.Controls.Add(this.SideBar);
            this.Controls.Add(this.panel1);
            this.Name = "AgentForm";
            this.Text = "AgentForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHam)).EndInit();
            this.SideBar.ResumeLayout(false);
            this.ShowPanel.ResumeLayout(false);
            this.ShowPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox btnHam;
        private System.Windows.Forms.FlowLayoutPanel SideBar;
        private System.Windows.Forms.Button btnEtudiants;
        private System.Windows.Forms.Button btnNotes;
        private System.Windows.Forms.Button btnRapport;
        private System.Windows.Forms.Panel ShowPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}