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
            // 🔐 Garante que não exista duplicidade de consumo real-time ou no lote
            modelBuilder.Entity<ConsumoEntity>()
                .HasIndex(c => new { c.PessoaId, c.DataConsumo, c.ValorTotal })
                .IsUnique();

            // 🔑 PessoaId como chave primária (1 para 1)
            modelBuilder.Entity<PontosEntity>().HasKey(p => p.PessoaId);

            // 🔗 Relação Memorial -> Consumo
            modelBuilder.Entity<MemorialEntity>()
                .HasOne(m => m.ConsumoEntity)
                .WithMany(c => c.RegistrosMemorial)
                .HasForeignKey(m => m.ConsumoId);
        }
    }
}