using Microsoft.EntityFrameworkCore;
using PontuacaoRealTime.API.Domain.Entities;

namespace PontuacaoRealTime.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ConsumoEntity> Consumos { get; set; }
        public DbSet<MemorialEntity> Memorial { get; set; }
        public DbSet<PontosEntity> Pontos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PontosEntity>().HasKey(p => p.PessoaId);

            modelBuilder.Entity<MemorialEntity>()
                .HasOne(m => m.ConsumoEntity)
                .WithMany(c => c.RegistrosMemorial)
                .HasForeignKey(m => m.ConsumoId);
        }
    }
}