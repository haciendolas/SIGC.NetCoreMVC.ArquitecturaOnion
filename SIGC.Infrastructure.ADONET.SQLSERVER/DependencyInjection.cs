using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SIGC.DomainService.IRepositories.IActiveIngredientRepositories;
using SIGC.DomainService.IRepositories.IAttributeValueRepositories;
using SIGC.DomainService.IRepositories.IAuthRepositories;
using SIGC.DomainService.IRepositories.IBrandRepositories;
using SIGC.DomainService.IRepositories.ICatalogRepositories;
using SIGC.DomainService.IRepositories.ICatalogTypeRepositories;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IRepositories.ICompanyRegisterRepositories;
using SIGC.DomainService.IRepositories.ICompanyRepositories;
using SIGC.DomainService.IRepositories.IConstantRepositories;
using SIGC.DomainService.IRepositories.IEstablishmentRepositories;
using SIGC.DomainService.IRepositories.IManufacturerRepositories;
using SIGC.DomainService.IRepositories.IPageCompanyRepositories;
using SIGC.DomainService.IRepositories.IPageRepositories;
using SIGC.DomainService.IRepositories.IPharmaceuticalFormRepositories;
using SIGC.DomainService.IRepositories.IPrescriptionTypeRepositories;
using SIGC.DomainService.IRepositories.IPresentationRepositories;
using SIGC.DomainService.IRepositories.IPriceTypeRepositories;
using SIGC.DomainService.IRepositories.IRolePermissionRepositories;
using SIGC.DomainService.IRepositories.IRoleRepositories;
using SIGC.DomainService.IRepositories.ITherapeuticActionRepositories;
using SIGC.DomainService.IRepositories.ITokenRepositories;
using SIGC.DomainService.IRepositories.IUbigeoRepositories;
using SIGC.DomainService.IRepositories.IUnitMeasureRepositories;
using SIGC.DomainService.IRepositories.IUserCompanyRepositories;
using SIGC.DomainService.IRepositories.IUserRepositories;
using SIGC.DomainService.IRepositories.IUserRoleRepositories;
using SIGC.DomainService.IRepositories.IWarehouseRepositories;
using SIGC.DomainService.IRepositories.UserRoleRepositories;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.ADONET.SQLSERVER.AppDBContext;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.ActiveIngredientRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.AttributeValueRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.AuthRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.BrandRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CatalogTypeRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRegisterRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.ConstantRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.EstablishmentRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.ManufacturerRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.PageCompanyRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.PageRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.PharmaceuticalFormRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.PrescriptionTypeRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.PresentationRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.PriceTypeRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.RolePermissionRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.RoleRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.TherapeuticActionRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.TokenRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UbigeoRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UnitMeasureRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UserCompanyRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UserRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.UserRoleRepositories;
using SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.WarehouseRepositories;
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
            services.AddScoped<ICategoryVerifyNameRepository, CategoryVerifyNameRepository>();
            services.AddScoped<ICategoryPaginationRepository, CategoryPaginationRepository>();
            services.AddScoped<ICategoryListRepository, CategoryListRepository>();

            services.AddScoped<IRolePermissionListRepository, RolePermissionListRepository>();
            services.AddScoped<IRolePermissionCreateRepository, RolePermissionCreateRepository>();
            services.AddScoped<IRolePermissionDeleteRepository, RolePermissionDeleteRepository>();

            services.AddScoped<IRolePaginationRepository, RolePaginationRepository>();
            services.AddScoped<IRoleChangeStateRepository, RoleChangeStateRepository>();
            services.AddScoped<IRoleCreateRepository, RoleCreateRepository>();
            services.AddScoped<IRoleUpdateRepository, RoleUpdateRepository>();
            services.AddScoped<IRoleVerifyCodeAndNameRepository, RoleVerifyCodeAndNameRepository>();
            services.AddScoped<IRoleGetRepository, RoleGetRepository>();
            services.AddScoped<IRoleListRepository, RoleListRepository>();

            services.AddScoped<IPageListRepository, PageListRepository>();

            services.AddScoped<IPageCompanyListRepository, PageCompanyListRepository>();
            services.AddScoped<IPageCompanyCreateNotExistsRepository, PageCompanyCreateNotExistsRepository>();
            services.AddScoped<IPageCompanyCreateRepository, PageCompanyCreateRepository>();
            services.AddScoped<IPageCompanyDeleteRepository, PageCompanyDeleteRepository>();

            services.AddScoped<ICompanyListRepository, CompanyListRepository>();
            services.AddScoped<ICompanyCreateRepository, CompanyCreateRepository>();
            services.AddScoped<ICompanyUpdateRepository, CompanyUpdateRepository>();
            services.AddScoped<ICompanyGetRepository, CompanyGetRepository>();
            services.AddScoped<ICompanyChangeStateRepository, CompanyChangeStateRepository>();
            services.AddScoped<ICompanyPaginationRepository, CompanyPaginationRepository>();
            services.AddScoped<ICompanyVerifyDocumentNumberAndSocialReasonRepository, CompanyVerifyDocumentNumberAndSocialReasonRepository>();

            services.AddScoped<ICompanyRegisterCreateRepository, CompanyRegisterCreateRepository>();

            services.AddScoped<IUbigeoListSearchRepository, UbigeoListSearchRepository>();
            services.AddScoped<IUbigeoListByUbigeoClassRepository, UbigeoListByUbigeoClassRepository>();
            services.AddScoped<IUbigeoListByClassAndCodeAndLenCodeRepository, UbigeoListByClassAndCodeAndLenCodeRepository>();

            services.AddScoped<IConstantListRepository, ConstantListRepository>();

            services.AddScoped<IUserCreateRepository, UserCreateRepository>();
            services.AddScoped<IUserVerifyNameAndMailRepository, UserVerifyNameAndMailRepository>();
            services.AddScoped<IUserPaginationRepository, UserPaginationRepository>();
            services.AddScoped<IUserUpdateRepository, UserUpdateRepository>();

            services.AddScoped<IUserCompanyCreateRepository, UserCompanyCreateRepository>();
            services.AddScoped<IUserCompanyChangeStateRepository, UserCompanyChangeStateRepository>();
            services.AddScoped<IUserCompanyGetRepository, UserCompanyGetRepository>();
            services.AddScoped<IUserCompanyUpdateRepository, UserCompanyUpdateRepository>();

            services.AddScoped<IUserRoleCreateRepository, UserRoleCreateRepository>();
            services.AddScoped<IUserRoleDeleteRepository, UserRoleDeleteRepository>();

            services.AddScoped<IEstablishmentListRepository,EstablishmentListRepository>();
            services.AddScoped<IEstablishmentPaginationRepository, EstablishmentPaginationRepository>();
            services.AddScoped<IEstablishmentCreateRepository, EstablishmentCreateRepository>();
            services.AddScoped<IEstablishmentGetRepository, EstablishmentGetRepository>();
            services.AddScoped<IEstablishmentChangeStateRepository, EstablishmentChangeStateRepository>();
            services.AddScoped<IEstablishmentUpdateRepository, EstablishmentUpdateRepository>();

            services.AddScoped<IWarehousePaginationRepository, WarehousePaginationRepository>();
            services.AddScoped<IWarehouseCreateRepository, WarehouseCreateRepository>();
            services.AddScoped<IWarehouseGetRepository, WarehouseGetRepository>();
            services.AddScoped<IWarehouseUpdateRepository, WarehouseUpdateRepository>();
            services.AddScoped<IWarehouseChangeStateRepository, WarehouseChangeStateRepository>();

            services.AddScoped<IBrandListRepository, BrandListRepository>();

            services.AddScoped<IManufacturerListRepository, ManufacturerListRepository>();

            services.AddScoped<ICatalogTypeListRepository, CatalogTypeListRepository>();

            services.AddScoped<ICatalogPaginationRepository, CatalogPaginationRepository>();

            services.AddScoped<IPrescriptionTypeListRepository, PrescriptionTypeListRepository>();

            services.AddScoped<IAttributeValueListRepository, AttributeValueListRepository>();

            services.AddScoped<IActiveIngredientListRepository, ActiveIngredientListRepository>();

            services.AddScoped<IPharmaceuticalFormListRepository, PharmaceuticalFormListRepository>();

            services.AddScoped<ITherapeuticActionListRepository, TherapeuticActionListRepository>();

            services.AddScoped<IPresentationListRepository, PresentationListRepository>();

            services.AddScoped<IPriceTypeListRepository, PriceTypeListRepository>();

            services.AddScoped<IUnitMeasureListRepository, UnitMeasureListRepository>();
            return services;
        }
    }
}
