using System.ComponentModel.DataAnnotations;

namespace FitnessPlanet.Models.Statistics
{
    public class StatisticsVM
    {
        [Display(Name = "Number of Clients")]
        public int CountClients { get; set; }
        [Display(Name = "Number of Products")]
        public int CountProducts { get; set; }
        [Display(Name = "Number of Orders")]
        public int CountOrders { get; set; }
        [Display(Name ="Total Sum of Orders")]
        public decimal SumOrders { get; set; }
    }
}
