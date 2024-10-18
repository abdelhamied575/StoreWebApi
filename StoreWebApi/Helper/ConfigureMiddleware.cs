﻿using StoreWeb.Repository.Data.Contexts;
using StoreWeb.Repository.Data;
using StoreWebApi.MiddleWares;
using Microsoft.EntityFrameworkCore;

namespace StoreWebApi.Helper
{
    public static class ConfigureMiddleware
    {

        public static async Task<WebApplication> ConfigureMiddlewareAsync(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<StoreDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context); 
            }
            catch (Exception ex)
            {
                var loger = loggerFactory.CreateLogger<Program>();
                loger.LogError(ex.Message);
                loger.LogError(ex, "There are Problems during apply migrations");
            }

            //StoreDbContext context = new StoreDbContext();
            //context.Database.MigrateAsync(); // Update-DataBase

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleWare>(); // Configure User-Defined MiddleWare

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.MapControllers();


            return app;
        }


    }
}
