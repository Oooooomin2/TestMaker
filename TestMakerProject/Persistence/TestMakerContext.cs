using TestMakerProject.Models;
using Microsoft.EntityFrameworkCore;

namespace TestMakerProject.Persistence
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
