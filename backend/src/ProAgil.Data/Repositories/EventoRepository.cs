using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Business.Interfaces;
using ProAgil.Business.Models;
using ProAgil.Data.Context;

namespace ProAgil.Data.Repositories
{
    public class EventoRepository : ProAgilRepository<Evento>, IEventoRepository
    {
        public EventoRepository(ProAgilContext context) : base(context)
        {
        }

        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(l => l.Lote)
                .Include(r => r.RedeSociais);

            if (includePalestrante)
            {
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(e => e.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoByTemaAsync(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(l => l.Lote)
                .Include(r => r.RedeSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(e => e.DataEvento)
                .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(l => l.Lote)
                .Include(r => r.RedeSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(e => e.DataEvento)
                .Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}