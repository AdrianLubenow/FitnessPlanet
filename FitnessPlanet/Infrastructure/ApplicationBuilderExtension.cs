using FitnessPlanet.Data;
using FitnessPlanet.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessPlanet.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedAdministrator(services);

            var dataCategory = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories(dataCategory);

            var dataBrand = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedManifacturers(dataBrand);

            return app;
        }
        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Administrator", "Client" };
            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = "admin";
                user.LastName = "admin";
                user.PhoneNum = "0888888888";
                user.UserName = "admin";
                user.Email = "admin@admin.com";

                var result = await userManager.CreateAsync(user, "admin1");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
        private static void SeedCategories(ApplicationDbContext dataCategory)
        {
            if (dataCategory.Categories.Any())
            {
                return;
            }
            dataCategory.Categories.AddRange(new[]
            {
                new Category {CategoryName="Cycling Ergometer"},
                new Category {CategoryName="Exercise Machines"},
                new Category {CategoryName="Treadmills"},
                new Category {CategoryName="Accessories"},
                new Category {CategoryName="Combined"}
            });
            dataCategory.SaveChanges();
        }

        private static void SeedManifacturers(ApplicationDbContext dataBrand)
        {
            if (dataBrand.Manifacturers.Any())
            {
                return;
            }
            dataBrand.Manifacturers.AddRange(new[]
            {
                new Manifacturer {ManifacturerName="Runner"},
                new Manifacturer {ManifacturerName="Sportsmith"},
                new Manifacturer {ManifacturerName="ProForm"},
                new Manifacturer {ManifacturerName="TRX"},
                new Manifacturer {ManifacturerName="Ruse-Sport"}
            });
            dataBrand.SaveChanges();
        }
    }
}
