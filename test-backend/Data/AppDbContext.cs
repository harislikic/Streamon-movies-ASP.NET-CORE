using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using haris_edin_rs1.ModelsAutentififkacija;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

using test_backend.Models;

namespace test_backend.Data
{
    public class AppDbContext : DbContext
    {


        public AppDbContext(
          DbContextOptions options) : base(options)
        {

        }

        public DbSet<Movies> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<AutentifikacijaToken> AutentifikacijaToken { get; set; }
        public DbSet<Comments> Comment { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

    }
}
