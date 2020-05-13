using System.Collections.Generic;

namespace Projet.API.Model
{
    public class Classe
    {
        public int ClasseId { get; set; }
        public string Nom { get; set; }
        public string Filiere { get; set; }
        public string Niveau { get; set; }

        public int UserId { get; set; }
        
        public ICollection<Publication> publication { get; set; }
        public ICollection<EtudiantClasse> etudiantClasse { get; set; }

    }
}