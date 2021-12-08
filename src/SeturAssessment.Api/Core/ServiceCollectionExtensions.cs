using MassTransit;
using MassTransit.KafkaIntegration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SeturAssessment.Messages.Events;
using System;

namespace SeturAssessment.Api.Core
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

                    rider.AddProducer<ReportByLocationPreparing>("PreparingEvent");
                    rider.AddProducer<ReportByLocationCompleted>("CompletedEvent");

                    rider.UsingKafka((context, factory) =>
                    {
                        factory.Host("localhost:9092");
                    });
                });
            });

            services.AddMassTransitHostedService(true);
            return services;
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
                    Title = "Setur",
                    Version = "v1",
                    Description = "Setur",
                    TermsOfService = new Uri("http://swagger.io/terms/"),
                    Extensions = { },
                    License = new OpenApiLicense()
                });
            });
            return services;
        }
    }
}
