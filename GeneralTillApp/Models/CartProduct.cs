using GeneralTillApp.Data;
using GeneralTillApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Models
{
    public class CartProduct : Product, ICartProduct
    {
        #region Properties 

        public double DiscountedPrice { get; set; }
        public double DiscountPercent { get; set; }
        public double DiscountAmount { get; set; }
        public DiscountTypeEnum DiscountType { get; set; }
        public double CustomerSavings { get; set; }
        public bool Discounted { get; set; }
        public int QtyInCart { get; set; }

        #endregion

        #region Constructor 

        /// <summary>
        /// Generic Constructor 
        /// </summary>
        public CartProduct()
        {

        }

        /// <summary>
        /// Constructor used when creating new CartProduct from existing product 
        /// </summary>
        /// <param name="product">Product Model that will be used to transfer all of the information</param>
        public CartProduct(Product product)
        {
            Title = product.Title;
            Description = product.Description;
            UPC = product.UPC;
            Discountable = product.Discountable;
            PST = product.PST;
            GST = product.GST;
            OnHand = product.OnHand;
            UnitSize = product.UnitSize;
            UnitOfMeasureId = product.UnitOfMeasureId;
            UnitOfMeasure = product.UnitOfMeasure;
            ShelfMax = product.ShelfMax;
            ShelfMin = product.ShelfMin;
            ProductGroupId = product.ProductGroupId;
            ProductGroup = product.ProductGroup;
            Price = product.Price;
            AcqCost = product.AcqCost;
            Tax = product.Tax;
            DateAdded = product.DateAdded;
            LastEdited = product.LastEdited;
            OnOrder = product.OnOrder;
        }

        #endregion

        #region Methods

        // Returns a discounted price for the given individual item 
        public double GetDiscountedPrice(Customer customer)
        {
            // Customer discount and adjust discount types are applied 
            if(customer.DiscountPercent > 0 && Discounted)
            {
                if (DiscountType == DiscountTypeEnum.Percent)
                    return Price;
                else if (DiscountType == DiscountTypeEnum.Amount)
                    return Price;
                else if (DiscountType == DiscountTypeEnum.BothPercentAndAmount)
                    return Price;
                else
                    return Price;
            }
            // Only customer discount present 
            else if(customer.DiscountPercent > 0)
            {
                return Price;
            }
            // Only manager discount present 
            else if(DiscountType != DiscountTypeEnum.None)
            {
                return Price;
            }
            // If this is hit return the regular price and make sure to set discounted to false 
            else
            {
                Discounted = false;
                return Price;
            }
        }

        // Returns the percent discounted item price 
        public double GetPercentDiscountedPrice()
        {
            return Price - (Price * DiscountPercent / 100);
        }

        // Returns the Amount discounted Item price 
        public double GetAmountDiscountedPrice()
        {
            return Price - DiscountAmount;
        }

        // Returns the Customer Discounted Item price 
        public double GetCustomerDiscountedPrice(Customer customer)
        {
            return Price - (Price * customer.DiscountPercent / 100);
        }

        #endregion
    }
}
