using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Application_Service
{
    public interface IOwnerService
    {
        // C - CREATE
        public Owner NewOwner(Owner owner);
        public Owner CreateOwner(Owner owner);

        // R - READ
        public FilteredList<Owner> GetAllOwners(FilterSearch filter);
        public Owner FindOwnerById(int id);
        public Owner FindOwnerByIdIncludePets(int id);


        // U - UPDATE
        public Owner UpdateOwner(Owner ownerToUpdate);


        // D - DELETE
        public Owner DeleteOwner(int id);

        


    }
}
