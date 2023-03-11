using FitnessPlanet.Abstraction;
using FitnessPlanet.Data;
using FitnessPlanet.Domain;
using System.Collections.Generic;
using System.Linq;

namespace FitnessPlanet.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Category> GetCategories()
        {
            List<Category> categories = _context.Categories.ToList();
            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.Find(categoryId);
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Where(x => x.CategoryId == categoryId).ToList();
        }
    }
}
