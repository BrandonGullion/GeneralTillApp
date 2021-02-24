using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Models
{
    public class Transaction
    {
        // Identification props
        public int Id { get; set; }
        public string TransactionNumber { get; set; }

        // Discount props
        public double DiscountPercent { get; set; }
        public double DiscountAmount { get; set; }
        public double DiscountSubtotal { get; set; }

        // Total Props
        public double SubTotal { get; set; }
        public double Total { get; set; }
        
        // Payment Information Props
        public double AmountPaid { get; set; }
        public double AmountOwed { get; set; }
        public double AmountOwing { get; set; }
        public DateTime PurchaseDate { get; set; }

        // TODO :: May not use 
        [NotMapped]
        public Tax Tax { get; set; }


        // If a customer prop is involved 
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        // Lists
        [NotMapped]
        public IEnumerable<CartItem> CartItems { get; set; }
        [NotMapped]
        public IEnumerable<Payment> Payments { get; set; }

    }
}
