using System.Threading.Tasks;
using ProAgil.Business.Models;

namespace ProAgil.Business.Interfaces
{
    public interface IPalestranteRepository : IProAgilRepository<Palestrante>
    {
         //PALESTRANTE
         Task<Palestrante[]> GetAllPalestrantesByNameAsync(string name, bool includeEventos);
         Task<Palestrante> GetPalestranteAsync(int palestranteId, bool includeEventos);
    }
}