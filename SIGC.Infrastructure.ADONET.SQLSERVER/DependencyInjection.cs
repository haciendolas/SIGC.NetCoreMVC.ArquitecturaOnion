using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIGC.DomainService.IRepositories.IAuthRepositories;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IRepositories.ITokenRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.AuthRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.TokenRepositories;

namespace SIGC.Infrastructure.ADONET.SQLSERVER
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSIGCInfrastructureADONETSQLSERVER(this IServiceCollection services, IConfiguration configuration, string sectionConnectionName)
        {
            services.Configure<AppDbContext>(configuration.GetSection(sectionConnectionName));

            services.AddDependencyInjectionRepository();
            return services;
        }

        private static IServiceCollection AddDependencyInjectionRepository(this IServiceCollection services)
        {
            services.AddScoped<IAuthLoginRepository,AuthLoginRepository>();
            services.AddScoped<IAuthGetRepository, AuthGetRepository>();

            services.AddScoped<ITokenCreateRepository, TokenCreateRepository>();
            services.AddScoped<ITokenGetExpirationRepository, TokenGetExpirationRepository>();
            services.AddScoped<ITokenUpdateRevocationRepository, TokenUpdateRevocationRepository>();

            services.AddScoped<ICategoryChangeStateRepository, CategoryChangeStateRepository>();
            services.AddScoped<ICategoryCreateRepository, CategoryCreateRepository>();
            services.AddScoped<ICategoryGetRepository, CategoryGetRepository>();
            services.AddScoped<ICategoryUpdateRepository, CategoryUpdateRepository>();
            services.AddScoped<ICategoryValidateRepository, CategoryValidateRepository>();

            return services;
        }
    }
}
