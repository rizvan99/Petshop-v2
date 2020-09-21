using Petshop2020.Core.Domain_Service;
using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Petshop2020.Core.Application_Service.Service
{
    public class PetService: IPetService
    {
        readonly IPetRepository _petRepo;
        readonly IOwnerRepository _ownerRepo;

        public PetService(IPetRepository petRepository, IOwnerRepository ownerRepository)
        {
            _petRepo = petRepository;
            _ownerRepo = ownerRepository;
        }

        public Pet CreatePet(Pet pet)
        {
            if (pet.Owner == null || pet.Owner.Id <= 0)
            {
                throw new InvalidDataException("Please specify an owner to the pet");
            }

            if (_ownerRepo.ReadById(pet.Owner.Id) == null)
            {
                throw new InvalidDataException("Owner was not found");
            }

            return _petRepo.CreatePet(pet);
        }

        public Pet DeletePet(int id)
        {
            return _petRepo.DeletePet(id);
        }

        public Pet FindPetById(int id)
        {
            return _petRepo.ReadById(id);
        }
        


        public Pet NewPet(Pet pet)
        {
            var newPet = new Pet()
            {
                Name = pet.Name,
                BirthDate = pet.BirthDate,
                Color = pet.Color,
                Price = pet.Price,
                PreviousOwner = pet.PreviousOwner,
                SoldDate = pet.SoldDate,
                Type = pet.Type
            };
            return newPet;
            
        }

        public Pet UpdatePet(Pet petToUpdate)
        {   
            var pet = FindPetById(petToUpdate.Id);
            pet.Name = petToUpdate.Name;
            pet.BirthDate = petToUpdate.BirthDate;
            pet.Color = petToUpdate.Color;
            pet.Price = petToUpdate.Price;
            pet.PreviousOwner = petToUpdate.PreviousOwner;
            pet.SoldDate = petToUpdate.SoldDate;
            pet.Type = petToUpdate.Type;
            return pet;
            
        }

        public FilteredList<Pet> GetAllPets(FilterSearch filter)
        {
                if (!string.IsNullOrEmpty(filter.SearchText) && string.IsNullOrEmpty(filter.SearchField))
                {
                    filter.SearchField = "Name";
                }

                return _petRepo.ReadAllPets(filter);
        }

        /**
         * Used for old UI
         **/
        public List<Pet> SearchForType(string type)
        {
            /*var allPets = GetAllPets();
            var query = allPets.Where(searchPet => searchPet.Type.ToLower().Equals(type));
            return query.ToList();*/
            return null;
        }

        /**
         * Used for old UI
         **/
        public List<Pet> SortPetsByPrice()
        {
            /*var allPets = GetAllPets();
            var query = allPets.OrderBy(pet => pet.Price);
            return query.ToList();*/
            return null;
        }

        /**
         * Used for old UI
         **/
        public List<Pet> GetFiveCheapestPets()
        {
            /*var allPetsSorted = SortPetsByPrice();
            var query = allPetsSorted.Take(5);
            return query.ToList();*/
            return null;
        }
    }
}
