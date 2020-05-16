using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using sample.API.Configuration;

namespace sample.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Configura componentes da aplicação
            services.AdicionarComponentesAplicacao();

            //Configurações da aplicação
            services.AdicionarConfiguracoesAplicacao(Configuration);

            //Configura Swagger
            if (!Environment.IsProduction())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Sample API",
                        Description = "API para servir de modelo para as demais aplicações",
                        Version = "v1"
                    });
                });
            }

            //Configura fluent validation para validação dos corpos da requisições com base no DTO. 
            services
               .AddMvc()
               .AddFluentValidation(
                   fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>()
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (!env.IsProduction())
            {
                app.UseSwagger();

                app.UseSwaggerUI(swagger =>
                {
                    swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample API V1");
                    swagger.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}