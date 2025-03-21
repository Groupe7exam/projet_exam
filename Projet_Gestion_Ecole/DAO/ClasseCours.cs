using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Gestion_Ecole.DAO
{
    class ClasseCours
    {
        public int ClasseId { get; set; }
        public virtual Classe Classe { get; set; }

        public int CoursId { get; set; }
        public virtual Cours Cours { get; set; }
    }
}
