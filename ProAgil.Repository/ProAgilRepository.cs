using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Repostory;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;
        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //GERAL
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Upadate<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        
        //EVENTO
        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lote)
                .Include(e => e.RedeSociais);

            if (includePalestrante)
            {
                query = query.Include(p => p.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoByTemaAsync(string tema, bool includePalestrante)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lote)
                .Include(c => c.RedeSociais);

            if (includePalestrante)
            {
                query = query.Include(p => p.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento)
                .Where(c => c.Tema.Contains(tema));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrante)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lote)
                .Include(c => c.RedeSociais);

            if (includePalestrante)
            {
                query = query.Include(p => p.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query = query.OrderByDescending(c => c.DataEvento)
                .Where(c => c.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }  

        //PELESTRANTE
        public async Task<Palestrante> GetAllPalestrantesAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedeSociais);

            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Nome)
                .Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNameAsync(string nome, bool includeEventos)
        {
             IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedeSociais);

            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
