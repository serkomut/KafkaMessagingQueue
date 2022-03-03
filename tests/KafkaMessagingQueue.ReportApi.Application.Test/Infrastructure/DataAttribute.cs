using AutoFixture;
using AutoFixture.Xunit2;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaMessagingQueue.ReportApi.Application.Test.Infrastructure
{
    public class DataAttribute : AutoDataAttribute
    {
        public DataAttribute() : base(() => new DataModuleFixture().ServiceProvider.GetService<IFixture>()) { }
    }
}
