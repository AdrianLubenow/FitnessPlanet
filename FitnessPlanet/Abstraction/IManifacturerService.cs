using FitnessPlanet.Domain;
using System.Collections.Generic;

namespace FitnessPlanet.Abstraction
{
    public interface IManifacturerService
    {
        List<Manifacturer> GetManifacturers();
        Manifacturer GetManifacturerById(int manifacturerId);
        List<Product> GetProductsByManifacturer(int manifacturerId);
    }
}
