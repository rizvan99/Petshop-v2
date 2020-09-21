using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Entity
{
    public class PetType
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public List<Pet> Pets { get; set; }
    }
}
