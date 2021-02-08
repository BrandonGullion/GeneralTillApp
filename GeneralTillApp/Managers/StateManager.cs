using GeneralTillApp.Managers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralTillApp.Managers
{
    // This class will be used to store session data for editing and adding forms 
    public class StateManager
    {
        public ProductManager ProductManager { get; set; }
        public CustomerManager CustomerManager { get; set; }
        public NotificationManager NotificationManager { get; set; }

        public StateManager()
        {
            ProductManager = new ProductManager();
            CustomerManager = new CustomerManager();
            NotificationManager = new NotificationManager();
        }
    }
}
