using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenSim.Portal.Model;

namespace OpenSim.Portal.Startup
{
    public static class DatabaseSetup
    {
        public static void AddContentDatabase(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment env) => 
            services.AddDatabase<PortalDbContext>(configuration, env, "ContentConnectionString");

        public static void AddIdentityDatabase(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment env)
        {
            //services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("OpenSim.Portal"));
            services.AddDatabase<UserDbContext>(configuration, env, "IdentityConnectionString");
        }

        private static void AddDatabase<TContext>(this IServiceCollection services, 
            IConfiguration configuration, IHostingEnvironment env, string connectionStringId)
            where TContext : DbContext
        {
            var host = env.IsDevelopment() ? "Local" : "Docker";
            var connectionString = configuration[$"Data:{host}:{connectionStringId}"];

            Console.WriteLine($"{connectionStringId}: {connectionString}");

            services.AddDbContext<TContext>(builder => builder.UseSqlServer(connectionString));
        }
    }
}