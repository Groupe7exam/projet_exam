using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Gestion_Ecole.DAO
{
    class SessionUtilisateur
    {

        public static int? UtilisateurId { get; private set; }
        public static string NomUtilisateur { get; private set; }
        public static string Role { get; private set; }
        public static string Telephone { get; private set; }

        public static void SetSession(Utilisateur utilisateur)
        {
            UtilisateurId = utilisateur.Id;
            NomUtilisateur = utilisateur.NomUtilisateur;
            Role = utilisateur.Role;
            Telephone = utilisateur.Telephone;
        }

        public static void Deconnecter()
        {
            UtilisateurId = null;
            NomUtilisateur = null;
            Role = null;
            Telephone = null;
        }

        public static bool EstAdmin()
        {
            return Role == "ADMIN";
        }

        public static bool EstProfesseur()
        {
            return Role == "Professeur";
        }
    }
}
