using CoreCommerce.API.Helper;
using CoreCommerce.Core.Interfaces;
using CoreCommerce.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace CoreCommerce.API.Extensions;

public static class AppServicesExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddDbContext<StoreContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });

        return services;
    }


}
