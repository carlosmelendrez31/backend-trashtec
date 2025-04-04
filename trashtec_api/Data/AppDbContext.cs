using Microsoft.EntityFrameworkCore;
using trashtec_api.Models;

namespace trashtec_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UsuariosModel> Usuarios { get; set; }
        public DbSet<DispositivoModel> Dispositivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuariosModel>()
                .HasOne<DispositivoModel>()
                .WithMany()
                .HasForeignKey(u => u.idDispositivo)
                .OnDelete(DeleteBehavior.SetNull); // 🔹 FK opcional

          
        }
    }
}

