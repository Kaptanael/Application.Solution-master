using Application.Model;
using Microsoft.EntityFrameworkCore;

namespace Application.Data.DataContext
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<UserTask> Tasks { get; set; }

        public DbSet<Value> Values { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(local);Initial Catalog=ApplicationDb;Integrated Security=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
