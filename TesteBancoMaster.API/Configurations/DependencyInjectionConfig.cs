using TesteBancoMaster.API.Services;
using TesteBancoMaster.API.Services.Interfaces;
using TesteBancoMaster.Infra.Data;
using TesteBancoMaster.Infra.Repositories;

namespace TesteBancoMaster.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddRegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IViagemService, ViagemService>();
            services.AddScoped<IViagemRepository, ViagemRepository>();
            services.AddScoped<ViagensContext>();
        }
    }
}
