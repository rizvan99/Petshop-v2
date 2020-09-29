using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Application_Service
{
    public interface IPetTypeService
    {
        //CREATE
        public PetType NewType(PetType petType);
        public PetType CreateType(PetType petType);


        //READ
        public FilteredList<PetType> GetAllTypes(FilterSearch filter);
        public PetType FindTypeById(int id);
        public PetType FindTypeByIdIncludePets(int id);

        //UPDATE
        public PetType UpdateType(PetType typeToUpdate);


        //DELETE
        public PetType DeleteType(int id);


    }
}
