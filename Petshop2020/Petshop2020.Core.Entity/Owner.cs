using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Entity
{
    public class Owner
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public int PhoneNumber { get; set; }

        public List<Pet> Pets { get; set; }

    }
}
