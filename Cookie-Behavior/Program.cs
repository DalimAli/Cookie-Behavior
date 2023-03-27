using Cookie_Behavior.Data;
using Cookie_Behavior.Models.IdentityInfos;
using Cookie_Behavior.Services.Implementation;
using Cookie_Behavior.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cookie_Behavior
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("CookieConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

    //        builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    //.AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
            })
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages()
               .AddRazorPagesOptions(options =>
               {
                   options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                   options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                   options.Conventions.AuthorizeAreaPage("Identity", "/Account/Register");
               });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                //AddAuthentication(options =>
                //{
                //    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                //})
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.SlidingExpiration = true;
                options.Cookie.Name = "Cookie_Behavior";
                //options.AccessDeniedPath = "/Forbidden/";
                //options.LoginPath = $"/Identity/Account/Login";
                //options.LogoutPath = $"/Identity/Account/Logout";
                //options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}