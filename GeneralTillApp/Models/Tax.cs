using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralTillApp.Models
{
    public class Tax
    {
        public int Id { get; set; }
        public double PST { get; set; }
        public double GST { get; set; }
        public double CartGST { get; set; }
        public double CartPST { get; set; }

    }
}
