using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        void Add<T>(T entity) where T: class;
        void Update<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;

        Task<bool> SaveChangesAsync();  
        Task<Evento[]> getAllEventoAsync(bool includesPalestrantes);
        Task<Evento[]> getAllEventoAsyncByTema(string tema, bool includesPalestrantes);
        Task<Evento> getEventoAsyncById(int EventoId, bool includesPalestrantes);
        Task<Palestrante> getPalestranteAsync(int PalestranteId, bool includesEventos);
        Task<Palestrante[]> getAllPalestranteAsyncByName(string name, bool includesEventos);
    }
}