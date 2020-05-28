using System.Threading.Tasks;

namespace Projet.API.Data
{
    public interface IGestionRepository
    {
         void Ajouter<T> (T entity) where T:class;
         void Supprimer<T> (T entity) where T:class;
         Task<bool> sauvegarderChangement();
    }
}