namespace Menbook.Test.Services
{
    using Data;
    using Data.Models.Beers;
    using FluentAssertions;
    using Menbook.Services.Implementations;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class BeerServiceTest
    {
        [Fact]
        public async Task AllBySearchAsyncGetCorrectResults()
        {
            // Arrange
            var db = this.GetDatabase();

            var firstBeer = new Beer { Id = 1, Name = "Zagorka" };
            var secondBeer = new Beer { Id = 2, Name = "Staropramen" };
            var thirdBeer = new Beer { Id = 3, Name = "Duff" };

            db.AddRange(firstBeer, secondBeer, thirdBeer);
            await db.SaveChangesAsync();

            var beerService = new BeerService(db);

            // Act
            var result = await beerService.AllBySearchAsync(1, int.MaxValue, "a");

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 2)
                .And
                .Match(r => r.ElementAt(2).Id == 1)
                .And
                .HaveCount(2);
        }

        private MenbookDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<MenbookDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new MenbookDbContext(dbOptions);
        }
    }
}
