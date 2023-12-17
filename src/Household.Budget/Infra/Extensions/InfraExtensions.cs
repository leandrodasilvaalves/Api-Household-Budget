using Household.Budget.Contracts.Data;
using Household.Budget.Contracts.Models.Identity;
using Household.Budget.Infra.Data.Context;
using Household.Budget.Infra.Data.Repositories;

using Raven.DependencyInjection;
using Raven.Identity;

namespace Household.Budget.Infra.Extensions
{
    public static class InfraExtensions
    {
        public static void AddInfra(this IServiceCollection services, IConfiguration config)
        {
            services.AddRavenDb(config);
            services.AddRavenIdentity(config);
        }

        private static void AddRavenDb(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<RavenConfig>(config.GetSection(RavenConfig.SectionName));
            services.AddSingleton<IRavenDbContext, RavenDbContext>();
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
        }

        private static void AddRavenIdentity(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddRavenDbDocStore()
                .AddRavenDbAsyncSession()
                .AddIdentity<AppIdentityModel, IdentityRole>()
                .AddRavenDbIdentityStores<AppIdentityModel, IdentityRole>();

            services.Configure<Microsoft.AspNetCore.Identity.IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequiredUniqueChars = 1;
            });
        }
    }
}