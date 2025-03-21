using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Projet_Gestion_Ecole.DAO;

namespace Projet_Gestion_Ecole
{
    class DBconnect : DbContext

    {
        public DBconnect() : base("ecoleConnect")
        {

        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<OTCode> OTCodes { get; set; }



        public DbSet<Professeur> Professeurs { get; set; }
        public DbSet<Cours> Cours { get; set; }
        public DbSet<Matiere> Matieres { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<ProfesseurMatiere> ProfesseurMatieres { get; set; }
        public DbSet<CoursMatiere> CoursMatieres { get; set; }
        public DbSet<ClasseCours> ClasseCours { get; set; }

        public DbSet<Etudiant> Etudiants { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Etudiant>()
            .HasRequired(e => e.Classe)
            .WithMany(c => c.Etudiants)
            .HasForeignKey(e => e.IdClasse);

            // Relation entre Note et Etudiant
            modelBuilder.Entity<Note>()
                .HasRequired(n => n.Etudiant)
                .WithMany(e => e.Notes)
                .HasForeignKey(n => n.IdEtudiant);

            // Relation entre Note et Matiere
            modelBuilder.Entity<Note>()
                .HasRequired(n => n.Matiere)
                .WithMany(m => m.Notes)
                .HasForeignKey(n => n.IdMatiere);
            // Configuration de la relation N:N entre Professeur et Matière via ProfesseurMatiere
            modelBuilder.Entity<ProfesseurMatiere>()
                .HasKey(pm => new { pm.ProfesseurId, pm.MatiereId, pm.ClasseId });

            modelBuilder.Entity<ProfesseurMatiere>()
                .HasRequired(pm => pm.Professeur)
                .WithMany(p => p.ProfesseurMatieres)
                .HasForeignKey(pm => pm.ProfesseurId);

            modelBuilder.Entity<ProfesseurMatiere>()
                .HasRequired(pm => pm.Matiere)
                .WithMany(m => m.ProfesseurMatieres)
                .HasForeignKey(pm => pm.MatiereId);

            // 🔹 Ajout de la relation entre Professeur et Classe via ProfesseurMatiere
            modelBuilder.Entity<ProfesseurMatiere>()
                .HasRequired(pm => pm.Classe)
                .WithMany(cl => cl.ProfesseurMatieres)
                .HasForeignKey(pm => pm.ClasseId);

            // Configuration de la relation N:N entre Cours et Matière via CoursMatiere
            modelBuilder.Entity<CoursMatiere>()
                .HasKey(cm => new { cm.CoursId, cm.MatiereId });

            modelBuilder.Entity<CoursMatiere>()
                .HasRequired(cm => cm.Cours)
                .WithMany(c => c.CoursMatieres)
                .HasForeignKey(cm => cm.CoursId);

            modelBuilder.Entity<CoursMatiere>()
                .HasRequired(cm => cm.Matiere)
                .WithMany(m => m.CoursMatieres)
                .HasForeignKey(cm => cm.MatiereId);

            // Configuration de la relation N:N entre Classe et Cours via ClasseCours
            modelBuilder.Entity<ClasseCours>()
                .HasKey(cc => new { cc.ClasseId, cc.CoursId });

            modelBuilder.Entity<ClasseCours>()
                .HasRequired(cc => cc.Classe)
                .WithMany(cl => cl.ClasseCours)
                .HasForeignKey(cc => cc.ClasseId);

            modelBuilder.Entity<ClasseCours>()
                .HasRequired(cc => cc.Cours)
                .WithMany(c => c.ClasseCours)
                .HasForeignKey(cc => cc.CoursId);

            modelBuilder.Entity<Etudiant>()
       .HasRequired(e => e.Classe)
       .WithMany(c => c.Etudiants)
       .HasForeignKey(e => e.IdClasse);
        }






    }
}
