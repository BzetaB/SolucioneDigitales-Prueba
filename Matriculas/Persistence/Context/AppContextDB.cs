using Matriculas.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Matriculas.Persistence.Context
{
    public class AppContextDB: DbContext
    {
        public AppContextDB(DbContextOptions<AppContextDB> options): base(options) { }
    
        public DbSet<Matricula> matriculas { get; set; }
        public DbSet<Estudiante> estudiantes { get; set; }
        public DbSet<Curso> cursos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Matricula>()
                .Property(m => m.Status)
                .HasConversion<string>();
        }
    }
}
