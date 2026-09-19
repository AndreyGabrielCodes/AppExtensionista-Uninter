using AppExtensionista.Models;
using Microsoft.EntityFrameworkCore;

namespace AppExtensionista.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<AlimentoModel> Alimentos { get; set; }
        public DbSet<GuiaArmazenamentoModel> GuiasArmazenamento { get; set; }
        public DbSet<EstoqueModel> Estoques { get; set; }
        public DbSet<RegistroDescarteModel> RegistrosDescarte { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Desativar deleção em cascata globalmente ou por relacionamento
            modelBuilder.Entity<RegistroDescarteModel>()
                .HasOne(rd => rd.Estoque)
                .WithMany()
                .HasForeignKey(rd => rd.IdEstoque)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}