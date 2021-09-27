using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieCatalog.Data.Entities;
using MovieCatalog.Data.Entitites;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCatalog.Data
{
    public class MovieCatalogDbContext : DbContext
    {
        public MovieCatalogDbContext(ILogger<MovieCatalogDbContext> logger, DbContextOptions<MovieCatalogDbContext> options) : base(options)
        {
            Logger = logger;
        }

        private ILogger<MovieCatalogDbContext> Logger { get; }

        public DbSet<Title> Titles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Title>(title =>
            {
                title.Property(t => t.Id).ValueGeneratedNever();
                title.Property(t => t.PrimaryTitle)
                    .HasMaxLength(500)
                    .IsRequired();
                title.HasIndex(t => t.PrimaryTitle);

                title.HasIndex(t => t.TitleType);
            });
        }

        public async Task ImportFromFileAsync(string filePath, int? maxValues = 100_000)
        {
            var toInsert = TsvParser.ParseTsv(filePath).Select(item => new Title
            {
                Id = int.Parse(item["tconst"][2..]), // A 'tconst' értéke a fájlban pl. 'tt6723592', a [..] range operátorral a 'tt'-t az elejéről levágjuk, a maradékot pedig int-té alakítjuk
                PrimaryTitle = item["primaryTitle"], // 'Tenet',
                OriginalTitle = item["originalTitle"],
                //TitleType = Enum.Parse(typeof(TitleType), item["titleType"]),
                EndYear = validateNumbers(item["endYear"]),
                //StartYear = validateNumbers(item["startYear"]),
                RunTimeMinutes = validateNumbers(item["runtimeMinutes"])
            });
            if (maxValues != null)
                toInsert = toInsert.Take(maxValues.Value); // Alapértelmezetten csak 100 000 elemet veszünk a fájlból összesen.

            while (toInsert.Any()) // Ha van még feldolgozatlan sor,
            {
                Titles.AddRange(toInsert.Take(100_000)); // 100 000-et felcsatolunk a Titles DbSet-be,
                toInsert = toInsert.Skip(100_000); // léptetünk egy 'oldalt',
                var saved = await SaveChangesAsync(); // elmentjük a DB-be, ami visszaadja a mentett sorok számát,
                Logger.LogInformation($"Saved {saved} rows."); // végül kiírjuk a mentett sorok számát.
            }
        }

        private int validateNumbers(string number)
        {
            if (number == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(number);
            }
        }
    }
}

//TitleType = (TitleType)Enum.Parse(typeof(TitleType), item["titleType"].ToUpper()),
  //              OriginalTitle = item["originalTitle"],
    //            StartYear = int.Parse(item["startYear"]),
      //          ,
        //        R