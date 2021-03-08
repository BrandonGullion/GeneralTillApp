using GeneralTillApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Models
{
    public class CartProduct : Product, ICartProduct
    {
        public CartProduct()
        {

        }

        /// <summary>
        /// 
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
            Cost = product.Cost;
            AcqCost = product.AcqCost;
            Tax = product.Tax;
            DateAdded = product.DateAdded;
            LastEdited = product.LastEdited;
            OnOrder = product.OnOrder;
        }

        public double DiscountedPrice { get; set; }
        public double DiscountPercent { get; set; }
        public double DiscountAmount { get; set; }
        public double CustomerSavings { get; set; }
        public bool Discounted { get; set; }
        public int QtyInCart { get; set; }
    }
}
