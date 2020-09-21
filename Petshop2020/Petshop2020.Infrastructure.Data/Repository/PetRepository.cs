using Petshop2020.Core.Domain_Service;
using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop2020.Infrastructure.Data
{
    public class PetRepository : IPetRepository
    {

        private static List<Pet> _pets = new List<Pet>();
        private static int id = 1;
    
        public PetRepository()
        {
            
        }

        public Pet CreatePet(Pet pet)
        {
            pet.Id = id++;
            _pets.Add(pet);
            return pet;
        }

        public Pet DeletePet(int id)
        {
            var petToDelete = this.ReadById(id);
            if (petToDelete != null)
            {
                _pets.Remove(petToDelete);
                return petToDelete;
            }
            return null;
        }


        public Pet ReadById(int id)
        {
            foreach (var pet in _pets)
            {   
                if (pet.Id == id)
                {
                    return pet;
                }
            }
            return null;
        }

        public Pet UpdatePet(Pet petToUpdate)
        {
            var petFromDB = this.ReadById(petToUpdate.Id);
            if (petFromDB != null)
            {
                petFromDB.Name = petToUpdate.Name;
                petFromDB.Type = petToUpdate.Type;
                petFromDB.BirthDate = petToUpdate.BirthDate;
                petFromDB.SoldDate = petToUpdate.SoldDate;
                petFromDB.Color = petToUpdate.Color;
                petFromDB.PreviousOwner = petToUpdate.PreviousOwner;
                petFromDB.Price = petToUpdate.Price;
                return petFromDB;
            }
            return null;
        }

        public IEnumerable<Pet> AllPetsFromList()
        {
            return _pets;
        }

        public FilteredList<Pet> ReadAllPets(FilterSearch filter)
        {
            var filteredList = new FilteredList<Pet>();

            var total = filteredList.TotalCount = _pets.Count();
            filteredList.FilterUsed = filter;

            IEnumerable<Pet> filtering = _pets;

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                switch (filter.SearchField)
                {
                    case "Name":
                        filtering = filtering.Where(o => o.Name.ToLower().Contains(filter.SearchText.ToLower()));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(filter.OrderDirection) && !string.IsNullOrEmpty(filter.OrderProperty))
            {
                var prop = typeof(Pet).GetProperty(filter.OrderProperty);
                filtering = "ASC".Equals(filter.OrderDirection) ?
                    filtering.OrderBy(o => prop.GetValue(o, null)) :
                    filtering.OrderByDescending(o => prop.GetValue(o, null));
            }

            filteredList.List = filtering.ToList();
            filteredList.TotalFound = filtering.Count();
            return filteredList;

        }

        public List<Pet> Filter(string orderDir)
        {
            if ("asc".Equals(orderDir))
            {
                return _pets.OrderBy(o => o.Name).ToList();
            }

            return _pets.OrderByDescending(o => o.Name).ToList();
        }


    }
}
