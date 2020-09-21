using Petshop2020.Core.Domain_Service;
using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop2020.Infrastructure.Data
{
    public class OwnerRepository : IOwnerRepository
    {
        private static List<Owner> _owners = new List<Owner>();
        private static int _id = 1;

        public OwnerRepository()
        {

        }

        

        public Owner CreateOwner(Owner owner)
        {
            owner.Id = _id++;
            _owners.Add(owner);
            return owner;
        }

        public Owner DeleteOwner(int id)
        {
            var ownerToDelete = this.ReadById(id);
            if (ownerToDelete != null)
            {
                _owners.Remove(ownerToDelete);
                return ownerToDelete;
            }
            return null;
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            return _owners;
        }

        /**
         * Using this one because the clone method
         * doesn't work with Delete
         **/
        public Owner ReadById(int id)
        {
            foreach (var owner in _owners)
            {
                if (owner.Id == id)
                {
                    return owner;
                }
            }
            return null;
        }


        /**
         * Using this one to clone owner, when calling
         * GetAllOwnersIncludePets()
         **/
        public Owner CloneById(int id)
        {
            return _owners.Select(o => new Owner()
            {
                Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Address = o.Address,
                PhoneNumber = o.PhoneNumber
            }).
            FirstOrDefault(o => o.Id == id);
        }

        public Owner UpdateOwner(Owner ownerToUpdate)
        {
            var ownerFromDB = this.ReadById(ownerToUpdate.Id);
            if (ownerFromDB != null)
            {
                ownerFromDB.FirstName = ownerToUpdate.FirstName;
                ownerFromDB.LastName = ownerToUpdate.LastName;
                ownerFromDB.Address = ownerToUpdate.Address;
                ownerFromDB.PhoneNumber = ownerToUpdate.PhoneNumber;
                return ownerFromDB;
            }
            return null;
        }

        public FilteredList<Owner> ReadAllOwnersSearch(FilterSearch filter)
        {
            var filteredList = new FilteredList<Owner>();

            filteredList.TotalCount = _owners.Count();
            filteredList.FilterUsed = filter;

            IEnumerable<Owner> filtering = _owners;

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
    }
}
