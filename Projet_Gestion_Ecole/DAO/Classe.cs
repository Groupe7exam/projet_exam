using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Gestion_Ecole.DAO
{
    class Classe
    {
        public int Id { get; set; }
        public string NomClasse { get; set; }

        public List<Etudiant> Etudiants { get; set; }
        public List<Cours> Cours { get; set; }
        public List<Professeur> Professeurs { get; set; }
    }
}
