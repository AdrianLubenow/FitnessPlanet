using FitnessPlanet.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FitnessPlanet.Models.Product;
using FitnessPlanet.Models.Order;

namespace FitnessPlanet.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Manifacturer> Manifacturers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<FitnessPlanet.Models.Product.ProductCreateVM> ProductCreateVM { get; set; }
        public DbSet<FitnessPlanet.Models.Product.ProductIndexVM> ProductIndexVM { get; set; }
        public DbSet<FitnessPlanet.Models.Product.ProductEditVM> ProductEditVM { get; set; }
        public DbSet<FitnessPlanet.Models.Product.ProductDetailsVM> ProductDetailsVM { get; set; }
        public DbSet<FitnessPlanet.Models.Product.ProductDeleteVM> ProductDeleteVM { get; set; }
        public DbSet<FitnessPlanet.Models.Order.OrderIndexVM> OrderIndexVM { get; set; }
        public DbSet<FitnessPlanet.Models.Order.OrderConfirmVM> OrderConfirmVM { get; set; }
    }
}
