using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T: class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T: class
        {
             _context.Update(entity);
        }

        public void Delete<T>(T entity) where T: class
        {
             _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync() 
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Evento[]> getAllEventoAsync(bool includesPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

                if (includesPalestrantes) {
                    query = query
                        .Include(p => p.PalestranteEvento)
                        .ThenInclude(p => p.Palestrante);
                }

                query = query.OrderByDescending(c => c.DataEvento);

                return await query.ToArrayAsync();
        }

        public async Task<Evento[]> getAllEventoAsyncByTema(string tema, bool includesPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

                if (includesPalestrantes) {
                    query = query
                        .Include(p => p.PalestranteEvento)
                        .ThenInclude(p => p.Palestrante);
                }

                query = query.OrderByDescending(c => c.DataEvento)
                .Where(c => c.Tema.Contains(tema));

                return await query.ToArrayAsync();
        }

        public async Task<Evento> getEventoAsyncById(int EventoId, bool includesPalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedesSociais);

                if (includesPalestrantes) {
                    query = query
                        .Include(p => p.PalestranteEvento)
                        .ThenInclude(p => p.Palestrante);
                }

                query = query.OrderByDescending(c => c.DataEvento)
                     .Where(c => c.Id == EventoId);

                return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante> getPalestranteAsync(int PalestranteId, bool includesEventos = false)
        {   
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);

                if (includesEventos) {
                    query = query
                        .Include(p => p.PalestranteEventos)
                        .ThenInclude(e => e.Evento);
                }

                query = query.OrderBy(p => p.Nome)
                        .Where(p => p.Id == PalestranteId);

                return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> getAllPalestranteAsyncByName(string name, bool includesEventos = false) 
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedesSociais);

                if (includesEventos) {
                    query = query
                        .Include(p => p.PalestranteEventos)
                        .ThenInclude(e => e.Evento);
                }

                query = query.Where(p => p.Nome.ToLower().Contains(name.ToLower()));
                        

                return await query.ToArrayAsync();
        }
    }
}