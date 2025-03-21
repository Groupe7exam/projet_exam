using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Gestion_Ecole.DAO
{
    class Professeur
    {
        [Key]
        public int Id { get; set; } // Identifiant unique
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }

        // Navigation properties
        // Navigation properties
        public virtual ICollection<ProfesseurMatiere> ProfesseurMatieres { get; set; } = new List<ProfesseurMatiere>();
        public virtual ICollection<Cours> Cours { get; set; } = new List<Cours>(); // Ajoutez cette ligne
    }
}

