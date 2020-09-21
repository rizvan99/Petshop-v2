﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop2020.Core.Entity
{
    public class Pet
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime SoldDate { get; set; }

        public PetType Type { get; set; }

        public DateTime BirthDate { get; set; }

        public string Color { get; set; }

        public string PreviousOwner { get; set; }

        public Double Price { get; set; }

        public Owner Owner { get; set; }

        
    }
}
