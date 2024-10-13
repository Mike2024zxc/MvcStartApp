using Microsoft.Extensions.Configuration;
using MvcStartApp.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcStartApp.Data.Models.Db.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connection = builder.Configuration.GetConnectionString("DefaultConnection");
        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);
        builder.Services.AddSingleton<IBlogRepository, BlogRepository>();
        builder.Services.AddSingleton<IRequestRepository, RequestRepository>();


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
        // ���������� ����������� � �������������� �� �������������� ����
        app.UseMiddleware<LoggingMiddleware>();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}