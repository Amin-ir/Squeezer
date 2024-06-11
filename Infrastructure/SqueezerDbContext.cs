using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squeezer.Models;
using Squeezer.Services.Directors;

namespace Squeezer.Infrastructure
{
    public class SqueezerDbContext : DbContext
    {
        private readonly IUserDirector _userDirector;
        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }
        public SqueezerDbContext(DbContextOptions options, IUserDirector userDirector) : base(options)
        {
            _userDirector = userDirector;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(user => new { user.Email }).IsUnique();

            var adminEntity = _userDirector.CreateAdminUser();

            adminEntity.Id = -1;

            modelBuilder.Entity<User>()
                        .HasData(adminEntity);
        }

    }
}
