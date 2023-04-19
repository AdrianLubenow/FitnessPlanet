using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FitnessPlanet.Models.Order
{
    public class OrderIndexVM
    {
        public int Id { get; set; }
        public string OrderDate { get; set; }
        public string UserId { get; set; }
        public string User { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }
        public string Picture { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Description { get; set; }
    }
}
