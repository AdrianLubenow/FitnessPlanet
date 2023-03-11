using FitnessPlanet.Domain;
using System.Collections.Generic;

namespace FitnessPlanet.Abstraction
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        Category GetCategoryById(int categoryId);
        List<Product> GetProductsByCategory(int categoryId);
    }
}
