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

        public DbSet<Pet> Pets { get; set; }

        public DbSet<PetType> PetTypes { get; set; }

        public DbSet<Owner> Owners { get; set; }

    }
}
