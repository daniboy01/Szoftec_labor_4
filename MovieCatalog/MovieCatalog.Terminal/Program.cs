using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieCatalog.Data;
using System.Threading.Tasks;

namespace MovieCatalog.Terminal
{
    public class Program
    {
        public static async Task Main(string[] args) =>
            await CreateHostBuilder(args).RunConsoleAsync();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                    services.AddDbContext<MovieCatalogDbContext>(o => o.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MovieCatalog"))
                            .AddHostedService<TestConsole>())
                .ConfigureLogging(l => l.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning));
    }
}
