using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Gestion_Ecole.DAO
{
    class OTCode
    {
        public int Id { get; set; }
        public int IdUtilisateur { get; set; }
        public string Code { get; set; }
        public DateTime DateExpiration { get; set; }

        public Utilisateur Utilisateur { get; set; }

    }
}
