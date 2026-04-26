using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Data.Contracts;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services) =>
        services
            .AddDbContext<UnitOfWork>(options => options.UseInMemoryDatabase("Shop"))
            .AddScoped<IUnitOfWork, UnitOfWork>();
}