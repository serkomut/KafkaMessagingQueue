using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using KafkaMessagingQueue.ReportApi.Application.Persistence;
using KafkaMessagingQueue.ReportApi.Core;
using System.Linq;
using System.Reflection;

namespace KafkaMessagingQueue.ReportApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(x =>
            {
                var connectionString = Configuration.GetConnectionString("ReportContext");
                var type = Configuration.GetValue<string>("DatabaseConfigurationType");
                return new ContextConfiguration
                {
                    ConnectionString = connectionString,
                    Type = type
                };
            });
            services.AddDbContext<ReportContext>();
            services.RegisterMassTransit();
            services.AddControllers();
            services.AddCustomSwagger();
            var assemblies = GetAssemblies();
            services.AddMediatR(assemblies);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Queue V1");
                c.DocumentTitle = "Report";
            });
        }

        private static Assembly[] GetAssemblies()
        {
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            return (from library in dependencies
                    where IsCandidateComplationLibrary(library)
                    select Assembly.Load(new AssemblyName(library.Name))).ToArray();
        }

        private static bool IsCandidateComplationLibrary(Library library)
        {
            return library.Name == "KafkaMessagingQueue.ReportApi.Application" || library.Dependencies.Any(x => x.Name.StartsWith("KafkaMessagingQueue.ReportApi.Application"));
        }
    }
}
