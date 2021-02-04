﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralTillApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province_State { get; set; }
        public string Country { get; set; }
        public string TelephoneNumber { get; set; }
        public string Note { get; set; }
        public string Email { get; set; }
        public double ARBalance { get; set; }
        public DateTime DateCreated { get; set; }
        
        public int CustomerGroupId { get; set; }

        [ForeignKey(nameof(CustomerGroupId))]
        public CustomerGroup CustomerGroup { get; set; }


    }
}
