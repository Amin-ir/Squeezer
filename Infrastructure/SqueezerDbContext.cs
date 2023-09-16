using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squeezer.Models;

namespace Squeezer.Infrastructure
{
    public class SqueezerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }
        public SqueezerDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(user => new { user.Email }).IsUnique();
        }

    }
}
