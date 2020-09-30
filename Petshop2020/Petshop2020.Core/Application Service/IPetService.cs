using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Application_Service
{
    public interface IPetService
    {
        //C - Create pet
        public Pet NewPet(Pet pet);
        public Pet CreatePet(Pet pet);


        //R - Read all pets
        public FilteredList<Pet> GetAllPets(FilterSearch filter);
        public Pet FindPetById(int id);
        public Pet FindPetByIdIncludeOwnersAndTypes(int id);


        //U - Update pet
        public Pet UpdatePet(Pet petToUpdate);


        //D - Delete pet
        public Pet DeletePet(int id);





        /**
         * Used for old console UI
         **/
        public List<Pet> SortPetsByPrice();

        public List<Pet> SearchForType(string type);

        public List<Pet> GetFiveCheapestPets();
        








    }
}
