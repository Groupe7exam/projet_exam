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
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Cours> Cours { get; set; }
        public DbSet<Matiere> Matieres { get; set; }
        public DbSet<Professeur> Professeurs { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<OTCode> OTCodes { get; set; }

       

       
    }
}
