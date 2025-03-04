using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Gestion_Ecole.DAO
{
    class Cours
    {
        public int Id { get; set; }
        public string NomCours { get; set; }
        public string Description { get; set; }

        public List<Matiere> Matieres { get; set; }
    }
}
