using Household.Budget.Contracts.Data;
using Household.Budget.Infra.Data.Context;
using Household.Budget.Infra.Data.Repositories;

namespace Household.Budget.Infra.Extensions
{
    public static class InfraExtensions
    {
        public static void AddInfra(this IServiceCollection services, IConfiguration config)
        {
            services.AddRavenDb(config);
        }

        private static void AddRavenDb(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<RavenConfig>(config.GetSection(RavenConfig.SectionName));
            services.AddSingleton<IRavenDbContext, RavenDbContext>();
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
        }
    }
}