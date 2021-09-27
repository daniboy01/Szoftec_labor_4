using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieCatalog.Data;
using MovieCatalog.Data.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieCatalog.Terminal
{
    public class TestConsole : IHostedService
    {
        public TestConsole(MovieCatalogDbContext dbContext, IHost host, ILogger<TestConsole> logger)
        {
            DbContext = dbContext;
            Host = host;
            Logger = logger;
        }

        private MovieCatalogDbContext DbContext { get; }
        private IHost Host { get; }
        private ILogger<TestConsole> Logger { get; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Started.");

            // TODO: Ide jön az alkalmazás kódja.
            //DbContext.Titles.Add(new Title { Id = 1, PrimaryTitle = "XWSW5U" });
            //DbContext.SaveChanges();

            //var title = DbContext.Titles.Where(t => t.Id == 1).FirstOrDefault();
            //Logger.LogInformation($"New Title saved: {title.Id} - {title.PrimaryTitle}");

            //await DbContext.Database.MigrateAsync(cancellationToken);

            //if (!await DbContext.Titles.AnyAsync(cancellationToken))
            await DbContext.ImportFromFileAsync(@"D:\BME_Laborok_eloadasok\Szoftverfejlesztes_laborok\EF\MovieCatalog\title.basics.tsv.gz", 10000); // Az útvonal értelemszerűen kitöltendő.


            await Host.StopAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Stopping...");
            return Task.CompletedTask;
        }
    }
}
