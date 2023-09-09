using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Squeezer.Models;

namespace Squeezer.Infrastructure
{
    public class SqueezerDbContext : DbContext
    {
        public SqueezerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }
    }
}
