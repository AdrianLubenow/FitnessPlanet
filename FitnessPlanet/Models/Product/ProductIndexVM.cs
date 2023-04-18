using System.ComponentModel.DataAnnotations;

namespace FitnessPlanet.Models.Product
{
    public class ProductIndexVM
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }
        public int ManifacturerId { get; set; }
        [Display(Name ="Manifacturer")]
        public string ManifacturerName { get; set; }
        public int CategoryId { get; set; }
        [Display(Name ="Category")]
        public string CategoryName { get; set; }
        public string Picture { get; set; }
        [Display(Name ="Quantity")]
        public int Quantity { get;set; }
        [Display(Name ="Price")]
        public decimal Price { get; set; }
        [Display(Name ="Discount")]
        public decimal Discount { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Color")]
        public string Color { get; set; }
    }
}
