using Petshop2020.Core.Domain_Service;
using Petshop2020.Core.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Petshop2020.Infrastructure.Data
{
    public class DataInitializer
    {
        readonly IPetRepository _petRepo;
        readonly IOwnerRepository _ownerRepo;
        readonly IPetTypeRepository _typeRepo;

        public DataInitializer(IPetRepository petRepository, 
            IOwnerRepository ownerRepository,
            IPetTypeRepository typeRepository)
        {
            _petRepo = petRepository;
            _ownerRepo = ownerRepository;
            _typeRepo = typeRepository;
        }

        public void InitData()
        {
            var pet1 = new Pet()
            {
                Name = "Billy",
                Type = new PetType() {Id = 1},
                BirthDate = DateTime.Now.AddYears(-2),
                Color = "White",
                PreviousOwner = "Joe",
                Price = 500,
                SoldDate = DateTime.Now.AddYears(-2),
                Owner = new Owner() {Id = 1}
            };
            _petRepo.CreatePet(pet1);

            var pet2 = new Pet()
            { 
                Name = "Bob",
                Type =  new PetType() {Id = 2},
                BirthDate = DateTime.Now.AddYears(-3),
                Color = "Black",
                PreviousOwner = "Michael",
                Price = 100,
                SoldDate = DateTime.Now.AddYears(-1),
                Owner = new Owner() {Id = 2}
            };
            _petRepo.CreatePet(pet2);

            
            var pet3 = new Pet()
            {
                Name = "Joe",
                Type = new PetType() {Id = 3},
                BirthDate = DateTime.Now.AddYears(-10),
                Color = "Orange",
                PreviousOwner = "Tommy",
                Price = 1500,
                SoldDate = DateTime.Now.AddYears(-2),
                Owner = new Owner() { Id = 2}
            };
            _petRepo.CreatePet(pet3);


            var pet4 = new Pet()
            {
                Name = "Timmy",
                Type = new PetType() {Id = 1},
                BirthDate = DateTime.Now.AddYears(-2),
                Color = "Brown",
                PreviousOwner = "Dimitri",
                Price = 1250,
                SoldDate = DateTime.Now.AddYears(-1),
                Owner = new Owner() { Id = 2}
            };
            _petRepo.CreatePet(pet4);


            var pet5 = new Pet()
            {
                Name = "Splinter",
                Type = new PetType() {Id = 2},
                BirthDate = DateTime.Now.AddYears(-3),
                Color = "Grey",
                PreviousOwner = "Michael",
                Price = 50,
                SoldDate = DateTime.Now.AddYears(-1),
                Owner = new Owner() { Id = 1}           
            };
            _petRepo.CreatePet(pet5);

            var pet6 = new Pet()
            {
                Name = "Mujo",
                Type = new PetType() {Id = 3},
                BirthDate = DateTime.Now.AddYears(-2),
                Color = "Yellow",
                PreviousOwner = "Betty",
                Price = 145,
                SoldDate = DateTime.Now.AddYears(-1),
                Owner = new Owner() { Id = 2}
            };
            _petRepo.CreatePet(pet6);
            

            var owner1 = new Owner()
            {
                FirstName = "Bob",
                LastName = "Bobsen",
                Address = "Storegade 1",
                PhoneNumber = 12345678,
            };
            _ownerRepo.CreateOwner(owner1);

            var owner2 = new Owner()
            {
                FirstName = "Joe",
                LastName = "Cool",
                Address = "Langevej 1",
                PhoneNumber = 22345678
            };
            _ownerRepo.CreateOwner(owner2);

            var type1 = new PetType()
            {
                Type = "Dog"
            };
            _typeRepo.CreatePetType(type1);

            var type2 = new PetType()
            {
                Type = "Cat"
            };
            _typeRepo.CreatePetType(type2);

            var type3 = new PetType()
            {
                Type = "Bird"
            };
            _typeRepo.CreatePetType(type3);

        }
    }
}
