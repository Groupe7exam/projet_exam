namespace Projet_Gestion_Ecole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Moussa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Matieres", "Cours_Id", "dbo.Cours");
            DropForeignKey("dbo.Cours", "Classe_Id", "dbo.Classes");
            DropForeignKey("dbo.ProfesseurClasses", "Professeur_Id", "dbo.Professeurs");
            DropForeignKey("dbo.ProfesseurClasses", "Classe_Id", "dbo.Classes");
            DropForeignKey("dbo.Matieres", "Professeur_Id", "dbo.Professeurs");
            DropForeignKey("dbo.Etudiants", "Classe_Id", "dbo.Classes");
            DropForeignKey("dbo.Notes", "Etudiant_Id", "dbo.Etudiants");
            DropForeignKey("dbo.Notes", "Matiere_Id", "dbo.Matieres");
            DropIndex("dbo.Cours", new[] { "Classe_Id" });
            DropIndex("dbo.Matieres", new[] { "Cours_Id" });
            DropIndex("dbo.Matieres", new[] { "Professeur_Id" });
            DropIndex("dbo.Etudiants", new[] { "Classe_Id" });
            DropIndex("dbo.Notes", new[] { "Etudiant_Id" });
            DropIndex("dbo.Notes", new[] { "Matiere_Id" });
            DropIndex("dbo.ProfesseurClasses", new[] { "Professeur_Id" });
            DropIndex("dbo.ProfesseurClasses", new[] { "Classe_Id" });
            DropColumn("dbo.Etudiants", "IdClasse");
            DropColumn("dbo.Notes", "IdEtudiant");
            DropColumn("dbo.Notes", "IdMatiere");
            RenameColumn(table: "dbo.Etudiants", name: "Classe_Id", newName: "IdClasse");
            RenameColumn(table: "dbo.Notes", name: "Etudiant_Id", newName: "IdEtudiant");
            RenameColumn(table: "dbo.Notes", name: "Matiere_Id", newName: "IdMatiere");
            CreateTable(
                "dbo.ClasseCours",
                c => new
                    {
                        ClasseId = c.Int(nullable: false),
                        CoursId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClasseId, t.CoursId })
                .ForeignKey("dbo.Classes", t => t.ClasseId, cascadeDelete: true)
                .ForeignKey("dbo.Cours", t => t.CoursId, cascadeDelete: true)
                .Index(t => t.ClasseId)
                .Index(t => t.CoursId);
            
            CreateTable(
                "dbo.CoursMatieres",
                c => new
                    {
                        CoursId = c.Int(nullable: false),
                        MatiereId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CoursId, t.MatiereId })
                .ForeignKey("dbo.Cours", t => t.CoursId, cascadeDelete: true)
                .ForeignKey("dbo.Matieres", t => t.MatiereId, cascadeDelete: true)
                .Index(t => t.CoursId)
                .Index(t => t.MatiereId);
            
            CreateTable(
                "dbo.ProfesseurMatieres",
                c => new
                    {
                        ProfesseurId = c.Int(nullable: false),
                        MatiereId = c.Int(nullable: false),
                        ClasseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProfesseurId, t.MatiereId, t.ClasseId })
                .ForeignKey("dbo.Classes", t => t.ClasseId, cascadeDelete: true)
                .ForeignKey("dbo.Matieres", t => t.MatiereId, cascadeDelete: true)
                .ForeignKey("dbo.Professeurs", t => t.ProfesseurId, cascadeDelete: true)
                .Index(t => t.ProfesseurId)
                .Index(t => t.MatiereId)
                .Index(t => t.ClasseId);
            
            CreateTable(
                "dbo.ProfesseurCours",
                c => new
                    {
                        Professeur_Id = c.Int(nullable: false),
                        Cours_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Professeur_Id, t.Cours_Id })
                .ForeignKey("dbo.Professeurs", t => t.Professeur_Id, cascadeDelete: true)
                .ForeignKey("dbo.Cours", t => t.Cours_Id, cascadeDelete: true)
                .Index(t => t.Professeur_Id)
                .Index(t => t.Cours_Id);
            
            AddColumn("dbo.Cours", "NomClasse", c => c.String());
            AlterColumn("dbo.Etudiants", "IdClasse", c => c.Int(nullable: false));
            AlterColumn("dbo.Notes", "IdEtudiant", c => c.Int(nullable: false));
            AlterColumn("dbo.Notes", "IdMatiere", c => c.Int(nullable: false));
            CreateIndex("dbo.Etudiants", "IdClasse");
            CreateIndex("dbo.Notes", "IdEtudiant");
            CreateIndex("dbo.Notes", "IdMatiere");
            AddForeignKey("dbo.Etudiants", "IdClasse", "dbo.Classes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Notes", "IdEtudiant", "dbo.Etudiants", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Notes", "IdMatiere", "dbo.Matieres", "Id", cascadeDelete: true);
            DropColumn("dbo.Cours", "Classe_Id");
            DropColumn("dbo.Matieres", "Cours_Id");
            DropColumn("dbo.Matieres", "Professeur_Id");
            DropTable("dbo.ProfesseurClasses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProfesseurClasses",
                c => new
                    {
                        Professeur_Id = c.Int(nullable: false),
                        Classe_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Professeur_Id, t.Classe_Id });
            
            AddColumn("dbo.Matieres", "Professeur_Id", c => c.Int());
            AddColumn("dbo.Matieres", "Cours_Id", c => c.Int());
            AddColumn("dbo.Cours", "Classe_Id", c => c.Int());
            DropForeignKey("dbo.Notes", "IdMatiere", "dbo.Matieres");
            DropForeignKey("dbo.Notes", "IdEtudiant", "dbo.Etudiants");
            DropForeignKey("dbo.Etudiants", "IdClasse", "dbo.Classes");
            DropForeignKey("dbo.ClasseCours", "CoursId", "dbo.Cours");
            DropForeignKey("dbo.ClasseCours", "ClasseId", "dbo.Classes");
            DropForeignKey("dbo.CoursMatieres", "MatiereId", "dbo.Matieres");
            DropForeignKey("dbo.CoursMatieres", "CoursId", "dbo.Cours");
            DropForeignKey("dbo.ProfesseurMatieres", "ProfesseurId", "dbo.Professeurs");
            DropForeignKey("dbo.ProfesseurMatieres", "MatiereId", "dbo.Matieres");
            DropForeignKey("dbo.ProfesseurMatieres", "ClasseId", "dbo.Classes");
            DropForeignKey("dbo.ProfesseurCours", "Cours_Id", "dbo.Cours");
            DropForeignKey("dbo.ProfesseurCours", "Professeur_Id", "dbo.Professeurs");
            DropIndex("dbo.ProfesseurCours", new[] { "Cours_Id" });
            DropIndex("dbo.ProfesseurCours", new[] { "Professeur_Id" });
            DropIndex("dbo.ProfesseurMatieres", new[] { "ClasseId" });
            DropIndex("dbo.ProfesseurMatieres", new[] { "MatiereId" });
            DropIndex("dbo.ProfesseurMatieres", new[] { "ProfesseurId" });
            DropIndex("dbo.CoursMatieres", new[] { "MatiereId" });
            DropIndex("dbo.CoursMatieres", new[] { "CoursId" });
            DropIndex("dbo.Notes", new[] { "IdMatiere" });
            DropIndex("dbo.Notes", new[] { "IdEtudiant" });
            DropIndex("dbo.Etudiants", new[] { "IdClasse" });
            DropIndex("dbo.ClasseCours", new[] { "CoursId" });
            DropIndex("dbo.ClasseCours", new[] { "ClasseId" });
            AlterColumn("dbo.Notes", "IdMatiere", c => c.Int());
            AlterColumn("dbo.Notes", "IdEtudiant", c => c.Int());
            AlterColumn("dbo.Etudiants", "IdClasse", c => c.Int());
            DropColumn("dbo.Cours", "NomClasse");
            DropTable("dbo.ProfesseurCours");
            DropTable("dbo.ProfesseurMatieres");
            DropTable("dbo.CoursMatieres");
            DropTable("dbo.ClasseCours");
            RenameColumn(table: "dbo.Notes", name: "IdMatiere", newName: "Matiere_Id");
            RenameColumn(table: "dbo.Notes", name: "IdEtudiant", newName: "Etudiant_Id");
            RenameColumn(table: "dbo.Etudiants", name: "IdClasse", newName: "Classe_Id");
            AddColumn("dbo.Notes", "IdMatiere", c => c.Int(nullable: false));
            AddColumn("dbo.Notes", "IdEtudiant", c => c.Int(nullable: false));
            AddColumn("dbo.Etudiants", "IdClasse", c => c.Int(nullable: false));
            CreateIndex("dbo.ProfesseurClasses", "Classe_Id");
            CreateIndex("dbo.ProfesseurClasses", "Professeur_Id");
            CreateIndex("dbo.Notes", "Matiere_Id");
            CreateIndex("dbo.Notes", "Etudiant_Id");
            CreateIndex("dbo.Etudiants", "Classe_Id");
            CreateIndex("dbo.Matieres", "Professeur_Id");
            CreateIndex("dbo.Matieres", "Cours_Id");
            CreateIndex("dbo.Cours", "Classe_Id");
            AddForeignKey("dbo.Notes", "Matiere_Id", "dbo.Matieres", "Id");
            AddForeignKey("dbo.Notes", "Etudiant_Id", "dbo.Etudiants", "Id");
            AddForeignKey("dbo.Etudiants", "Classe_Id", "dbo.Classes", "Id");
            AddForeignKey("dbo.Matieres", "Professeur_Id", "dbo.Professeurs", "Id");
            AddForeignKey("dbo.ProfesseurClasses", "Classe_Id", "dbo.Classes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProfesseurClasses", "Professeur_Id", "dbo.Professeurs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Cours", "Classe_Id", "dbo.Classes", "Id");
            AddForeignKey("dbo.Matieres", "Cours_Id", "dbo.Cours", "Id");
        }
    }
}
