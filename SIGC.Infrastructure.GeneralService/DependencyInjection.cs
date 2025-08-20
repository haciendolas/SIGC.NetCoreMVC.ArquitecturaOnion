using Microsoft.Extensions.DependencyInjection;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.GeneralService.Services;

namespace SIGC.Infrastructure.GeneralService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSIGCInfrastructureGeneralService(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentSessionService, CurrentSessionService>();
            services.AddScoped<IGenerateJWTToken,GenerateJWTToken>();
            services.AddScoped<IMessageService, MessageService>();

            return services;
        }
    }
}