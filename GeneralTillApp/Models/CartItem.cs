using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public double ItemSubtotal { get; set; }
        public double DiscountAmount { get; set; }
        public double DiscountPercent { get; set; }
        public double DiscountSubTotal { get; set; }
        public double CustomerDiscountTotal { get; set; }
        public bool Discounted { get; set; }
        public bool AmountDiscountedBool { get; set; }
        public bool PercentDiscountedBool { get; set; }
        public double CustomerDiscountSavings { get; set; }
        public double AdjustDiscountSavings { get; set; }

    }
}
