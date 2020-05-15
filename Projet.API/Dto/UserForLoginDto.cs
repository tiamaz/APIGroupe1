using System.ComponentModel.DataAnnotations;

namespace Projet.API.Dto
{
    public class UserForLoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string password { get; set; }
    }
}