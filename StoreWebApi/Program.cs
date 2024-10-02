
using Microsoft.EntityFrameworkCore;
using StoreWeb.Core;
using StoreWeb.Core.Mapping.Products;
using StoreWeb.Core.Services.Contract;
using StoreWeb.Repository;
using StoreWeb.Repository.Data;
using StoreWeb.Repository.Data.Contexts;
using StoreWeb.Services.Services.Products;

namespace StoreWebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(M => M.AddProfile(new ProductProfile()));


            var app = builder.Build();


            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<StoreDbContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
            }
            catch(Exception ex)
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
