using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Gestion_Ecole.DAO
{
    class Matiere
    {
        [Key]
        public int Id { get; set; }
        public string NomMatiere { get; set; }

        // Navigation properties
        public virtual ICollection<ProfesseurMatiere> ProfesseurMatieres { get; set; } = new List<ProfesseurMatiere>();
        public virtual ICollection<CoursMatiere> CoursMatieres { get; set; } = new List<CoursMatiere>();

        public virtual ICollection<Note> Notes { get; set; }

    }
}
