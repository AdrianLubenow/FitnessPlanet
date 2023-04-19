using FitnessPlanet.Abstraction;
using FitnessPlanet.Data;
using FitnessPlanet.Domain;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace FitnessPlanet.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(string name, int manifacturerId, int categoryId, string picture, string color, int quantity, decimal price, decimal discount, string description)
        {
            Product item = new Product
            {
                ProductName= name,
                Manifacturer = _context.Manifacturers.Find(manifacturerId),
                Category = _context.Categories.Find(categoryId),

                Picture = picture,
                Quantity = quantity,
                Price = price,
                Discount = discount,
                Description = description,
                Color = color

            };

            _context.Products.Add(item);
            return _context.SaveChanges() != 0;
        }

        public Product GetProductById(int productId) 
        {
            return _context.Products.Find(productId);
        }
        public List<Product> GetProducts()
        {
            List<Product> products = _context.Products.ToList();
            return products;
        }
        public bool RemoveById(int productId)
        {
            var product = GetProductById(productId);

            if (product == default(Product))
                return false;

            _context.Remove(product);
            return _context.SaveChanges() != 0;
        }

        public List<Product> GetProductsByCategoryAndManifacturer(string categoryName, string manifacturerName)
        {
            IQueryable<Product> query = _context.Products.Include(p => p.Category).Include(p => p.Manifacturer);

            if (!string.IsNullOrWhiteSpace(categoryName) && categoryName != "All")
            {
                query = query.Where(p => p.Category.CategoryName.ToLower() == categoryName.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(manifacturerName) && manifacturerName != "All")
            {
                query = query.Where(p => p.Manifacturer.ManifacturerName.ToLower() == manifacturerName.ToLower());
            }

            return query.ToList();
        }

        public bool Update(int productId, string name, int manifacturerId, int categoryId, string picture, string color, int quantity, decimal price, decimal discount, string description)
        {
            var product = GetProductById(productId);
            if (product == default(Product))
            { return false; }

            product.ProductName = name;
            product.Manifacturer = _context.Manifacturers.Find(manifacturerId);
            product.Category = _context.Categories.Find(categoryId);

            product.Picture = picture;
            product.Color = color;
            product.Quantity = quantity;
            product.Price = price;
            product.Discount = discount;
            product.Description = description;

            _context.Update(product);
            return _context.SaveChanges() != 0;
        }
    }
}
