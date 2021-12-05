using SeturAssessment.Persistence;
using SeturAssessment.Persistence.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace SeturAssessment.Commands.Test
{
    public class AddContactHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly SeturContext context;
        public AddContactHandlerTest(DataModuleFixture dataModuleFixture)
        {
            context = dataModuleFixture.ServiceProvider.GetService<SeturContext>();
        }
    }
}
