using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using SeturAssessment.Domain;
using SeturAssessment.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeturAssessment.Queries.Test.Infrastructure
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
                    ConnectionString = "TestDatabase",
                    Type = "InMemory"
                };
                return new SeturContext(config);
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

            var context = ServiceProvider.GetService<SeturContext>();
            var seedData = new SeedData(context);
            seedData.AddGuides();
        }

        public ServiceProvider ServiceProvider { get; }
    }

    public class SeedData
    {
        readonly string[] locations = new[] { "Adana", "Antalya", "Istanbul", "Ankara", "Izmir", "Eskisehir", "Diyarbakir", "Trabzon", "Bolu", "Mersin" };
        SeturContext context;
        public SeedData(SeturContext context)
        {
            this.context = context;
        }
        public void AddGuides()
        {
            var guides = new List<Guide>();

            for (int i = 0; i < 10; i++)
            {
                var model = new Guide
                {
                    Company = $"Company_{i}",
                    Id = Guid.NewGuid(),
                    Name = $"Name {i}",
                    Surname = $"Surname {i}",
                    CreateBy = "SYSTEM",
                    Contacts = new []
                    {
                        new Contact
                        {
                            ContactType = ContactType.EMAIL,
                            Value = $"email{i}@email.com",
                            CreateBy = "SYSTEM"
                        },
                        new Contact
                        {
                            ContactType = ContactType.PHONE,
                            Value = $"05{i}21234567",
                            CreateBy = "SYSTEM"
                        },
                        new Contact
                        {
                            ContactType = ContactType.LOCATION,
                            Value = locations[i],
                            CreateBy = "SYSTEM"
                        }
                    }
                };
                guides.Add(model);
            }

            var guide = new Guide
            {
                Company = $"Test",
                Id = Guid.NewGuid(),
                Name = $"TestName",
                Surname = $"TestSurname",
                CreateBy = "SYSTEM",
                Contacts = new[]
                    {
                        new Contact
                        {
                            ContactType = ContactType.EMAIL,
                            Value = $"emailtest@email.com",
                            CreateBy = "SYSTEM"
                        },
                        new Contact
                        {
                            ContactType = ContactType.PHONE,
                            Value = $"05121231425",
                            CreateBy = "SYSTEM"
                        },
                        new Contact
                        {
                            ContactType = ContactType.LOCATION,
                            Value = "Antalya",
                            CreateBy = "SYSTEM"
                        }
                    }
            };
            guides.Add(guide);
            context.AddRange(guides);
            context.SaveChanges();
        }
    }
}
