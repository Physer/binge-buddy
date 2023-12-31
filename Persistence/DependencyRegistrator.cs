﻿using Application.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("IntegrationTests")]
namespace Persistence;

public static class DependencyRegistrator
{
    public static void RegisterPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BingeContext>(options => options.UseSqlServer(configuration.GetConnectionString("BingeDatabase")));
        services.AddScoped<IRepository, BingeRepository>();
    }
}
