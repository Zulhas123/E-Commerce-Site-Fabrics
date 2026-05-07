using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductTypeService, ProductTypeService>();
        services.AddScoped<ISpacialTagService, SpacialTagService>();
        services.AddScoped<IOrderService, OrderService>();
        return services;
    }
}

