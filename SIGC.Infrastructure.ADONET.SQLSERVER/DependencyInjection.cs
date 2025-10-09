using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIGC.DomainService.IRepositories.IAuthRepositories;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IRepositories.ICompanyRepositories;
using SIGC.DomainService.IRepositories.IPageCompanyRepositories;
using SIGC.DomainService.IRepositories.IPageRepositories;
using SIGC.DomainService.IRepositories.IRolePermissionRepositories;
using SIGC.DomainService.IRepositories.IRoleRepositories;
using SIGC.DomainService.IRepositories.ITokenRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.AuthRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.PageCompanyRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.PageRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.RolePermissionRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.RoleRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.TokenRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Transactions;

namespace SIGC.Infrastructure.ADONET.SQLSERVER
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSIGCInfrastructureADONETSQLSERVER(this IServiceCollection services, IConfiguration configuration, string sectionConnectionName)
        {
            services.Configure<AppDbContext>(configuration.GetSection(sectionConnectionName));
            services.AddScoped<ITransactionAccessor, TransactionAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

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

            services.AddScoped<IRolePermissionListRepository, RolePermissionListRepository>();
            services.AddScoped<IRolePermissionCreateRepository, RolePermissionCreateRepository>();
            services.AddScoped<IRolePermissionDeleteRepository, RolePermissionDeleteRepository>();

            services.AddScoped<IRolePaginationRepository, RolePaginationRepository>();
            services.AddScoped<IRoleChangeStateRepository, RoleChangeStateRepository>();
            services.AddScoped<IRoleCreateRepository, RoleCreateRepository>();
            services.AddScoped<IRoleUpdateRepository, RoleUpdateRepository>();
            services.AddScoped<IRoleVerifyCodeAndNameRepository, RoleVerifyCodeAndNameRepository>();
            services.AddScoped<IRoleGetRepository, RoleGetRepository>();

            services.AddScoped<IPageListRepository, PageListRepository>();

            services.AddScoped<IPageCompanyListRepository, PageCompanyListRepository>();
            services.AddScoped<IPageCompanyCreateNotExistsRepository, PageCompanyCreateNotExistsRepository>();

            services.AddScoped<ICompanyListRepository, CompanyListRepository>();
           
            return services;
        }
    }
}
