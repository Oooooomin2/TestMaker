using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDD.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DDD.Domain.Data
{
    public class TestMakerContext : DbContext
    {
        public TestMakerContext (DbContextOptions<TestMakerContext> options)
            : base(options)
        {
        }

        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
