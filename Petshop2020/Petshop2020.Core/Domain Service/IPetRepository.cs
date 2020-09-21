using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Domain_Service
{
    public interface IPetRepository
    {
        public Pet CreatePet(Pet pet);

        public FilteredList<Pet> ReadAllPets(FilterSearch filter);

        public IEnumerable<Pet> AllPetsFromList();

        public Pet UpdatePet(Pet petToUpdate);

        public Pet DeletePet(int id);

        public Pet ReadById(int id);

        


    }
}
