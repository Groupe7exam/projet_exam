using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Gestion_Ecole.DAO
{
    class CoursMatiere
    {
        public int CoursId { get; set; }
        public virtual Cours Cours { get; set; }

        public int MatiereId { get; set; }
        public virtual Matiere Matiere { get; set; }
    }
}
