using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Models.Interfaces
{
    public interface ICartProduct : IProduct 
    {
        double DiscountedPrice { get; set; }
        double DiscountPercent { get; set; }
        double DiscountAmount { get; set; }
        bool Discounted { get; set; }
        int QtyInCart { get; set; }
    }
}
