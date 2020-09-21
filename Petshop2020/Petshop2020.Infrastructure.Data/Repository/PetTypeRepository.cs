using Petshop2020.Core.Domain_Service;
using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop2020.Infrastructure.Data.Repository
{
    public class PetTypeRepository : IPetTypeRepository
    {

        private static List<PetType> _petTypes = new List<PetType>();
        private static int _id = 1;

        

        public PetType CreatePetType(PetType petType)
        {
            petType.Id = _id++;
            _petTypes.Add(petType);
            return petType;
        }

        public PetType DeletePetType(int id)
        {
            var typeToDelete = this.ReadTypeById(id);
            if (typeToDelete != null)
            {
                _petTypes.Remove(typeToDelete);
                return typeToDelete;
            }
            return null;
        }

        public IEnumerable<PetType> GetAllTypes()
        {
            return _petTypes;
        }

        /**
         * Using this one because the clone method
         * doesn't work with Delete
         **/
        public PetType ReadTypeById(int id)
        {
            foreach (var type in _petTypes)
            {
                if (type.Id == id)
                {
                    return type;
                }
            }
            return null;
        }

        /**
         * Using this one to clone type, when calling
         * GetAllTypesIncludePets()
         **/
        public PetType CloneById(int id)
        {
            return _petTypes.Select(type => new PetType()
            {
                Id = type.Id,
                Type = type.Type
            }).
            FirstOrDefault(type => type.Id == id);
        }

        public PetType UpdatePetType(PetType typeToUpdate)
        {
            var typeFromDB = this.ReadTypeById(typeToUpdate.Id);
            if (typeFromDB != null)
            {
                typeFromDB.Type = typeToUpdate.Type;
                return typeFromDB;
            }
            return null;
        }

        public FilteredList<PetType> GetAllTypesSearch(FilterSearch filter)
        {
            var filteredList = new FilteredList<PetType>();

            filteredList.TotalCount = _petTypes.Count();
            filteredList.FilterUsed = filter;

            IEnumerable<PetType> filtering = _petTypes;

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                switch (filter.SearchField)
                {
                    case "Type":
                        filtering = filtering.Where(o => o.Type.ToLower().Contains(filter.SearchText.ToLower()));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(filter.OrderDirection) && !string.IsNullOrEmpty(filter.OrderProperty))
            {
                var prop = typeof(PetType).GetProperty(filter.OrderProperty);
                filtering = "ASC".Equals(filter.OrderDirection) ?
                    filtering.OrderBy(o => prop.GetValue(o, null)) :
                    filtering.OrderByDescending(o => prop.GetValue(o, null));
            }

            filteredList.List = filtering.ToList();
            filteredList.TotalFound = filtering.Count();
            return filteredList;
        }
    }
}
