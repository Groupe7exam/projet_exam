using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Gestion_Ecole.DAO
{
    class Cours
    {
        [Key]
        public int Id { get; set; }
        public string NomCours { get; set; }

        public string NomClasse { get; set; }
        public string Description { get; set; }

        // Navigation properties
        // Navigation properties
        public virtual ICollection<CoursMatiere> CoursMatieres { get; set; } = new List<CoursMatiere>();

        public virtual ICollection<ClasseCours> ClasseCours { get; set; } = new List<ClasseCours>(); // Ajouté pour la relation N:N avec Classe

        public virtual ICollection<Professeur> Professeurs { get; set; }

        // Navigation properties
        
    }
}
