using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace KafkaMessagingQueue.Persistence.Test.Infrastructure
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
                return new DatabaseContext(config);
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
        }

        public ServiceProvider ServiceProvider { get; }
    }
}
