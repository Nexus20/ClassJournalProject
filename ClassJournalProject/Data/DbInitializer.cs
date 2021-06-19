using System.Threading.Tasks;
using ClassJournalProject.Models;
using Microsoft.AspNetCore.Identity;

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


    }
}
