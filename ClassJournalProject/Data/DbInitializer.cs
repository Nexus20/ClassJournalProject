using System.Threading.Tasks;
using ClassJournalProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace ClassJournalProject.Data {

    public static class DbInitializer {

        public static async Task InitializeRolesAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
            const string adminEmail = "jack.gelder0804@gmail.com";
            const string password = "ABCabc123_";

            if (await roleManager.FindByNameAsync("admin") == null) {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("moderator") == null) {
                await roleManager.CreateAsync(new IdentityRole("moderator"));
            }
            if (await roleManager.FindByNameAsync("teacher") == null) {
                await roleManager.CreateAsync(new IdentityRole("teacher"));
            }
            if (await roleManager.FindByNameAsync("student") == null) {
                await roleManager.CreateAsync(new IdentityRole("student"));
            }

            if (await userManager.FindByEmailAsync(adminEmail) == null) {
                var admin = new User() {
                    UserName = "admin",
                    Email = adminEmail
                };
                var result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }

        public static void Initialize(ApplicationDbContext context) {

            context.Database.EnsureCreated();

            if (context.Specialties.Any()) {
                return;
            }

            var specialties = new Specialty[] {
                new() {Id = 121, Name = "IPZ"},
                new() {Id = 122, Name = "KN"},
                new() {Id = 123, Name = "KIU"},
            };

            foreach (var specialty in specialties) {
                context.Specialties.Add(specialty);
            }

            context.SaveChanges();

            var subjects = new Subject[] {
                new() {Name = "Maths"},
                new() {Name = "Physics"},
                new() {Name = "C++"},
                new() {Name = "Java"},
                new() {Name = "PHP"},
                new() {Name = "JS"},
                new() {Name = "Hypertext and hypermedia"},
                new() {Name = "Algorithms and data structures"},
                new() {Name = "English"},
            };

            foreach (var subject in subjects) {
                context.Subjects.Add(subject);
            }

            context.SaveChanges();

            var groups = new Group[] {
                new() {Number = 1, SpecialtyId = 121, Year = 2020},
                new() {Number = 1, SpecialtyId = 121, Year = 2020},
                new() {Number = 2, SpecialtyId = 122, Year = 2020},
                new() {Number = 2, SpecialtyId = 122, Year = 2020},
                new() {Number = 1, SpecialtyId = 123, Year = 2020},
            };

            foreach (var group in groups) {
                context.Groups.Add(group);
            }

            context.SaveChanges();
        }


    }
}
