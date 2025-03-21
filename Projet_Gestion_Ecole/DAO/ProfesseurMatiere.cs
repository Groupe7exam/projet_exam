using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Gestion_Ecole.DAO
{
    class ProfesseurMatiere
    {
        public int ProfesseurId { get; set; }
        public virtual Professeur Professeur { get; set; }

        public int MatiereId { get; set; }
        public virtual Matiere Matiere { get; set; }

        public int ClasseId { get; set; }
        public virtual Classe Classe { get; set; }
    }
}
