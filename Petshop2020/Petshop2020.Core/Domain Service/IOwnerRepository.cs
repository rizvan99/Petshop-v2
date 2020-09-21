using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Domain_Service
{
    public interface IOwnerRepository
    {
        public Owner CreateOwner(Owner owner);

        public IEnumerable<Owner> GetAllOwners();

        public FilteredList<Owner> ReadAllOwnersSearch(FilterSearch filter);

        public Owner UpdateOwner(Owner ownerToUpdate);

        public Owner DeleteOwner(int id);

        public Owner ReadById(int id);

        public Owner CloneById(int id);
    }
}
