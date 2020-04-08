using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
        {            
        }

        public DbSet<Evento> Eventos { get; set; }        
        public DbSet<Palestrante> Palestrantes { get; set; }        
        public DbSet<Lote> Lotes { get; set; }        
        public DbSet<RedeSocial> RedeSociais { get; set; }        
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(x => new { x.EventoId, x.PalestranteId});
        }
    }
}