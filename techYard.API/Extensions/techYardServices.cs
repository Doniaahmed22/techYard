using techYard.Repository.Interfaces;
using techYard.Repository.Repositories;
using techYard.Service.Services.AccountServices;
using techYard.Service.Services.CategoryServices;
using techYard.Service.Services.featuresServices;
using techYard.Service.Services.ProductImagesServices;
using techYard.Service.Services.productsServices;
using techYard.Service.Services.profileServices;

namespace techYard.API.Extensions
{
    public static class techYardServices
    {
        public static IServiceCollection AddTechYardServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductServices, ProductServices>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryServices, CategoryServices>();
            services.AddScoped<IProductFeatureServices, ProductFeatureServices>();
            services.AddScoped<IProductDetailsImagesServices, ProductDetailsImagesServices>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }

    }
}
