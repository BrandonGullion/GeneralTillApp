using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralTillApp.Models
{
    public class ProductGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductsInGroup { get; set; }
        public bool Promotion { get; set; }
        public double PromotionPercent { get; set; }
        public double PromotionAmount { get; set; }
        public DateTime PromoStartDate { get; set; }
        public DateTime PromoEndDate { get; set; }

    }
}
