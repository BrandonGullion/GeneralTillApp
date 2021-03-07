using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Models
{
    public class CartProduct : Product
    {
        public double DiscountedPrice { get; set; }
        public bool Discounted { get; set; }
        public int QtyInCart { get; set; }
    }
}
