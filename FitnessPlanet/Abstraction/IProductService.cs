using Castle.Components.DictionaryAdapter;
using FitnessPlanet.Domain;
using System.Collections.Generic;

namespace FitnessPlanet.Abstraction
{
    public interface IProductService
    {
        bool Create(string name, int manifacturerId, int categoryId, string picture, string color, int quantity, decimal price, decimal discount, string description);
        bool Update(int productId, string name, int manifacturerId, int categoryId, string picture,string color, int quantity, decimal price, decimal discount, string description);
        List<Product> GetProducts();
        Product GetProductById(int productId);
        bool RemoveById(int machineproductId);
        List<Product> GetProductsByCategoryAndManifacturer(string categoryName, string manifacturerName);
    }
}
