using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Domain_Service
{
    public interface IPetTypeRepository
    {
        public PetType CreatePetType(PetType petType);

        public IEnumerable<PetType> GetAllTypes();

        public FilteredList<PetType> GetAllTypesSearch(FilterSearch filter);

        public PetType UpdatePetType(PetType typeToUpdate);

        public PetType DeletePetType(int id);

        public PetType ReadTypeById(int id);

        public PetType CloneById(int id);
    }
}
