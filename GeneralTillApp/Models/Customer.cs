using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralTillApp.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string CustomerCode { get; set; }

        [Display(Name = "Address")]
        public string StreetAddress { get; set; }
        public string City { get; set; }


        [Display(Name ="Province/State")]
        public string Province_State { get; set; }

        [Display(Name ="Postal Code")]
        public string PostalCode { get; set; }
        public string Country { get; set; }

        [Display(Name = "Phone Number")]
        public string TelephoneNumber { get; set; }
        public string Note { get; set; }
        public string Email { get; set; }

        [Display(Name ="A/R Balance")]
        public double ARBalance { get; set; }

        [Display(Name ="A/R Limit")]
        public double ARLimit { get; set; }

        [Display(Name ="Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name ="Last Edit")]
        public DateTime LastEdited { get; set; }

        [Display(Name ="Discount %")]
        public double DiscountPercent { get; set; }

        public int CustomerGroupId { get; set; }

        [Display(Name ="Customer Group")]
        [ForeignKey(nameof(CustomerGroupId))]
        public CustomerGroup CustomerGroup { get; set; }


    }
}
