using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace SeturAssessment.Persistence.Test
{
    public class DataModuleFixture
    {
        public DataModuleFixture()
        {
            var services = new ServiceCollection();
            services.AddScoped(x =>
            {
                var options = new DbContextOptionsBuilder<SeturContext>()
                    .UseInMemoryDatabase("TestDatabase")
                    .Options;
                return new SeturContext(options);
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
