using Petshop2020.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Infrastructure.SQLite.Data
{
    public class DBInitializer
    {
        public static void SeedDB(PetshopContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            var owner1 = new Owner()
            {
                FirstName = "Bob",
                LastName = "Bobsen",
                Address = "Storegade 1",
                PhoneNumber = 12345678,
            };
            ctx.Owners.Add(owner1);

            var owner2 = new Owner()
            {
                FirstName = "Joe",
                LastName = "Cool",
                Address = "Langevej 1",
                PhoneNumber = 22345678
            };
            ctx.Owners.Add(owner2);

            var owner3 = new Owner()
            {
                FirstName = "John",
                LastName = "Wayne",
                Address = "Richman 1",
                PhoneNumber = 22345678
            };
            ctx.Owners.Add(owner3);

            var type1 = new PetType()
            {
                Type = "Dog"
            };
            ctx.PetTypes.Add(type1);

            var type2 = new PetType()
            {
                Type = "Cat"
            };
            ctx.PetTypes.Add(type2);

            var type3 = new PetType()
            {
                Type = "Bird"
            };
            ctx.PetTypes.Add(type3);


            var pet1 = new Pet()
            {
                Name = "Billy",
                Type = type1,
                BirthDate = DateTime.Now.AddYears(-2),
                Color = "White",
                PreviousOwner = "Joe",
                Price = 500,
                SoldDate = DateTime.Now.AddYears(-2),
                Owner = owner1
            };
            ctx.Pets.Add(pet1);



            var pet2 = new Pet()
            {
                Name = "Bob",
                Type = type2,
                BirthDate = DateTime.Now.AddYears(-3),
                Color = "Black",
                PreviousOwner = "Michael",
                Price = 100,
                SoldDate = DateTime.Now.AddYears(-1),
                Owner = owner1
            };
            ctx.Pets.Add(pet2);


            var pet3 = new Pet()
            {
                Name = "Joe",
                Type = type3,
                BirthDate = DateTime.Now.AddYears(-10),
                Color = "Orange",
                PreviousOwner = "Tommy",
                Price = 1500,
                SoldDate = DateTime.Now.AddYears(-2),
                Owner = owner1
            };
            ctx.Pets.Add(pet3);


            var pet4 = new Pet()
            {
                Name = "Timmy",
                Type = type1,
                BirthDate = DateTime.Now.AddYears(-2),
                Color = "Brown",
                PreviousOwner = "Dimitri",
                Price = 1250,
                SoldDate = DateTime.Now.AddYears(-1),
                Owner = owner2
            };
            ctx.Pets.Add(pet4);


            var pet5 = new Pet()
            {
                Name = "Splinter",
                Type = type2,
                BirthDate = DateTime.Now.AddYears(-3),
                Color = "Grey",
                PreviousOwner = "Michael",
                Price = 50,
                SoldDate = DateTime.Now.AddYears(-1),
                Owner = owner2
            };
            ctx.Pets.Add(pet5);

            var pet6 = new Pet()
            {
                Name = "Mujo",
                Type = type3,
                BirthDate = DateTime.Now.AddYears(-2),
                Color = "Yellow",
                PreviousOwner = "Betty",
                Price = 145,
                SoldDate = DateTime.Now.AddYears(-1),
                Owner = owner2
            };
            ctx.Pets.Add(pet6);


            ctx.SaveChanges();
        }
    }
}
