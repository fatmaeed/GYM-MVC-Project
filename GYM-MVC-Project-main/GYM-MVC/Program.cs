using GYM.Domain.Entities;
using GYM_MVC.Core.Helper;
using GYM_MVC.Core.IRepositories;
using GYM_MVC.Core.IUnitOfWorks;
using GYM_MVC.Core.MapperConf;
using GYM_MVC.Data.Data;
using GYM_MVC.Data.DBHelper;
using GYM_MVC.Data.Repositories;
using GYM_MVC.Data.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace GYM_MVC {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(
                (options) => {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequiredLength = 6;
                }
                ).AddEntityFrameworkStores<GYMContext>().AddDefaultTokenProviders();
            builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddDbContext<GYMContext>(options => options.UseLazyLoadingProxies().UseSqlServer(
               builder.Configuration.GetConnectionString("cs")));
            builder.Services.AddAutoMapper(typeof(MapperConfig));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ITrainerRepo, TrainerRepo>();
            builder.Services.AddScoped<IExcerciseRepo, ExcerciseRepo>();
            builder.Services.AddScoped<IMemberRepo, MemberRepo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production
                // scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            //app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            // .WithStaticAssets();

            DBInitializer.Seed(app);
            app.Run();
        }
    }
}