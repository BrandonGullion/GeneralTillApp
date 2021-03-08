using GeneralTillApp.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ApplicationDbContext Context { get; set; }

        // Discount props
        public double DiscountSubtotal { get; set; }

        public Cart Cart { get; set; }

        [NotMapped]
        public double OldSubtotal { get; set; }

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


        // Currently Selected Customer
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        // Lists
        [NotMapped]
        public IEnumerable<Payment> Payments { get; set; }

        #region Constructor 

        public Transaction(ApplicationDbContext context)
        {
            Context = context;
            Payments = new List<Payment>();
            Cart = new Cart();
            Tax = new Tax();
            Customer = new Customer();
        }
        #endregion

        #region Methods

        // Checks to see if the passed upc exists in the DB if so add to cart items and re calc totals 
        public void CheckUPC(string upc, int quantity)
        {
            Cart.CartProduct = new CartProduct(Context.Products.Where(p => p.UPC == upc).FirstOrDefault());

            if (Cart.CartProduct != null)
            {
                Cart.AddProductToCart(quantity);
               CalculateTotal(Customer);
            }
            else
                Console.WriteLine($"Could not find item with UPC : {upc}");

        }

        // Used to calculate the tax on transaction subtotal 
        public void CalcTax()
        {
            Tax.CartGST = SubTotal * 0.05;
            Tax.CartPST = SubTotal * 0.07;
            Total = SubTotal + Tax.CartPST + Tax.CartGST;
        }

        // Calculates the total of all product prices in the cart items list 
        public void CalculateTotal(Customer customer)
        {
            SubTotal = 0;
            DiscountSubtotal = 0;
            OldSubtotal = 0;

            // Going over each item in the cart and adding to total
            foreach (var cartProduct in Cart.CartProducts)
            {
                // Accounting for idv. discounted subtotals

                // If both the item and the customer have discounts 
                if (cartProduct.Discounted && customer.DiscountPercent > 0)
                {

                }
                // If customer discount is appropriate 
                else if (customer.DiscountPercent > 0)
                {
                }
                // If the item is discounted itself 
                else if (cartProduct.Discounted)
                {
                }
                // No discounts 
                else
                {
                    SubTotal += cartProduct.Price;
                }
            }

            // Calculate the taxes against the sub total 
            CalcTax();

            AmountOwing = Total;
            AmountPaid = Total;
        }

        // New all required fields in the transaction model 
        public void ClearTransaction()
        {
            Cart = new Cart();
            Customer = new Customer();
            Payments = new List<Payment>();
        }




        #endregion
    }
}
