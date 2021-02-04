using GeneralTillApp.Data;
using GeneralTillApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Managers
{
    public class ProductManager
    {
        public Product Product { get; set; }

        public ProductManager()
        {
            Product = new Product();
        }

        /// <summary>
        /// Sets the product data so it can be injected in another portion of the application 
        /// </summary>
        /// <param name="context">Current DB Context</param>
        /// <param name="product">product to set when selecting edit for example</param>
        public void SetProduct(Product product)
        {
            Product = product;
        }

        /// <summary>
        /// Gets and returns a product based on the Id
        /// </summary>
        /// <param name="context">Current Application Db Context</param>
        /// <param name="id">desired item id</param>
        /// <returns></returns>
        public Product GetProduct(ApplicationDbContext context, int id)
        {
            var product = new Product();

            if (context != null && id != 0)
            {
                product = context.Products.FirstOrDefault(p => p.Id == id);
            }

            return product;
        }
    }
}
