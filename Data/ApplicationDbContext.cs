using ColegioSanJose.Models;
using Microsoft.EntityFrameworkCore;

namespace ColegioSanJose.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Alumno> Alumnos => Set<Alumno>();
        public DbSet<Materia> Materias => Set<Materia>();
        public DbSet<Expediente> Expedientes => Set<Expediente>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Evitar duplicados: misma Materia repetida para el mismo Alumno
            modelBuilder.Entity<Expediente>()
                .HasIndex(e => new { e.AlumnoId, e.MateriaId })
                .IsUnique();

            // Relaciones
            modelBuilder.Entity<Expediente>()
                .HasOne(e => e.Alumno)
                .WithMany(a => a.Expedientes)
                .HasForeignKey(e => e.AlumnoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Expediente>()
                .HasOne(e => e.Materia)
                .WithMany(m => m.Expedientes)
                .HasForeignKey(e => e.MateriaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
