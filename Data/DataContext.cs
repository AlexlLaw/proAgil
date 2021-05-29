using Microsoft.EntityFrameworkCore;
using ProAgil.API3.model;

namespace ProAgil.API3.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<Evento> Eventos { get; set;}
        
    }
}