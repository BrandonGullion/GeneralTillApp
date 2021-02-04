using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Models
{
    public class CustomerGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double DiscountPercent { get; set; }
    }
}
