using CalificacionesWEBApp.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CalificacionesWEBApp.Data
{
    public class DatosDbContext : DbContext
    {
        public DatosDbContext(DbContextOptions<DatosDbContext> options)
            : base(options)
        {
        }

        public DbSet<CursoModel> Cursos { get; set; }
        public DbSet<ProfesorModel> Profesores { get; set; }
        public DbSet<MateriaModel> Materias { get; set; }
        public DbSet<EstudianteModel> Estudiantes { get; set; }
        public DbSet<CalificacionModel> Calificaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CalificacionModel>()
                .HasOne(c => c.Materia)
                .WithMany(m => m.Calificaciones)
                .HasForeignKey(c => c.MateriaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CalificacionModel>()
                .HasOne(c => c.Estudiante)
                .WithMany(e => e.Calificaciones)
                .HasForeignKey(c => c.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MateriaModel>()
                .HasOne(m => m.Profesor)
                .WithMany(p => p.Materias)
                .HasForeignKey(m => m.ProfesorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MateriaModel>()
                .HasOne(m => m.Curso)
                .WithMany(c => c.Materias)
                .HasForeignKey(m => m.CursoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EstudianteModel>()
                .HasOne(e => e.Curso)
                .WithMany(c => c.Estudiantes)
                .HasForeignKey(e => e.CursoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
