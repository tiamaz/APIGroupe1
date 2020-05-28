using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Projet.API.Data;
using Projet.API.Model;

namespace Projet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClasseController: ControllerBase
    {
        private readonly IGestionRepository _repo;

        public ClasseController(IGestionRepository repo)
        {
            _repo = repo;
        }
        /// Respository pattern

        [HttpPost("Ajouter")]
        public async Task<IActionResult> AjouterClasse(Classe classe) /// IL faut créer un DTO 
        {
                    _repo.Ajouter(classe);

                    if(await _repo.sauvegarderChangement()) return Ok("La classe a été ajouté");
                        return BadRequest("Un problème est survenu");
        }


    }
}