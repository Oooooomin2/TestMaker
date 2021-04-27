using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestMaker.Models;

namespace TestMaker.Data
{
    public class TestMakerContext : DbContext
    {
        public TestMakerContext (DbContextOptions<TestMakerContext> options)
            : base(options)
        {
        }

        public DbSet<Test> Tests { get; set; }
        public DbSet<Choice> Choices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Test>().ToTable("Tests");
            modelBuilder.Entity<Choice>().ToTable("Choices");
        }
    }
}
