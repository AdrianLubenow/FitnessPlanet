using FitnessPlanet.Abstraction;
using FitnessPlanet.Domain;
using FitnessPlanet.Models.Category;
using FitnessPlanet.Models.Manifacturer;
using FitnessPlanet.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using System.Collections.Generic;
using System.Linq;

namespace FitnessPlanet.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IManifacturerService manifacturerService;

        public ProductController(IProductService productService, ICategoryService categoryService, IManifacturerService manifacturerService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.manifacturerService = manifacturerService;
        }

        public ActionResult Create() 
        {
            var product = new ProductCreateVM();
            product.Manifacturers = manifacturerService.GetManifacturers().Select(x => new ManifacturerPairVM()
            {
                Id = x.Id,
                Name = x.ManifacturerName
            }).ToList();
            product.Categories = categoryService.GetCategories().Select(x => new CategoryPairVM()
            {
                Id = x.Id,
                Name = x.CategoryName
            }).ToList();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] ProductCreateVM product) 
        {
            if (ModelState.IsValid)
            {
                var createdId = productService.Create(product.ProductName, product.ManifacturerId, product.CategoryId, product.Picture, product.Quantity, product.Price, product.Discount);

                if (createdId)
                    return RedirectToAction("CreateSuccess");
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Index(string searchStringCategoryName, string searchStringManifacturerName)
        {
            List<ProductIndexVM> products = productService.GetProducts(searchStringCategoryName, searchStringManifacturerName).Select(product => new ProductIndexVM
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ManifacturerId = product.ManifacturerId,
                ManifacturerName = product.Manifacturer.ManifacturerName,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.CategoryName,
                Picture = product.Picture,
                Quantity = product.Quantity,
                Price = product.Price,
                Discount = product.Discount
            }).ToList();
            return this.View(products);
        }
        public ActionResult Edit(int id)
        {
            Product product = productService.GetProductById(id);

            if (product == null)
                return NotFound();

            ProductEditVM updatedProduct = new ProductEditVM()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ManifacturerId = product.ManifacturerId,
                CategoryId = product.CategoryId,
                Picture = product.Picture,
                Quantity = product.Quantity,
                Price = product.Price,
                Discount = product.Discount
            };
            updatedProduct.Categories = categoryService.GetCategories().Select(c => new CategoryPairVM()
            {
                Id = c.Id,
                Name = c.CategoryName
            }).ToList();
            return View(updatedProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductEditVM product)
        {
            {
                if(ModelState.IsValid)
                {
                    var updated = productService.Update(id, product.ProductName, product.ManifacturerId, product.CategoryId, product.Picture, product.Quantity, product.Price,
                        product.Discount);

                    if (updated)
                        return this.RedirectToAction("Index");
                }
                return View(product);
            }
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Product item = productService.GetProductById(id);
            
            if (item == null)
                return NotFound();

            ProductDetailsVM product = new ProductDetailsVM()
            {
                Id = item.Id,
                ProductName = item.ProductName,
                ManifacturerId = item.ManifacturerId,
                ManufacturerName = item.Manifacturer.ManifacturerName,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.CategoryName,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            };
            return View(product);
        }
        public ActionResult Delete(int id)
        {
            Product item = productService.GetProductById(id);
            
            if(item==null)
                return NotFound();

            ProductDeleteVM product = new ProductDeleteVM()
            {
                Id = item.Id,
                ProductName = item.ProductName,
                ManifacturerId = item.ManifacturerId,
                ManufacturerName = item.Manifacturer.ManifacturerName,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.CategoryName,
                Picture = item.Picture,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            };
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = productService.RemoveById(id);

            if (deleted)
                return this.RedirectToAction("DeleteSuccess");
            else
                return View();
        }
        public IActionResult Success()
        {
            return View();
        }
        public IActionResult CreateSuccess()
        {
            return this.View(); 
        }
        public IActionResult DeleteSuccess()
        {
            return this.View(); 
        }
    }
}
