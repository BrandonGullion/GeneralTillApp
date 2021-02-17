using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public double DiscountPercent { get; set; }
        public double DiscountAmount { get; set; }
        public double DiscountSubtotal { get; set; }
        public double SubTotal { get; set; }
        public double Total { get; set; }
        public Tax Tax { get; set; }
        public DateTime PurchaseTime { get; set; }


        public IEnumerable<CartItem> CartItems { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }


    }
}
