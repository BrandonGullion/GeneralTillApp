using GeneralTillApp.Data;
using GeneralTillApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string TransactionNumber { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        public CartProduct CartProduct { get; set; }
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

        public Cart()
        {
            CartProducts = new List<CartProduct>();
        }


        #region Methods

        // Add Item to the cart 
        public void AddProductToCart(int quantity)
        {
            var Duplicate = false;

            // If the item that was scanned is already in cart, follow this flow
            if (CartProducts.Contains(CartProduct))
            {
                var index = CartProducts.IndexOf(CartProducts.Where(c => c.UPC == CartProduct.UPC).FirstOrDefault());
                CartProducts[index].QtyInCart += quantity;
                Duplicate = true;
            }

            // If the scanned item is new, follow this flow
            if (!Duplicate)
                CartProducts.Add(CartProduct);

            // If already in the cart follow this flow after count has been updated 
            if (Duplicate)
                Duplicate = false;


            // SfGrid.Refresh();
            // StateHasChanged();
        }

        // Calcs item discounts 
        public void CalcCartItemDiscount()
        {
            DiscountSubTotal = 0;

            /* Only used if there is a customer discount along with adjust discount applied, uses
                 CustomerDiscountTotal from Calc() */
            if (CustomerDiscountTotal > 0)
            {
                if (AmountDiscountedBool && PercentDiscountedBool)
                {
                    var tempTotal = CustomerDiscountTotal - DiscountAmount;
                    DiscountSubTotal = tempTotal - (tempTotal * (DiscountPercent / 100));
                }

                else if (DiscountPercent > 0)
                {
                    DiscountSubTotal = CustomerDiscountTotal - (CustomerDiscountTotal * DiscountPercent / 100);
                }

                else if (DiscountAmount >= 0)
                {
                    DiscountSubTotal = CustomerDiscountTotal - DiscountAmount;
                }

                else if (DiscountPercent == 0)
                {
                    DiscountSubTotal = CustomerDiscountTotal;
                }
            }
            else
            {
                if (AmountDiscountedBool && PercentDiscountedBool)
                {
                    var tempTotal = CartProduct.Cost - DiscountAmount;
                    DiscountSubTotal = tempTotal - (tempTotal * (DiscountPercent / 100));
                }

                else if (DiscountPercent > 0)
                {
                    DiscountSubTotal = CartProduct.Cost - (CartProduct.Cost * DiscountPercent / 100);
                }

                else if (DiscountAmount >= 0)
                {
                    DiscountSubTotal = CartProduct.Cost - DiscountAmount;
                }

                else if (DiscountPercent == 0)
                {
                    DiscountSubTotal = CartProduct.Cost;
                }
            }
        }

        // Validates total cart discount percent and then calls CalcCartDiscountAmount to update the view
        public void ApplyCartDiscount(string value, AdjustTypeEnum adjustType)
        {
            // Checking for any alpha characters
            bool containsLetters = value.Any(x => !char.IsLetter(x));

            // If none convert to double and pass to calc whole cart discount
            if (containsLetters)
            {
                var convertValue = Convert.ToDouble(value);

                if (adjustType == AdjustTypeEnum.Percent)
                {
                    foreach (var cartItem in CartProducts)
                    {
                        cartItem.Discounted = true;
                        cartItem.DiscountPercent = convertValue;
                        CalcCartItemDiscount();
                    }
                }
                else if (adjustType == AdjustTypeEnum.Amount)
                {

                }
            }
            else
            {
                //TODO :: Inform that no alphabetical characters are allowed
                Console.WriteLine("Not allowed character in the discount field");
            }
        }

        // Validates indv cart item percent and calls CalcCartItemDiscount to update view
        public void ApplyIndvItemDiscount(CartProduct cartProduct, object percent, AdjustTypeEnum adjustType)
        {
            // Checking for any alpha characters
            bool containsLetters = percent.ToString().Any(x => !char.IsLetter(x));

            // If no alpha calc indv item discounted value
            if (containsLetters)
            {
                var discountValue = Convert.ToDouble(percent);

                if (adjustType == AdjustTypeEnum.Percent)
                {
                    if (cartProduct.DiscountPercent >= 0)
                    {
                        cartProduct.DiscountPercent = discountValue;
                        cartProduct.Discounted = true;
                        CalcCartItemDiscount();
                    }
                }
                else if (adjustType == AdjustTypeEnum.Amount)
                {
                    if (cartProduct.DiscountAmount >= 0)
                    {
                        cartProduct.DiscountAmount = discountValue;
                        cartProduct.Discounted = true;
                        CalcCartItemDiscount();
                    }
                }
            }
            else
            {
                Console.WriteLine("Not allowed character in the discount field");
            }
        }

        // Clears the current cart
        public void ClearCart()
        {
            CartProducts.Clear();
        }

        #endregion

    }
}
