using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Gestion_Ecole.DAO
{
    class Classe
    {
        [Key]
        public int Id { get; set; }
        public string NomClasse { get; set; }

        public virtual ICollection<Etudiant> Etudiants { get; set; } = new List<Etudiant>();
        public virtual ICollection<ClasseCours> ClasseCours { get; set; } = new List<ClasseCours>();
        public virtual ICollection<ProfesseurMatiere> ProfesseurMatieres { get; set; } = new List<ProfesseurMatiere>();
    }
}
