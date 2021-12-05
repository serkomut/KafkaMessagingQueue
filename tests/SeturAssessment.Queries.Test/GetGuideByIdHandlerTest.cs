﻿using SeturAssessment.Persistence;
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
    public class GetGuideByIdHandlerTest : IClassFixture<DataModuleFixture>
    {
        private readonly SeturContext context;
        private readonly GetGuideByIdHandler handler;
        public GetGuideByIdHandlerTest(DataModuleFixture dataModuleFixture)
        {
            context = dataModuleFixture.ServiceProvider.GetService<SeturContext>();
            handler = new GetGuideByIdHandler(context);
        }
        [Fact]
        public async Task Handle_Should_Be_Success()
        {
            var guide = await context.Guides.FirstAsync();
            var request = new GetGuideById(guide.Id);

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.True(result.Contacts.Length > 0);
        }

        [Fact]
        public async Task Handle_Should_Be_Exception_When_Not_In_Database()
        {
            var request = new GetGuideById(Guid.NewGuid());

            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));

            Assert.Equal("Kayıt bulunamadı!", exception.Message);
        }
    }
}
