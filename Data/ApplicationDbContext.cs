using ContactList.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContactList.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Friend" },
                new Category { CategoryId = 2, Name = "Work" },
                new Category { CategoryId = 3, Name = "Family" }
            );

            // Optional seeded contact — remove if you don't want it
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactId = 1,
                    FirstName = "Sam",
                    LastName = "Example",
                    Phone = "555-1234",
                    Email = "sam@example.com",
                    Organization = "Acme",
                    CategoryId = 2,
                    DateAdded = DateTime.UtcNow
                }
            );
        }
    }
}
