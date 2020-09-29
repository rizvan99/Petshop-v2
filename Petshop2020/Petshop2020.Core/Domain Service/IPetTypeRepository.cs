using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Domain_Service
{
    public interface IPetTypeRepository
    {
        // C - CREATE
        public PetType CreatePetType(PetType petType);


        // R - READ
        public IEnumerable<PetType> GetAllTypes();
        public FilteredList<PetType> GetAllTypesSearch(FilterSearch filter);
        public PetType ReadTypeById(int id);
        public PetType CloneById(int id);
        public PetType ReadTypeByIdIncludePets(int id);

        // U - UPDATE
        public PetType UpdatePetType(PetType typeToUpdate);


        // D - DELETE
        public PetType DeletePetType(int id);

    }
}
