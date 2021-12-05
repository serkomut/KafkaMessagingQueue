using SeturAssessment.Persistence;
using SeturAssessment.Queries.Test.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeturAssessment.Messages.Queries;
using System.Threading;
using System;

namespace SeturAssessment.Queries.Test
{
    public class GetContactByIdHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly SeturContext context;
        private readonly GetContactByIdHandler handler;
        public GetContactByIdHandlerTest(DataModuleFixture dataModuleFixture)
        {
            context = dataModuleFixture.ServiceProvider.GetService<SeturContext>();
            handler = new GetContactByIdHandler(context);
        }

        [Fact]
        public async Task Handle_Should_Be_Success()
        {
            var contact = await context.Contacts.FirstAsync();
            var request = new GetContactById(contact.Id);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Handle_Should_Be_Exception_When_Not_In_Database()
        {
            var request = new GetContactById(Guid.NewGuid());

            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));

            Assert.Equal("Kayıt bulunamadı!", exception.Message);
        }
    }
}
