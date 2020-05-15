using System.ComponentModel.DataAnnotations;

namespace Projet.API.Dto
{
    public class UserForRegisterDto
    {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public string Ville { get; set; }
        [Required]
        public string Status { get; set; }
    }
}