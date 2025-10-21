using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GYM.Domain.Entities;
using GYM_MVC.Data.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GYM_MVC.Data.DBHelper
{
    public  static class DBInitializer
    {
        public static async void Seed(IApplicationBuilder app)
        {
            GYMContext context = app.ApplicationServices.CreateScope()
                                          .ServiceProvider
                                          .GetRequiredService<GYMContext>();

            var userManager = app.ApplicationServices.CreateScope()
                                            .ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = app.ApplicationServices.CreateScope()
                                            .ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            string mainAdminpassword = "Abdo12345$";
            ApplicationUser mainAdmin = new ApplicationUser
            {
                UserName = "Abdo_Dorgham",
                Email = "abdodorgham257@gmail.com",
                PhoneNumber = "01068117863"
            };

            List<IdentityRole<int>> roles = new List<IdentityRole<int>>()
            {
                new IdentityRole<int>() { Name = "Admin"} ,
                new IdentityRole< int >() { Name = "Member"} ,
                new IdentityRole < int >() { Name = "Trainer"} ,
            };

            if (!userManager.Users.Any())
            {
                await userManager.CreateAsync(mainAdmin, mainAdminpassword);

            }

            if (!roleManager.Roles.Any())
            {
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
                await userManager.AddToRoleAsync(mainAdmin, roles[0].Name);
            }
        }

    }
}
