using Microsoft.EntityFrameworkCore;
using SimpleCRUD.DataAccess.Entities;

namespace SimpleCRUD.DataAccess
{
    public class AppDBContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.GenerateSeed();
        }
    }
}
