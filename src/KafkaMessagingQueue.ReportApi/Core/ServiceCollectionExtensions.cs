using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using KafkaMessagingQueue.ReportApi.Application.Events.Consumers;
using KafkaMessagingQueue.ReportApi.Application.Events.Models;
using System;
using System.Net;
using System.Reflection;

namespace KafkaMessagingQueue.ReportApi.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
                config.AddRider(rider =>
                {
                    rider.AddConsumer<ReportByLocationPreparingConsumer>();
                    rider.AddConsumer<ReportByLocationCompletedConsumer>();

                    rider.UsingKafka((context, factory) =>
                    {
                        factory.Host("localhost:9092");
                        factory.TopicEndpoint<ReportByLocationPreparing>("PreparingEvent", 
                            GenerateUniqName(nameof(ReportByLocationPreparing)), e =>
                        {
                            e.ConfigureConsumer<ReportByLocationPreparingConsumer>(context);
                        });

                        factory.TopicEndpoint<ReportByLocationCompleted>("CompletedEvent",
                            GenerateUniqName(nameof(ReportByLocationCompleted)), e =>
                            {
                                e.ConfigureConsumer<ReportByLocationCompletedConsumer>(context);
                            });
                    });
                });
            });

            services.AddMassTransitHostedService(true);
            return services;
        }

        private static string GenerateUniqName(string eventName)
        {
            string callingAssembly = Assembly.GetCallingAssembly().GetName().Name;
            var c = $"{Dns.GetHostName()}.{callingAssembly}.{eventName}";
            return c;
        }
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                            Scheme = "outh2",
                            In = ParameterLocation.Header
                        },
                        new[] {"readAccess", "writeAccess"}
                    }
                });

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Title",
                    Version = "v1",
                    Description = "Description",
                    TermsOfService = new Uri("http://swagger.io/terms/"),
                    Extensions = { },
                    License = new OpenApiLicense()
                });
            });
            return services;
        }
    }
}
