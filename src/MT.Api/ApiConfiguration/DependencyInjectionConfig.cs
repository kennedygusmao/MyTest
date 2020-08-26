
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MT.Data.Context;
using MT.Data.Repository;
using MT.Domain.Interfaces.Repository;
using MT.Domain.Interfaces.Service;
using MT.Service.Interface;
using MT.Service.Notificacoes;
using MT.Service.Service;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MT.API.ApiConfiguration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<BContext>();

            ///Repository
            services.AddScoped<ICaminhaoRepository, CaminhaoRepository>();
            services.AddScoped<IModeloRepository, ModeloRepository>();
            ///Service
            services.AddScoped<ICaminhaoService, CaminhaoService>();
            services.AddScoped<IModeloService, ModeloService>();

            services.AddScoped<INotificador, Notificador>();         
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
