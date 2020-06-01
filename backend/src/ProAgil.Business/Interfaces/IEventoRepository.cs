using System.Threading.Tasks;
using ProAgil.Business.Models;

namespace ProAgil.Business.Interfaces
{
    public interface IEventoRepository : IProAgilRepository<Evento>
    {         
         //EVENTOS
         Task<Evento[]> GetAllEventoByTemaAsync(string tema, bool includePalestrantes);
         Task<Evento[]> GetAllEventoAsync(bool includePalestrante);
         Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes);
    }
}