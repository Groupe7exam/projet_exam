namespace Projet_Gestion_Ecole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomClasse = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomCours = c.String(),
                        Description = c.String(),
                        Classe_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.Classe_Id)
                .Index(t => t.Classe_Id);
            
            CreateTable(
                "dbo.Matieres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomMatiere = c.String(),
                        Cours_Id = c.Int(),
                        Professeur_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cours", t => t.Cours_Id)
                .ForeignKey("dbo.Professeurs", t => t.Professeur_Id)
                .Index(t => t.Cours_Id)
                .Index(t => t.Professeur_Id);
            
            CreateTable(
                "dbo.Etudiants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Matricule = c.String(),
                        Nom = c.String(),
                        Prenom = c.String(),
                        DateNaissance = c.DateTime(nullable: false),
                        Sexe = c.String(),
                        Adresse = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                        IdClasse = c.Int(nullable: false),
                        Classe_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.Classe_Id)
                .Index(t => t.Classe_Id);
            
            CreateTable(
                "dbo.Professeurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Email = c.String(),
                        Telephone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdEtudiant = c.Int(nullable: false),
                        IdMatiere = c.Int(nullable: false),
                        note = c.Single(nullable: false),
                        Etudiant_Id = c.Int(),
                        Matiere_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Etudiants", t => t.Etudiant_Id)
                .ForeignKey("dbo.Matieres", t => t.Matiere_Id)
                .Index(t => t.Etudiant_Id)
                .Index(t => t.Matiere_Id);
            
            CreateTable(
                "dbo.OTCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdUtilisateur = c.Int(nullable: false),
                        Code = c.String(),
                        DateExpiration = c.DateTime(nullable: false),
                        Utilisateur_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Utilisateurs", t => t.Utilisateur_Id)
                .Index(t => t.Utilisateur_Id);
            
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomUtilisateur = c.String(),
                        MotDePasse = c.String(),
                        Role = c.String(),
                        Telephone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProfesseurClasses",
                c => new
                    {
                        Professeur_Id = c.Int(nullable: false),
                        Classe_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Professeur_Id, t.Classe_Id })
                .ForeignKey("dbo.Professeurs", t => t.Professeur_Id, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.Classe_Id, cascadeDelete: true)
                .Index(t => t.Professeur_Id)
                .Index(t => t.Classe_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OTCodes", "Utilisateur_Id", "dbo.Utilisateurs");
            DropForeignKey("dbo.Notes", "Matiere_Id", "dbo.Matieres");
            DropForeignKey("dbo.Notes", "Etudiant_Id", "dbo.Etudiants");
            DropForeignKey("dbo.Matieres", "Professeur_Id", "dbo.Professeurs");
            DropForeignKey("dbo.ProfesseurClasses", "Classe_Id", "dbo.Classes");
            DropForeignKey("dbo.ProfesseurClasses", "Professeur_Id", "dbo.Professeurs");
            DropForeignKey("dbo.Etudiants", "Classe_Id", "dbo.Classes");
            DropForeignKey("dbo.Cours", "Classe_Id", "dbo.Classes");
            DropForeignKey("dbo.Matieres", "Cours_Id", "dbo.Cours");
            DropIndex("dbo.ProfesseurClasses", new[] { "Classe_Id" });
            DropIndex("dbo.ProfesseurClasses", new[] { "Professeur_Id" });
            DropIndex("dbo.OTCodes", new[] { "Utilisateur_Id" });
            DropIndex("dbo.Notes", new[] { "Matiere_Id" });
            DropIndex("dbo.Notes", new[] { "Etudiant_Id" });
            DropIndex("dbo.Etudiants", new[] { "Classe_Id" });
            DropIndex("dbo.Matieres", new[] { "Professeur_Id" });
            DropIndex("dbo.Matieres", new[] { "Cours_Id" });
            DropIndex("dbo.Cours", new[] { "Classe_Id" });
            DropTable("dbo.ProfesseurClasses");
            DropTable("dbo.Utilisateurs");
            DropTable("dbo.OTCodes");
            DropTable("dbo.Notes");
            DropTable("dbo.Professeurs");
            DropTable("dbo.Etudiants");
            DropTable("dbo.Matieres");
            DropTable("dbo.Cours");
            DropTable("dbo.Classes");
        }
    }
}
