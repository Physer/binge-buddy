using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Persistence;

public static class DependencyRegistrator
{
    public static void RegisterPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        //Debug.WriteLine(configuration.GetConnectionString("BingeDatabase"));
        services.AddDbContext<BingeContext>(options => options.UseSqlServer(configuration.GetConnectionString("BingeDatabase")));
    }
}
