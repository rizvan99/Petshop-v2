using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Domain_Service
{
    public interface IOwnerRepository
    {
        // C - CREATE
        public Owner CreateOwner(Owner owner);


        // R - READ
        public IEnumerable<Owner> GetAllOwners();
        public FilteredList<Owner> ReadAllOwnersSearch(FilterSearch filter);
        public Owner ReadById(int id);
        public Owner ReadByIdIncludePets(int id);
        public Owner CloneById(int id);


        // U - UPDATE
        public Owner UpdateOwner(Owner ownerToUpdate);


        // D - DELETE
        public Owner DeleteOwner(int id);

    }
}
