using FitnessPlanet.Models.Category;
using FitnessPlanet.Models.Manifacturer;
using FitnessPlanet.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace FitnessPlanet.Models.Product
{
    public class ProductCreateVM
    {
        public ProductCreateVM() 
        {
            Manifacturers = new List<ManifacturerPairVM>();
            Categories = new List<CategoryPairVM>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name ="Manifacturer")]
        public int ManifacturerId { get; set; }
        public virtual List<ManifacturerPairVM> Manifacturers { get; set; }
        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        public virtual List<CategoryPairVM> Categories { get; set; }

        [Display(Name ="Picture")]
        public string Picture { get; set; }
        [Required]
        [Range(0, 1000)]
        [Display(Name ="Quantity")]
        public int Quantity { get; set; }
        [Required]
        [Display(Name ="Price")]
        public decimal Price { get; set; }
        [Display(Name ="Discount")]
        public decimal Discount { get; set; }
    }
}
