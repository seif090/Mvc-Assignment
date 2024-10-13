using Microsoft.EntityFrameworkCore;
using Company.Data.context;
using Company.Data.Entities;
using Company.repository.interfaces;
using Company.repository.repositories;
using Company.Services.Services;
using Company.Services.Interfaces;
using Company.Services.mapping;
using Microsoft.AspNetCore.Identity;

namespace company.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //AddScoped
            //AddTransient
            //AddSingleTone
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmplyeeService>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new DepartmentProfile()));
            builder.Services.AddAutoMapper(x => x.AddProfile(new EmployeeProfiler()));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.Password.RequiredUniqueChars = 2;
                config.Password.RequireDigit = true;
                config.Password.RequiredLength = 10;
                config.Password.RequireUppercase = true;
                config.Password.RequireLowercase = true;
                config.Password.RequireNonAlphanumeric = true;
                config.User.RequireUniqueEmail = true;
                config.Lockout.AllowedForNewUsers = true;//
                config.Lockout.MaxFailedAccessAttempts = 3;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
            }).AddEntityFrameworkStores<CompanyDbContext>()
            .AddDefaultTokenProviders();
            var app = builder.Build();


            //builder.Services.ConfigureApplicationCookie(option =>
            //{
            //    option.Cookie.HttpOnly = true;
            //    option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //    option.SlidingExpiration = true;
            //    option.LoginPath = "Account/Login";
            //    option.LogoutPath = "Account/Logout";
            //    option.AccessDeniedPath = "Account/AccessDenied";
            //    option.Cookie.Name = "any name";
            //    option.Cookie.SecurePolicy= CookieSecurePolicy.Always;
            //    option.Cookie.SameSite= SameSiteMode.Strict;
            //});


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=SignUp}");

            app.Run();
        }
    }
}