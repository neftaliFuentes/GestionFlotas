
using GestionFlotas.dataaccess;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace GestionFlotas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

			builder.Services.AddDbContextFactory<FlotasContext>(opt => opt.UseSqlServer(
				   builder.Configuration.GetConnectionString("FlotasConnection"),
				   sqlServerOptions => sqlServerOptions.CommandTimeout(120)));

			builder.WebHost.UseWebRoot("wwwroot");
            builder.WebHost.UseStaticWebAssets();

			builder.Services.AddBlazorBootstrap();
			builder.Services.AddRadzenComponents();

			var app = builder.Build();

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

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
