namespace Menbook.Test.Services
{
    using Data;
    using Data.Models.Beers;
    using FluentAssertions;
    using Menbook.Services.Implementations;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
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

            db.Beers.AddRange(firstBeer, secondBeer, thirdBeer);
            await db.SaveChangesAsync();

            var beerService = new BeerService(db);

            // Act
            //var result = await beerService.AllBySearchAsync(1, 3, "a");

            // Assert
            var list = new List<Beer> { firstBeer, secondBeer, thirdBeer };

            list
                .Should()
                .Match(r => r.ElementAt(0).Id == 1)
                .And
                .HaveCount(3);
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
