using Microsoft.Extensions.DependencyInjection;
using SIGC.ApplicationService.Commons.Mappers.Auth;
using System.Reflection;

namespace SIGC.ApplicationService
{
   public static class DependencyInjection
    {
         public static IServiceCollection AddSIGCCoreApplicationService(this IServiceCollection services)
        {

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });

            services.AddScoped<IAuthMapper, AuthMapper>();
            return services;
        }
    }
}
