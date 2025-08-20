using Microsoft.Extensions.DependencyInjection;
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

            return services;
        }
    }
}
