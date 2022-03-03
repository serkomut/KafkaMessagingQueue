using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using KafkaMessagingQueue.ReportApi.Application.Domain;
using KafkaMessagingQueue.ReportApi.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KafkaMessagingQueue.ReportApi.Application.Test.Infrastructure
{
    public class DataModuleFixture
    {
        public DataModuleFixture()
        {
            var services = new ServiceCollection();
            services.AddScoped(x =>
            {
                var config = new ContextConfiguration
                {
                    ConnectionString = "ReportDatabase",
                    Type = "InMemory"
                };
                return new ReportContext(config);
            });


            services.AddSingleton<IFixture, Fixture>(provider =>
            {
                var fixture = new Fixture();
                fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => fixture.Behaviors.Remove(b));
                fixture.Behaviors.Add(new OmitOnRecursionBehavior());

                return fixture;
            });

            ServiceProvider = services.BuildServiceProvider();

            var context = ServiceProvider.GetService<ReportContext>();
            var seedData = new SeedData(context);
            seedData.AddReports();
        }

        public ServiceProvider ServiceProvider { get; }
    }

    public class SeedData
    {
        ReportContext context;
        public SeedData(ReportContext context)
        {
            this.context = context;
        }

        public void AddReports()
        {
            var reports = new List<Report>();
            for (int i = 0; i < 30; i++)
            {
                var report = new Report
                {
                    Id = Guid.NewGuid(),
                    CreateBy = "SYSTEM",
                    CreateDate = DateTime.Now,
                    Data = "{\"name\": \"Name\"}",
                    Name = $"Name {i}"
                };
                reports.Add(report);
            }
            context.Reports.AddRange(reports);
            context.SaveChanges();
        }
    }
}
