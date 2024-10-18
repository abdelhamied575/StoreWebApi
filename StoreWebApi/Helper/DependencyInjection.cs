using Microsoft.EntityFrameworkCore;
using StoreWeb.Core.Services.Contract;
using StoreWeb.Core;
using StoreWeb.Repository;
using StoreWeb.Repository.Data.Contexts;
using StoreWeb.Services.Services.Products;
using StoreWeb.Core.Mapping.Products;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.Errors;

namespace StoreWebApi.Helper
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependency(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddBuiltInService();
            services.AddBuiltInService();
            services.AddSwiggerService();
            services.AddDbContextService(configuration);
            services.AddUserDefinendService();
            services.AddAutoMapperService(configuration);
            services.ConfigureInValidModelStateResponseService(configuration);

            return services;

        }

        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers();

            return services;

        }


        private static IServiceCollection AddSwiggerService(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;

        }

        private static IServiceCollection AddDbContextService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            return services;

        }

        private static IServiceCollection AddUserDefinendService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;

        }

        private static IServiceCollection AddAutoMapperService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));

            return services;

        }
        
        private static IServiceCollection ConfigureInValidModelStateResponseService(this IServiceCollection services,IConfiguration configuration)
        {


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext =>
                {
                    var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                            .SelectMany(P => P.Value.Errors)
                                            .Select(E => E.ErrorMessage)
                                            .ToArray();

                    var response = new ApiValidationErrorResponse()
                    {

                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                    //return BadRequest(response); // Error due to Not Inhert From ApiError Class

                });
            });


            return services;

        }

            


    }
}
