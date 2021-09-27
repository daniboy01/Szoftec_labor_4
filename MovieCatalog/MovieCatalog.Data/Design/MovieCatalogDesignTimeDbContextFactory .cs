using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace MovieCatalog.Data.Design
{
    internal class MovieCatalogDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MovieCatalogDbContext>
    {
        public MovieCatalogDbContext CreateDbContext(string[] args) =>
            new(new Logger<MovieCatalogDbContext>(new LoggerFactory()), new DbContextOptionsBuilder<MovieCatalogDbContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MovieCatalog").Options);
    }
}