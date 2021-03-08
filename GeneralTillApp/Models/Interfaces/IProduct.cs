using System;

namespace GeneralTillApp.Models
{
    public interface IProduct
    {
        double AcqCost { get; set; }
        double Price { get; set; }
        DateTime DateAdded { get; set; }
        string Description { get; set; }
        bool Discountable { get; set; }
        bool GST { get; set; }
        int Id { get; set; }
        DateTime LastEdited { get; set; }
        int OnHand { get; set; }
        bool OnOrder { get; set; }
        ProductGroup ProductGroup { get; set; }
        int ProductGroupId { get; set; }
        bool PST { get; set; }
        int ShelfMax { get; set; }
        int ShelfMin { get; set; }
        Tax Tax { get; set; }
        string Title { get; set; }
        UnitOfMeasure UnitOfMeasure { get; set; }
        int UnitOfMeasureId { get; set; }
        int UnitSize { get; set; }
        string UPC { get; set; }
    }
}