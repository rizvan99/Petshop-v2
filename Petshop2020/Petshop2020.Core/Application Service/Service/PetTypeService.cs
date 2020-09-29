using Petshop2020.Core.Domain_Service;
using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop2020.Core.Application_Service.Service
{
    public class PetTypeService : IPetTypeService
    {

        readonly IPetTypeRepository _typeRepo;
        readonly IPetRepository _petRepo;

        public PetTypeService(IPetTypeRepository typeRepository, IPetRepository petRepository)
        {
            _typeRepo = typeRepository;
            _petRepo = petRepository;
        }


        public PetType CreateType(PetType petType)
        {
            return _typeRepo.CreatePetType(petType);
        }

        public PetType DeleteType(int id)
        {
            return _typeRepo.DeletePetType(id);
        }

        public PetType FindTypeById(int id)
        {
            return _typeRepo.ReadTypeById(id);
        }

        public PetType FindTypeByIdIncludePets(int id)
        {
            return _typeRepo.ReadTypeByIdIncludePets(id);
        }

        public FilteredList<PetType> GetAllTypes(FilterSearch filter)
        {
            if (!string.IsNullOrEmpty(filter.SearchText) && string.IsNullOrEmpty(filter.SearchField))
            {
                filter.SearchField = "Type";
            }

            return _typeRepo.GetAllTypesSearch(filter);
        }

        public PetType NewType(PetType petType)
        {
            var newType = new PetType()
            {
                Type = petType.Type
            };
            return newType;
        }

        public PetType UpdateType(PetType typeToUpdate)
        {
            var type = FindTypeById(typeToUpdate.Id);
            type.Type = typeToUpdate.Type;
            return type;
        }
    }
}
