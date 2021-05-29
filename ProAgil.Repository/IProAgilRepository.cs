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

        Task<Evento[]> getAllEventoAsyncByTema(string tema, bool includesPalestrantes);
        Task<Evento[]> getAllEventoAsync(bool includesPalestrantes);
        Task<Evento> getAllEventoAsyncById(int EventoId, bool includesPalestrantes);
        Task<Evento[]> getAllPalestranteAsyncByName(string tema, bool includesPalestrantes);
        Task<Evento[]> getAllPalestranteAsync(int PalestranteId, bool includesPalestrantes);



    }
}