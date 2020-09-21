using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Application_Service
{
    public interface IOwnerService
    {
        public Owner NewOwner(Owner owner);

        public Owner CreateOwner(Owner owner);

        public FilteredList<Owner> GetAllOwners(FilterSearch filter);

        public Owner UpdateOwner(Owner ownerToUpdate);

        public Owner DeleteOwner(int id);

        public Owner FindOwnerById(int id);

        public Owner FindOwnerByIdIncludePets(int id);


    }
}
