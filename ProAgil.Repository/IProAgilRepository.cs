using System;
using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository : IDisposable
    {
        //GERAL
         void Add<T>(T entity) where T : class;
         void Upadate<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChangesAsync();

         //EVENTOS
         Task<Evento[]> GetAllEventoByTemaAsync(string tema, bool includePalestrante);
         Task<Evento[]> GetAllEventoAsync(bool includePalestrante);
         Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrante);

         //PALESTRANTE
         Task<Palestrante> GetAllPalestrantesAsync(int palestranteId, bool includeEventos);
         Task<Palestrante[]> GetAllPalestrantesByNameAsync(string nome, bool includeEventos);
    }
}