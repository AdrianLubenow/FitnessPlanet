using System.ComponentModel.DataAnnotations;

namespace FitnessPlanet.Models.Manifacturer
{
    public class ManifacturerPairVM
    {
        public int Id { get; set; }

        [Display(Name ="Manifacturer")]
        public string Name { get; set; }
    }
}
