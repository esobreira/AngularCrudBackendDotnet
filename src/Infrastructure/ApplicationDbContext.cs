using BEComentarios.Domain;
using Microsoft.EntityFrameworkCore;

namespace BEComentarios.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Comentario> Comentarios { get; set; }

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .UseSqlite(_configuration.GetConnectionString("WebApiDatabase"));
        }
    }
}
