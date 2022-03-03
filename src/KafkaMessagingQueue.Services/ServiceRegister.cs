using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;

namespace KafkaMessagingQueue.Services
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddGuideService(this IServiceCollection services)
        {
            services.AddHttpClient<IReportService, ReportService>((provider, client) =>
            {
                var config = provider.GetService<IConfiguration>();
                client.BaseAddress = new Uri(config["ReportApiUrl"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            return services;
        }
    }
}
