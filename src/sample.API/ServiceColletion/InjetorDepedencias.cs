using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sample.Application.DTO;
using sample.Application.Repositories;
using sample.Application.Repositories.Interfaces;
using sample.Infrastructure;
using sample.Infrastructure.Services;
using System;
using static sample.Application.DTO.Colaborador;

namespace sample.API.Configuration
{
    public static class InjetorDepedencias
    {
        /// <summary>
        /// Configuração das variáveis de ambiente da aplicação (appsettings.json)
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AdicionarConfiguracoesAplicacao(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            return services
                .Configure<ConfiguracoesAplicacao>(o =>
                {
                    o.UrlDummy = configuration.GetValue<string>("AppConfiguration:UrlDummy");
                }
            );
        }

        /// <summary>
        /// Registrar os componentes da aplicação no container de injeção de dependências.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AdicionarComponentesAplicacao(this IServiceCollection services)
        {
            ConfigurarValidator(services);
            ConfigurarRepositories(services);
            ConfigurarServices(services);

            return services;
        }

        /// <summary>
        /// Configuração das validações dos corpos das requisições dos DTO's.
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigurarValidator(IServiceCollection services)
        {
            services
                .AddTransient<IValidator<Colaborador>, ColaboradorValidator>();
        }

        /// <summary>
        /// Configuração dos repositories da aplicação.
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigurarRepositories(IServiceCollection services)
        {
            services
                .AddTransient<IColaboradorRepository, ColaboradorRepository>();
        }

        /// <summary>
        /// Configuração dos services da aplicação.
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigurarServices(IServiceCollection services)
        {
            services
                .AddTransient<IColaboradorService, ColaboradorService>();
        }
    }
}