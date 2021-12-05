using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace SeturAssessment.Api.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
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
