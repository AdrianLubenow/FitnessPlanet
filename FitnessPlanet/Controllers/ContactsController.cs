using FitnessPlanet.Data;
using FitnessPlanet.Domain;
using FitnessPlanet.Models.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessPlanet.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(ContactFormVM form)
        {
            if (ModelState.IsValid)
            {
                var message = new ContactMessage
                {
                    Id = form.Id,
                    FirstName = form.FirstName,
                    LastName = form.LastName,
                    Email = form.Email,
                    Message = form.Message,
                    CreatedAt = DateTime.Now
                };

                _context.ContactMessages.Add(message);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Your message has been sent successfully.";

                return RedirectToAction("ContactForm");
            }

            return View(form);
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            var messages = _context.ContactMessages.ToList();
            return View(messages);
        }
        [HttpGet]
        public IActionResult ContactForm()
        {
            var contactForm = new ContactFormVM();
            return View(contactForm);
        }
    }
}
