using System;

namespace Projet.API.Model
{
    public class Publication
    {
        public int PublicationId { get; set; }
        public string Contenu { get; set; }
        public DateTime DatePublication { get; set; }
        public int ClasseId { get; set; }

        public Publication()
        {
            DatePublication = DateTime.Now;
        }
    }
}