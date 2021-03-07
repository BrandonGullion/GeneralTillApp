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
        public double DiscountPercent { get; set; }
        public double DiscountAmount { get; set; }
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


        // If a customer prop is involved 
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
            Cart = new Cart() { CartProducts = new List<CartProduct>() };
        }


        #endregion



        #region Methods

        // Used to calculate the tax on transaction subtotal 
        public void CalcTax()
        {
            Tax.CartGST = SubTotal * 0.05;
            Tax.CartPST = SubTotal * 0.07;
            Total = SubTotal + Tax.CartPST + Tax.CartGST;
        }

        // Calculates the total of all product prices in the cart items list 
        public void CalcTotal(Customer customer)
        {
            SubTotal = 0;
            DiscountSubtotal = 0;
            OldSubtotal = 0;

            // Going over each item in the cart and adding to total
            foreach (var cartItem in Cart.CartProducts)
            {
                // Accounting for idv. discounted subtotals

                // If both the item and the customer have discounts 
                if (cartItem.Discounted && customer.DiscountPercent > 0)
                {

                    CalcTax();
                }
                // If customer discount is appropriate 
                else if (customer.DiscountPercent > 0)
                {

                    CalcTax();
                }
                // If the item is discounted itself 
                else if (cartItem.Discounted)
                {
                    CalcTax();
                }
                // No discounts 
                else
                {
                    CalcTax();
                }
            }

            AmountOwing = Total;
            AmountPaid = Total;
        }

        // Calcs item discounts 
        public void CalcCartItemDiscount(Cart cart)
        {
            cart.DiscountSubTotal = 0;

            /* Only used if there is a customer discount along with adjust discount applied, uses
                 CustomerDiscountTotal from Calc() */
            if (cart.CustomerDiscountTotal > 0)
            {
                if (cart.AmountDiscountedBool && cart.PercentDiscountedBool)
                {
                    var tempTotal = cart.CustomerDiscountTotal - cart.DiscountAmount;
                    cart.DiscountSubTotal = tempTotal - (tempTotal * (cart.DiscountPercent / 100));
                }

                else if (cart.DiscountPercent > 0)
                {
                    cart.DiscountSubTotal = cart.CustomerDiscountTotal - (cart.CustomerDiscountTotal * cart.DiscountPercent / 100);
                }

                else if (cart.DiscountAmount >= 0)
                {
                    cart.DiscountSubTotal = cart.CustomerDiscountTotal - cart.DiscountAmount;
                }

                else if (cart.DiscountPercent == 0)
                {
                    cart.DiscountSubTotal = cart.CustomerDiscountTotal;
                }
            }
            else
            {
                if (cart.AmountDiscountedBool && cart.PercentDiscountedBool)
                {
                    var tempTotal = cart.Product.Cost - cart.DiscountAmount;
                    cart.DiscountSubTotal = tempTotal - (tempTotal * (cart.DiscountPercent / 100));
                }

                else if (cart.DiscountPercent > 0)
                {
                    cart.DiscountSubTotal = cart.Product.Cost - (cart.Product.Cost * cart.DiscountPercent / 100);
                }

                else if (cart.DiscountAmount >= 0)
                {
                    cart.DiscountSubTotal = cart.Product.Cost - cart.DiscountAmount;
                }

                else if (cart.DiscountPercent == 0)
                {
                    cart.DiscountSubTotal = cart.Product.Cost;
                }
            }
        }

        // Add item to the Transaction Cart
        public void AddToCart(IProduct product, int quantity, Customer customer)
        {
            var Duplicate = false;

            // If the item that was scanned is already in cart, follow this flow
            if (Cart.CartProducts.Contains(Cart.CartProducts.Where(c => c.UPC == product.UPC).FirstOrDefault()))
            {
                var index = Cart.CartProducts.IndexOf(Cart.CartProducts.Where(c => c.UPC == product.UPC).FirstOrDefault());
                Cart.CartProducts[index].QtyInCart += quantity;
                Duplicate = true;
            }

            // If the scanned item is new, follow this flow
            if (!Duplicate)
            {
                Cart.CartProducts.Add((CartProduct)product);
                CalcTotal(customer);
            }

            // If already in the cart follow this flow after count has been updated 
            if (Duplicate)
            {
                CalcTotal(customer);
                Duplicate = false;
            }


            // SfGrid.Refresh();
            // StateHasChanged();
        }

        // Validates total cart discount percent and then calls CalcCartDiscountAmount to update the view
        public void ApplyCartDiscount(string value, AdjustTypeEnum adjustType, IEnumerable<Cart> cartItems,
            Customer customer)
        {
            // Checking for any alpha characters
            bool containsLetters = value.Any(x => !char.IsLetter(x));

            // If none convert to double and pass to calc whole cart discount
            if (containsLetters)
            {
                var convertValue = Convert.ToDouble(value);

                if (adjustType == AdjustTypeEnum.Percent)
                {
                    foreach (var cartItem in cartItems)
                    {
                        cartItem.Discounted = true;
                        cartItem.DiscountPercent = convertValue;
                        CalcCartItemDiscount(cartItem);
                    }
                }
                else if (adjustType == AdjustTypeEnum.Amount)
                {

                }

                CalcTotal(customer);
            }
            else
            {
                //TODO :: Inform that no alphabetical characters are allowed
                Console.WriteLine("Not allowed character in the discount field");
            }
        }
        
        // Checks to see if the passed upc exists in the DB if so add to cart items and re calc totals 
        public void CheckUPC(string upc, int quantity, Customer customer)
        {
            Cart.Product = Context.Products.FirstOrDefault(p => p.UPC == upc);

            if (Cart.Product != null)
                AddToCart(Cart.Product, quantity, customer);
            else
                Console.WriteLine($"Could not find item with UPC : {upc}");
        }

        // Validates indv cart item percent and calls CalcCartItemDiscount to update view
        public void ApplyIndvItemDiscount(Cart value, object percent, AdjustTypeEnum adjustType)
        {
            // Checking for any alpha characters
            bool containsLetters = percent.ToString().Any(x => !char.IsLetter(x));

            // If no alpha calc indv item discounted value
            if (containsLetters)
            {
                var discountValue = Convert.ToDouble(percent);

                if (adjustType == AdjustTypeEnum.Percent)
                {
                    if (value.DiscountPercent >= 0)
                    {
                        value.DiscountPercent = discountValue;
                        value.PercentDiscountedBool = true;
                        value.Discounted = true;
                        CalcCartItemDiscount(value);
                    }
                }
                else if (adjustType == AdjustTypeEnum.Amount)
                {
                    if (value.DiscountAmount >= 0)
                    {
                        value.DiscountAmount = discountValue;
                        value.AmountDiscountedBool = true;
                        value.Discounted = true;
                        CalcCartItemDiscount(value);
                    }
                }
            }
            else
            {
                Console.WriteLine("Not allowed character in the discount field");
            }
        }

        #endregion
    }
}
