using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralTillApp.Data;

namespace GeneralTillApp.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public PaymentTypeEnum PaymentType { get; set; }
        public double PaymentAmount { get; set; }
    }
}
