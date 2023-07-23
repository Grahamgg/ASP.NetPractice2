using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using NathansCRUDWebsite.Models;



namespace NathansCRUDWebsite
{

    public class Program
    {

        public static void Main(string[] args)
        {
       
            string connectionKey = File.ReadAllText("appsettings.json");
            JObject jsonObject = JObject.Parse(connectionKey);
            JToken token = jsonObject["ConnectionStrings"]["bestbuy"];
            string connString = token.ToString();
            ProductRepo.connectionString = connString;
            

            CreateHostBuilder(args).Build().Run();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDbConnection>((s) =>
            {
                IDbConnection conn = new MySqlConnection(builder.Configuration.GetConnectionString("bestbuy"));
                conn.Open();
                return conn;

            });

            builder.Services.AddTransient<IProductRepository, ProductRepository>();

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


            app.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
