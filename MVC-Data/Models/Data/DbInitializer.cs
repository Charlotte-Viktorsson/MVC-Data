using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data.Models.Data
{
    public class DbInitializer
    {
        internal static void Initialize(PeopleDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {

            //context.Database.EnsureCreated();
            context.Database.Migrate();

            // seed roles
            if (!context.UserRoles.Any())
            {
                //seed admin role
                //const string ROLE_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
                //IdentityRole adminrole = new IdentityRole
                //{
                //    Id = ROLE_ID,
                //    Name = "Admin",
                //    NormalizedName = "ADMIN"
                //};

                // Saves the role in the underlying AspNetRoles table

                //IdentityResult result = roleManager.CreateAsync(adminrole).Result;
                //if (!result.Succeeded)
                //{
                //    ApplicationException exception = new ApplicationException($"Default role Admin cannot be created");
                //    //logger.LogError(exception, "Default user not created");
                //    throw exception;
                //}
                IdentityResult result = roleManager.CreateAsync(new IdentityRole("Admin")).Result;
                if (!result.Succeeded)
                {
                    ApplicationException exception = new ApplicationException($"Default role Admin cannot be created");
                    throw exception;
                }

                result = roleManager.CreateAsync(new IdentityRole("User")).Result;
                if (!result.Succeeded)
                {
                    ApplicationException exception = new ApplicationException($"Default role User cannot be created");
                    throw exception;
                }

                result = roleManager.CreateAsync(new IdentityRole("SuperAdmin")).Result;
                if (!result.Succeeded)
                {
                    ApplicationException exception = new ApplicationException($"Role SuperAdmin cannot be created");

                    throw exception;
                }
            }

            if (!context.Users.Any())
            {
                //create admin user
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@admin.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    BirthDate = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };
                IdentityResult result = userManager.CreateAsync(user, "Admin1122!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
                else
                {
                    ApplicationException exception = new ApplicationException($"Default user Admin cannot be created");

                    throw exception;
                }

                //create SuperAdmin user
                user = new ApplicationUser
                {
                    UserName = "SuperAdmin",
                    Email = "superadmin@admin.com",
                    FirstName = "Super",
                    LastName = "Admin",
                    BirthDate = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };

                result = userManager.CreateAsync(user, "Super9900!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "SuperAdmin").Wait();
                }
                else
                {
                    ApplicationException exception = new ApplicationException($"Default user SuperAdmin cannot be created");

                    throw exception;
                }
            }

        }
    }
}
