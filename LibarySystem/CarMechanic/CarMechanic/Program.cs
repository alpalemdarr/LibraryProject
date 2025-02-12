using CarMechanic.Data;
using CarMechanic.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarMechanic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DataContext>(options =>
            {
                var config = builder.Configuration;
                var connectionstring = config.GetConnectionString("database");
                options.UseSqlServer(connectionstring);
            });
            builder.Services.AddScoped<IMalfunctionRepository, MalfunctionRepository>();
            builder.Services.AddScoped<IServiceReportRepository, ServiceReportRepository>();

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

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
