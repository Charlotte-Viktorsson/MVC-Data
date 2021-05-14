using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_Data.Models.Data;

namespace MVC_Data.Models.Data
{
    public class PeopleDbContext : DbContext
    {

        public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options)
        { }

        //Join table configured using Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonLanguage>().HasKey(pl =>
            new
            {
                pl.PersonId,
                pl.LanguageId
            });

            modelBuilder.Entity<PersonLanguage>()
                .HasOne<Person>(pl => pl.Person)      //pl = PersonLanguage
                .WithMany(p => p.PersonLanguages)     //p = person
                .HasForeignKey(pl => pl.PersonId);

            modelBuilder.Entity<PersonLanguage>()
                .HasOne<Language>(pl => pl.Language)  //pl = PersonLanguage
                .WithMany(l => l.PersonLanguages)     //l = Language
                .HasForeignKey(pl => pl.LanguageId);
        }

        public DbSet<Person> People { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<PersonLanguage> PersonLanguages { get; set; }
    }
}
/* PM> cd ExProjectName
 * PM> dotnet ef migrations add InitialCreate * PM> dotnet ef database update
 */