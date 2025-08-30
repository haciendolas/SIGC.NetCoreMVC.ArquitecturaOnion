using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IRepositories.IUserRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UserRepositories;

namespace SIGC.Infrastructure.ADONET.SQLSERVER
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSIGCInfrastructureADONETSQLSERVER(this IServiceCollection services,
       IConfiguration configuration, string sectionConnectionName)
        {
            services.Configure<AppDbContext>(configuration.GetSection(sectionConnectionName));

            services.AddDependencyInjectionRepository();
            return services;
        }

        private static IServiceCollection AddDependencyInjectionRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserLoginRepository,UserLoginRepository>();
            services.AddScoped<ICategoryChangeStateRepository, CategoryChangeStateRepository>();
            services.AddScoped<ICategoryCreateRepository, CategoryCreateRepository>();
            services.AddScoped<ICategoryGetRepository, CategoryGetRepository>();
            services.AddScoped<ICategoryUpdateRepository, CategoryUpdateRepository>();
            services.AddScoped<ICategoryValidateRepository, CategoryValidateRepository>();
            return services;
        }
    }
}
