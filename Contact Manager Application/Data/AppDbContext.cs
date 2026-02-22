using Contact_Manager_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Contact_Manager_Application.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(c => c.Salary)
                .HasColumnType("decimal(18,4)"); 
        }
    }
}
