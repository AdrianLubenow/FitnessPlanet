using FitnessPlanet.Abstraction;
using FitnessPlanet.Data;
using FitnessPlanet.Domain;
using System.Collections.Generic;
using System.Linq;

namespace FitnessPlanet.Services
{
    public class ManifacturerService : IManifacturerService
    {
        private readonly ApplicationDbContext _context;
        public ManifacturerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Manifacturer GetManifacturerById(int manifacturerId)
        {
            return _context.Manifacturers.Find(manifacturerId);
        }
        public List<Manifacturer> GetManifacturers()
        {
            List<Manifacturer> manifacturers = _context.Manifacturers.ToList();
            return manifacturers;
        }
        public List<Product> GetProductsByManifacturer(int manifacturerId)
        {
            return _context.Products.Where(x => x.ManifacturerId == manifacturerId).ToList();
        }
    }
}
