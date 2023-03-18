using Castle.Components.DictionaryAdapter;
using FitnessPlanet.Domain;
using System.Collections.Generic;

namespace FitnessPlanet.Abstraction
{
    public interface IProductService
    {
        bool Create(string name, int manifacturerId, int categoryId, string picture, int quantity, decimal price, decimal discount);
        bool Update(int productId, string name, int manifacturerId, int categoryId, string picture, int quantity, decimal price, decimal discount);
        List<Product> GetProducts();
        Product GetProductById(int productId);
        bool RemoveById(int machineproductId);
        List<Product> GetProducts(string searchStringCategoryName, string searchStringManifacturerName);
    }
}
