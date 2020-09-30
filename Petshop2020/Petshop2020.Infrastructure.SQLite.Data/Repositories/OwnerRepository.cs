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
    public class OwnerRepository : IOwnerRepository
    {
        private PetshopContext _ctx;

        public OwnerRepository(PetshopContext ctx)
        {
            _ctx = ctx;
        }

        public Owner CloneById(int id)
        {
            return _ctx.Owners.Select(o => new Owner()
            {
                Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Address = o.Address,
                PhoneNumber = o.PhoneNumber
            })
                .FirstOrDefault(o => o.Id == id);
        }

        public Owner CreateOwner(Owner owner)
        {
            var newOwner = _ctx.Add(owner);
            _ctx.SaveChanges();
            return newOwner.Entity;
        }

        public Owner DeleteOwner(int id)
        {
            var ownerDeleted = _ctx.Owners.FirstOrDefault(o => o.Id == id);
            if (ownerDeleted != null)
            {
                _ctx.Remove(ownerDeleted);
                _ctx.SaveChanges();
            }
            
            return ownerDeleted;
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _ctx.Owners;
        }

        public FilteredList<Owner> ReadAllOwnersSearch(FilterSearch filter)
        {
            var filteredList = new FilteredList<Owner>();

            filteredList.TotalCount = _ctx.Owners.Count();
            filteredList.FilterUsed = filter;

            IEnumerable<Owner> filtering = _ctx.Owners;

            if (!string.IsNullOrEmpty(filter.SearchText))
            {
                switch (filter.SearchField)
                {
                    case "FirstName":
                        filtering = filtering.Where(o => o.FirstName.ToLower().Contains(filter.SearchText.ToLower()));
                        break;
                    case "LastName":
                        filtering = filtering.Where(o => o.LastName.ToLower().Contains(filter.SearchText.ToLower()));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(filter.OrderDirection) && !string.IsNullOrEmpty(filter.OrderProperty))
            {
                var prop = typeof(Owner).GetProperty(filter.OrderProperty);
                filtering = "ASC".Equals(filter.OrderDirection) ?
                    filtering.OrderBy(o => prop.GetValue(o, null)) :
                    filtering.OrderByDescending(o => prop.GetValue(o, null));
            }

            filteredList.List = filtering.ToList();
            filteredList.TotalFound = filtering.Count();
            return filteredList;
        }

        public Owner ReadById(int id)
        {
            return _ctx.Owners
                .AsNoTracking() 
                .FirstOrDefault(o => o.Id == id);
        }

        public Owner ReadByIdIncludePets(int id)
        {
            return _ctx.Owners
                .AsNoTracking()
                .Include(o => o.Pets)
                .FirstOrDefault(o => o.Id == id);
        }

        public Owner UpdateOwner(Owner ownerToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
