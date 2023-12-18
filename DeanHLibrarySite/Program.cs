using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DeanHLibrarySite.Data;
using DeanHLibrarySite.Models;
using Microsoft.AspNetCore.Identity;
using DeanHLibrarySite.Pages;

namespace DeanHLibrarySite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DeanHLibrarySiteContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DeanHLibrarySiteContext") ?? throw new InvalidOperationException("Connection string 'DeanHLibrarySiteContext' not found.")));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DeanHLibrarySiteContext>();

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<UserManager<ApplicationUser>>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                SeedBookTable.Initialize(services);
                SeedReservations.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}