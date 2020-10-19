using Microsoft.EntityFrameworkCore;
using Petshop2020.Core.Entity;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Petshop2020.Infrastructure.SQLite.Data
{
    public class PetshopContext : DbContext
    {

        /**
         * base = calling super class constructor
         **/
        public PetshopContext(DbContextOptions<PetshopContext> opt) : base(opt)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>()
                .HasOne(o => o.Owner)
                .WithMany(p => p.Pets)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<PetType> PetTypes { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
