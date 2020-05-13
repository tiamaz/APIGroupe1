using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Projet.API.Model
{
    public class User: IdentityUser<int>
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Ville { get; set; }
        public string Status { get; set; }

        public ICollection<Classe> classe { get; set; }
        public ICollection<EtudiantClasse> etudiantClasse { get; set; }
    }
}