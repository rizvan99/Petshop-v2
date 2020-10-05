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
    public class PetRepository : IPetRepository
    {
        private PetshopContext _ctx;

        public PetRepository(PetshopContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Pet> AllPetsFromList()
        {
            return _ctx.Pets;
        }

        public Pet CreatePet(Pet pet)
        {
            _ctx.Attach(pet).State = EntityState.Added;
            _ctx.SaveChanges();
            return pet;

            /*var newPet = _ctx.Add(pet);
            _ctx.SaveChanges();
            return newPet.Entity;*/
        }

        public Pet UpdatePet(Pet petToUpdate)
        {
            var updated = _ctx.Update(petToUpdate).Entity;
            _ctx.SaveChanges();
            return updated;
        }

        public Pet DeletePet(int id)
        {
            throw new NotImplementedException();
        }

        public FilteredList<Pet> ReadAllPets(FilterSearch filter)
        {
            var filteredList = new FilteredList<Pet>();

            var total = filteredList.TotalCount = _ctx.Pets.Count();
            filteredList.FilterUsed = filter;

            IEnumerable<Pet> filtering = _ctx.Pets;

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

        public Pet ReadById(int id)
        {
            return _ctx.Pets.FirstOrDefault(p => p.Id == id);
        }

        public Pet ReadByIdIncludeOwnersAndTypes(int id)
        {
            return _ctx.Pets
                .AsNoTracking()
                .Include(p => p.Owner)
                .Include(p => p.Type)
                .FirstOrDefault(p => p.Id == id);
        }

    }
}
