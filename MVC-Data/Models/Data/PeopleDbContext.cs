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

        public DbSet<Person> People { get; set; }

        //public DbSet<MVC_Data.Models.Data.City> City { get; set; }
    }
}
