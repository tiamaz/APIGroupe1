using System.Threading.Tasks;

namespace Projet.API.Data
{
    public class GestionRepository : IGestionRepository
    {
        private readonly ApplicationDbContext _db;

        public GestionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Ajouter<T>(T entity) where T : class
        {
            _db.Add(entity);
        }

        public async Task<bool> sauvegarderChangement()
        {
                return (await _db.SaveChangesAsync() > 0);
        }

        public void Supprimer<T>(T entity) where T : class
        {
                _db.Remove(entity);
        }
    }
}