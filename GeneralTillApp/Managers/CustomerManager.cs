using GeneralTillApp.Data;
using GeneralTillApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Managers
{
    public class CustomerManager
    {
        public Customer Customer { get; set; }

        public CustomerManager()
        {
            Customer = new Customer();
        }

        /// <summary>
        /// Sets the product data so it can be injected in another portion of the application 
        /// </summary>
        /// <param name="context">Current DB Context</param>
        /// <param name="product">product to set when selecting edit for example</param>
        public void SetProduct(Customer customer)
        {
            Customer = customer;
        }

        /// <summary>
        /// Gets and returns a product based on the Id
        /// </summary>
        /// <param name="context">Current Application Db Context</param>
        /// <param name="id">desired item id</param>
        /// <returns></returns>
        public Customer GetCustomer(ApplicationDbContext context, int id)
        {
            var customer = new Customer();

            if (context != null && id != 0)
            {
                customer = context.Customers.FirstOrDefault(p => p.Id == id);
            }

            return customer;
        }
    }
}
