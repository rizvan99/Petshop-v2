using Petshop2020.Core.Domain_Service;
using Petshop2020.Core.Entity;
using Petshop2020.Core.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop2020.Core.Application_Service.Service
{
    public class OwnerService : IOwnerService
    {
        readonly IOwnerRepository _ownerRepo;
        readonly IPetRepository _petRepo;

        public OwnerService(IOwnerRepository ownerRepository, IPetRepository petRepository)
        {
            _ownerRepo = ownerRepository;
            _petRepo = petRepository;
        }

        

        public Owner CreateOwner(Owner owner)
        {
            return _ownerRepo.CreateOwner(owner);
        }

        public Owner DeleteOwner(int id)
        {
            return _ownerRepo.DeleteOwner(id);
        }

        public Owner FindOwnerById(int id)
        {
            return _ownerRepo.ReadById(id);
        }

        public Owner FindOwnerByIdIncludePets(int id)
        {
            var owner = _ownerRepo.CloneById(id);
           
            if (owner != null)
            {
                owner.Pets = _petRepo.AllPetsFromList().Where(pet => pet.Owner.Id == owner.Id).ToList();
            }
            else
            {
                return _ownerRepo.ReadById(id);
            }

            return owner;
        }

        public FilteredList<Owner> GetAllOwners(FilterSearch filter)
        {
            if (!string.IsNullOrEmpty(filter.SearchText) && string.IsNullOrEmpty(filter.SearchField))
            {
                filter.SearchField = "FirstName";
            }

            return _ownerRepo.ReadAllOwnersSearch(filter);
        }

        public Owner NewOwner(Owner owner)
        {
            var newOwner = new Owner()
            {
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Address = owner.Address,
                PhoneNumber = owner.PhoneNumber
            };
            return newOwner;
        }

        public Owner UpdateOwner(Owner ownerToUpdate)
        {
            var owner = FindOwnerById(ownerToUpdate.Id);
            owner.FirstName = ownerToUpdate.FirstName;
            owner.LastName = ownerToUpdate.LastName;
            owner.Address = ownerToUpdate.Address;
            owner.PhoneNumber = ownerToUpdate.PhoneNumber;
            return owner;
        }
    }
}
