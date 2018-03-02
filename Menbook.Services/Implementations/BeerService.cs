namespace Menbook.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models.Beers;
    using Microsoft.EntityFrameworkCore;
    using Models.Beers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BeerService : IBeerService
    {
        private readonly MenbookDbContext db;

        public BeerService(MenbookDbContext db)
        {
            this.db = db;
        }

        public async Task AddBeerAsync(string name, string country, BeerType type, double alcoholByVolume, string imageUrl, string userId)
        {
            var beer = new Beer
            {
                Name = name,
                Country = country,
                Type = type,
                AlcoholByVolume = alcoholByVolume,
                ImageUrl = imageUrl,
                AuthorId = userId
            };

            await this.db.AddAsync(beer);

            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<BeerListingServiceModel>> AllAsync(int page = 1, int pageSize = 9)
            => await this.db
                .Beers
                .OrderBy(b => b.Name)
                .Select(b => new BeerListingServiceModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    AlcoholByVolume = b.AlcoholByVolume,
                    Country = b.Country,
                    ImageUrl = b.ImageUrl,
                    Type = b.Type,
                    Author = b.Author.UserName,
                    AuthorId = b.AuthorId,
                    AverageRate = b.Ratings.Average(r => r.Rate)
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public async Task<IEnumerable<BeerListingServiceModel>> AllBySearchAsync(int page, int pageSize, string search)
            => await this.db
                .Beers
                .OrderBy(b => b.Name)
                .Where(b => b.Name.ToLower().Contains(search.ToLower()))
                .Select(b => new BeerListingServiceModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    AlcoholByVolume = b.AlcoholByVolume,
                    Country = b.Country,
                    ImageUrl = b.ImageUrl,
                    Type = b.Type,
                    Author = b.Author.UserName,
                    AuthorId = b.AuthorId,
                    AverageRate = b.Ratings.Average(r => r.Rate)
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public async Task<int> TotalBySearchAsync(string search)
            => await this.db
                .Beers
                .Where(b => b.Name.ToLower().Contains(search.ToLower()))
                .CountAsync();

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var beer = await this.db
                    .Beers
                    .FindAsync(id);

            if (beer == null)
            {
                return false;
            }

            this.db.Beers.Remove(beer);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> EditBeerAsync(int id, string name, string country, BeerType type, double alcoholByVolume, string imageUrl)
        {
            var beer = await this.db.Beers.FindAsync(id);

            if (beer == null)
            {
                return false;
            }

            beer.Name = name;
            beer.Country = country;
            beer.Type = type;
            beer.AlcoholByVolume = alcoholByVolume;
            beer.ImageUrl = imageUrl;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<BeerDetailsServiceModel> GetBeerDetailsByIdAsync(int id)
            => await this.db
                .Beers
                .Where(b => b.Id == id)
                .ProjectTo<BeerDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<bool> RateBeerAsync(string userId, int beerId, int rating)
        {
            var beer = this.db
                .BeerRatings
                .Find(beerId, userId);

            if (beer != null)
            {
                return false;
            }

            var beerRating = new BeerRating
            {
                BeerId = beerId,
                UserId = userId,
                Rate = rating
            };

            await this.db.AddAsync(beerRating);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<int> TotalAsync()
            => await this.db
                .Beers
                .CountAsync();

        public IEnumerable<BeerNameListingServiceModel> GetAllNamesAsync()
            => this.db
                .Beers
                .OrderBy(b => b.Name)
                .Select(b => new BeerNameListingServiceModel
                {
                    Id = b.Id,
                    Name = $"{b.Name} {b.Type}"
                })
                .ToList();

        public async Task<double> GetAbvBIdAsync(int beerId)
            => await this.db
                .Beers
                .Where(b => b.Id == beerId)
                .Select(b => b.AlcoholByVolume)
                .FirstOrDefaultAsync();
    }
}
