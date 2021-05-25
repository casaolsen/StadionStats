using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StadionStats.Data
{
    public static class UserAndRoleDataInitializer
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("gitte@stadionstats.dk").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "gitte@stadionstats.dk";
                user.Email = "gitte@stadionstats.dk";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Bruger").Wait();
                }
            }


            if (userManager.FindByEmailAsync("thomas@stadionstats.dk").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "thomas@stadionstats.dk";
                user.Email = "thomas@stadionstats.dk";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByEmailAsync("gitte-api@stadionstats.dk").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "gitte-api@stadionstats.dk";
                user.Email = "gitte-api@stadionstats.dk";

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "App").Wait();
                }
            }


        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Kundeservice").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Bruger";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("App").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "App";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
