using InsuranceProviderManagementSystem.Models;
using InsuranceProviderManagementSystem.Repository;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Serilog;
using InsuranceProviderManagementSystem.CustomMiddleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using InsuranceProviderManagementSystem.Services;
using Hangfire;
using System.Web.Mvc;

namespace InsuranceProviderManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = new LoggerConfiguration()
            .WriteTo.File("Log/logs.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger(); // Specify the log file name and rolling policy

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<InsuranceProviderManagementSystemContext>(options => options.UseSqlServer("Server=DESKTOP-JQ4K9V9\\SQLEXPRESS;Database=InsuranceProviderManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;"));

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.WriteIndented = true; // Optional: for pretty printing
                });
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(2); // Set the timeout
                options.Cookie.HttpOnly = true; // Makes the session cookie accessible only by the server
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<EmailService>();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login";
                    //options.AccessDeniedPath = "/Account/AccessDenied";
                });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();

            builder.Host.UseSerilog(logger);

            builder.Services.AddLogging(logging => logging.AddSerilog(logger, dispose: true));
            //Adding CORS Configuration
            builder.Services.AddCors(
                options =>
                {
                    options.AddPolicy("AllowSpecificOrigin", builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
                }
            );

            //builder.Services.AddHangfire(config =>
            //config.UseSqlServerStorage("SQLServerConnection")); // Use your database connection string
            //builder.Services.AddHangfireServer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.MapControllers();

            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}
