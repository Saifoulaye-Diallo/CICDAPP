using Microsoft.EntityFrameworkCore;

namespace SeleniumApp.Models
{
    public class SeleniumAppDbContext : DbContext
    {
        public SeleniumAppDbContext(DbContextOptions<SeleniumAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Etudiant> Etudiants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Etudiant>(entity =>
            {
                entity.Property(e => e.Nom).IsFixedLength();
                entity.Property(e => e.Prenom).IsFixedLength();
                entity.Property(e => e.Email).IsFixedLength();
                entity.Property(e => e.Sexe).IsFixedLength();
            });
        }
    }
}
