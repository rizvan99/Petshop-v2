using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Domain_Service
{
    public interface IPetRepository
    {
        // C - CREATE
        public Pet CreatePet(Pet pet);


        // R - READ
        public FilteredList<Pet> ReadAllPets(FilterSearch filter);
        public IEnumerable<Pet> AllPetsFromList();
        public Pet ReadById(int id);


        // U - UPDATE
        public Pet UpdatePet(Pet petToUpdate);


        //D - DELETE
        public Pet DeletePet(int id);


    }
}
