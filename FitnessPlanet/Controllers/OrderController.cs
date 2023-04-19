using FitnessPlanet.Data;
using FitnessPlanet.Domain;
using FitnessPlanet.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Claims;

namespace FitnessPlanet.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.SingleOrDefault(u=> u.Id == userId);

            List<OrderIndexVM> orders = _context.Orders.Select(x => new OrderIndexVM
            {
                Id = x.Id,
                OrderDate = x.OrderDate.ToString("dd-MMM,yyyy hh:mm", CultureInfo.InvariantCulture),
                UserId = x.UserId,
                User = x.User.UserName,
                ProductId = x.ProductId,
                Product = x.Product.ProductName,
                Picture = x.Product.Picture,
                Color = x.Product.Color,
                Quantity = x.Quantity,
                Price = x.Price,
                Discount = x.Discount,
                TotalPrice = x.TotalPrice,
                Description = x.Description
            }).ToList();
            return View(orders);
        }

        public IActionResult MyOrders(string searchString)
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this._context.Users.SingleOrDefault(u => u.Id == currentUserId);

            if (user == null)
                return null;

            List<OrderIndexVM> orders = _context.Orders.Where(x => x.UserId == user.Id).Select(x => new OrderIndexVM
            {
                Id = x.Id,
                OrderDate = x.OrderDate.ToString("dd-MMM,yyyy hh:mm", CultureInfo.InvariantCulture),
                UserId = x.UserId,
                User = x.User.UserName,
                ProductId = x.ProductId,
                Product = x.Product.ProductName,
                Picture = x.Product.Picture,
                Color = x.Product.Color,
                Quantity = x.Quantity,
                Price = x.Price,
                Discount = x.Discount,
                TotalPrice = x.TotalPrice,
                Description = x.Description
            }).ToList();

            if (!String.IsNullOrEmpty(searchString))
                orders = orders.Where(o => o.Product.ToLower().Contains(searchString.ToLower())).ToList();

            return this.View(orders);
        }

        public ActionResult Create(int productId, int quantity)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = this._context.Users.SingleOrDefault(u => u.Id == userId);
            var product = this._context.Products.SingleOrDefault(x => x.Id == productId);

            if (user == null || product == null || quantity < 1 || quantity > product.Quantity)
            {
                TempData["ErrorMessage"] = $"The quantity should be between 1 and {product.Quantity}.";

                return RedirectToAction("Index", "Product");
            }

            OrderConfirmVM orderForDb = new OrderConfirmVM
            {
                UserId = userId,
                User = user.UserName,
                ProductId = productId,
                ProductName = product.ProductName,
                Picture = product.Picture,
                Color = product.Color,

                Quantity = quantity,
                Price = product.Price,
                Discount = product.Discount,
                TotalPrice = quantity * product.Price - quantity * product.Price * product.Discount / 100,
                Description = product.Description
            };
            return View(orderForDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderConfirmVM bindingModel)
        {
                if (this.ModelState.IsValid)
                {
                    string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var user = this._context.Users.SingleOrDefault(u => u.Id == userId);
                    var product = this._context.Products.SingleOrDefault(x => x.Id == bindingModel.ProductId);

                    if (user == null || product == null || product.Quantity < bindingModel.Quantity || bindingModel.Quantity == 0)
                    { return this.RedirectToAction("Index", "Product"); }

                    Order orderForDb = new Order
                    {
                        OrderDate = DateTime.UtcNow,
                        ProductId = bindingModel.ProductId,
                        UserId = userId,
                        Quantity = bindingModel.Quantity,
                        Price = product.Price,
                        Discount = product.Discount,
                        Description = product.Description,
                        Color = product.Color
                    };

                    product.Quantity -= bindingModel.Quantity;
                    this._context.Products.Update(product);
                    this._context.Orders.Add(orderForDb);
                    this._context.SaveChanges();
                }
                return this.RedirectToAction("Index", "Product");
        }
    }
}
