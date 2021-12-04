using AutoFixture;
using SeturAssessment.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace SeturAssessment.Persistence.Test
{
    public class GuideTest : IClassFixture<DataModuleFixture>
    {
        private readonly IFixture fixture;
        private readonly SeturContext context;
        public GuideTest(DataModuleFixture dataModuleFixture)
        {
            fixture = dataModuleFixture.ServiceProvider.GetService<IFixture>();
            context = dataModuleFixture.ServiceProvider.GetService<SeturContext>();
        }

        [Theory, Data]
        public void Model_Should_Be_Valid_When_Any_Entity(Guide entity)
        {
            Assert.Equal(nameof(Guid), entity.Id.GetType().Name);
            Assert.NotEmpty(entity.Name);
            Assert.NotEmpty(entity.Surname);
            Assert.True(entity.Name.Length <= 200);
            Assert.True(entity.Surname.Length <= 200);
        }

        [Theory, Data]
        public async Task Creation_Should_Success_When_Any_Entity(Guide entity)
        {
            await context.Guides.AddAsync(entity);
            var result = await context.SaveChangesAsync();

            Assert.NotNull(entity.Contacts);
            Assert.True(result > 0);
        }

        [Theory, Data]
        public async Task Update_Should_Success_When_An_Entity(Guide entity)
        {
            await context.Guides.AddAsync(entity);
            await context.SaveChangesAsync();

            var item = context.Guides.FirstOrDefault(x => x.Id == entity.Id);

            item.Name = "Name";
            item.Surname = "Surname";
            item.Company = "Setur";
            context.Guides.Update(item);
            var result = await context.SaveChangesAsync();
            Assert.True(result > 0);
            Assert.Equal(entity.Name, item.Name);
        }

        [Theory, Data]
        public async Task Delete_Should_Success_When_An_Entity(Guide entity)
        {
            await context.Guides.AddAsync(entity);
            await context.SaveChangesAsync();

            var item = context.Guides.FirstOrDefault(x => x.Id == entity.Id);
            context.Guides.Remove(item);
            var result = await context.SaveChangesAsync();
            var exists = context.Guides.Any(x => x.Id == entity.Id);
            Assert.False(exists);
            Assert.True(result > 0);
        }
    }
}
