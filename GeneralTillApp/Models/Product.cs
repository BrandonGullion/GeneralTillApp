using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralTillApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string UPC { get; set; }

        public bool Discountable { get; set; }
        public bool PST { get; set; }
        public bool GST { get; set; }

        public int OnHand { get; set; }
        public int UnitSize { get; set; }
        public int UnitOfMeasureId { get; set; }

        [ForeignKey(nameof(UnitOfMeasureId))]
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public int ShelfMin { get; set; }
        public int ShelfMax { get; set; }
        public int ProductGroupId { get; set; }
        [ForeignKey(nameof(ProductGroupId))]
        public ProductGroup ProductGroup { get; set; }
        public double Cost { get; set; }
        public double AcqCost { get; set; }

        [NotMapped]
        public Tax Tax { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastEdited { get; set; }
        public bool OnOrder { get; set; }
    }
}
