using Microsoft.EntityFrameworkCore;
using Petshop2020.Core.Domain_Service;
using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop2020.Infrastructure.SQLite.Data.Repositories
{
    public class PetTypeRepository : IPetTypeRepository
    {

        private PetshopContext _ctx;

        public PetTypeRepository(PetshopContext ctx)
        {
            _ctx = ctx;
        }

        public PetType CloneById(int id)
        {
            return _ctx.PetTypes.Select(type => new PetType()
            {
                Id = type.Id,
                Type = type.Type
            }).
            FirstOrDefault(type => type.Id == id);
        }

        public PetType CreatePetType(PetType petType)
        {
            var newType = _ctx.Add(petType);
            _ctx.SaveChanges();
            return newType.Entity;
        }

        public PetType DeletePetType(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PetType> GetAllTypes()
        {
            return _ctx.PetTypes;
        }

        public FilteredList<PetType> GetAllTypesSearch(FilterSearch filter)
        {
            var filteredList = new FilteredList<PetType>();

            filteredList.TotalCount = _ctx.PetTypes.Count();
            filteredList.FilterUsed = filter;

            IEnumerable<PetType> filtering = _ctx.PetTypes;

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

        public PetType ReadTypeById(int id)
        {
            return _ctx.PetTypes.FirstOrDefault(t => t.Id == id);
        }

        public PetType ReadTypeByIdIncludePets(int id)
        {
            return _ctx.PetTypes
                .AsNoTracking()
                .Include(t => t.Pets)
                .FirstOrDefault(t => t.Id == id);
        }

        public PetType UpdatePetType(PetType typeToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
