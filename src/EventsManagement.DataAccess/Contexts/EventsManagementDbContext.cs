using EventsManagement.DataObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace EventsManagement.DataAccess.Contexts
{
    internal class EventsManagementDbContext : DbContext
    {
        public EventsManagementDbContext(DbContextOptions<EventsManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<EventUser> EventUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .ToTable("Events", "dbo")
                .Property(e => e.DateAndTime)
                .HasColumnName("DateTime");

            modelBuilder.Entity<User>()
                .ToTable("Users", "dbo");

            modelBuilder.Entity<EventUser>()
                .ToTable("EventUsers", "dbo")
                .Property(eu => eu.RegistrationDate)
                .HasColumnName("Registration");

            base.OnModelCreating(modelBuilder);
        }
    }
}
