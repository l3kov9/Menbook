namespace Menbook.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models.Cars;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CarService : ICarService
    {
        private readonly MenbookDbContext db;

        public CarService(MenbookDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> AddUserCarAsync(string userId, int modelId)
        {
            var result = await this.db.UserCars.FindAsync(userId, modelId);

            if (result != null)
            {
                return false;
            }

            var userCar = new UserCar
            {
                UserId = userId,
                ModelId = modelId
            };

            await this.db.AddAsync(userCar);
            this.db.SaveChanges();

            return true;
        }

        public async Task<bool> RemoveFromMyCarsAsync(string userId, int modelId)
        {
            var userCar = await this.db
                    .UserCars
                    .FindAsync(userId, modelId);

            if (userCar == null)
            {
                return false;
            }

            this.db.UserCars.Remove(userCar);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RateCarAsync(string userId, int modelId, int rating)
        {
            var userRate = await this.db.Ratings.FindAsync(userId, modelId);

            if (userRate != null)
            {
                return false;
            }

            var userRating = new Rating
            {
                UserId = userId,
                ModelId = modelId,
                Rate = rating
            };

            await this.db.AddAsync(userRating);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CarMakeListingServiceModel>> AllBySearchAsync(int page = 1, int pageSize = 10, string search = "")
            => await this.db
                .Makes
                .Where(m=>m.Name.ToLower().Contains(search.ToLower()))
                .OrderBy(m => m.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<CarMakeListingServiceModel>()
                .ToListAsync();

        public async Task ChangeModelImgAsync(string make, string model, string imageUrl)
        {
            var result = this.db
                    .Models
                    .Where(m => m.Name == model && m.Make.Name == make)
                    .FirstOrDefault();

            if (result == null)
            {
                return;
            }

            result.ImageUrl = imageUrl;

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteImageAsync(string make, string model)
        {
            var result = this.db
                    .Models
                    .Where(m => m.Name == model && m.Make.Name == make)
                    .FirstOrDefault();

            if (result == null)
            {
                return;
            }

            result.ImageUrl = null;

            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarModelFavouritesListingServiceModel>> GetCarInfoByIds(IEnumerable<int> carIds)
            => await this.db
                .Models
                .Where(m => carIds.Contains(m.Id))
                .Select(m => new CarModelFavouritesListingServiceModel
                {
                    ModelId = m.Id,
                    Make = m.Make.Name,
                    Model = m.Name,
                    ImageUrl = m.ImageUrl
                })
                .ToListAsync();

        public async Task<int> GetMakeIdByNameAsync(string make)
            => await this.db
                .Makes
                .Where(m => m.Name == make)
                .Select(m => m.Id)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<CarModelListingServiceModel>> ModelsByMake(int id)
            => await this.db
                .Models
                .Where(m => m.MakeId == id)
                .ProjectTo<CarModelListingServiceModel>()
                .ToListAsync();

        public async Task<string> NameById(int id)
            => await this.db
                .Makes
                .Where(m => m.Id == id)
                .Select(m => m.Name)
                .FirstOrDefaultAsync();

        public async Task<int> Total()
            => await this.db
                .Makes
                .CountAsync();

        public async Task<IDictionary<int, double>> AverageRateListingByIdAsync(int id)
            => await this.db
                .Models
                .Where(m => m.MakeId == id)
                .Select(m => new
                {
                    m.Id,
                    AverageRating = m.UserRatings.Any() ? (m.UserRatings.Sum(r => r.Rate) * 1.0 / (double)(m.UserRatings.Count())) : 0.0
                })
                .ToDictionaryAsync(m => m.Id, m => m.AverageRating);

        public async Task<CarDetailsServiceModel> DetailsByIdAsync(int id)
        {
            var reviews = await this.db
                    .Reviews
                    .Where(r => r.ModelId == id)
                    .OrderByDescending(r => r.PublishDate)
                    .Select(r => new CarReviewListingServiceModel
                    {
                        User = r.User.UserName,
                        Text = r.Text,
                        PublishedDate = r.PublishDate
                    })
                    .ToListAsync();

            return await this.db
                  .Models
                  .Where(m => m.Id == id)
                  .Select(m => new CarDetailsServiceModel
                  {
                      ModelId = m.Id,
                      Make = m.Make.Name,
                      Model = m.Name,
                      ImageUrl = m.ImageUrl,
                      Reviews = reviews
                  })
                  .FirstOrDefaultAsync();
        }

        public async Task<bool> PostReviewAsync(string userId, int modelId, string review)
        {
            var reviewExists = await this.db.Reviews.FindAsync(userId, modelId);

            if (reviewExists != null)
            {
                return false;
            }

            var userReview = new Review
            {
                UserId = userId,
                ModelId = modelId,
                Text = review,
                PublishDate = DateTime.UtcNow
            };

            await this.db.AddAsync(userReview);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<string>> UserReviewsIdsAsync(int id)
            => await this.db
                    .Reviews
                    .Where(r => r.ModelId == id)
                    .Select(r => r.UserId)
                    .ToListAsync();

        public async Task<IEnumerable<TopCarsDetailsListingServiceModel>> GetTopFiveCarsByRatingAsync()
            => await this.db
                .Models
                .Where(m => m.UserRatings.Any())
                .Select(m => new TopCarsDetailsListingServiceModel
                {
                    ModelId = m.Id,
                    Make = m.Make.Name,
                    Model = m.Name,
                    ImageUrl = m.ImageUrl,
                    AverageRating = m.UserRatings.Average(r => r.Rate),
                    TotalVotes = m.UserRatings.Count()
                })
                .OrderByDescending(m => m.AverageRating)
                .ThenByDescending(m => m.TotalVotes)
                .Take(5)
                .ToListAsync();

        public async Task<int> TotalBySearchAsync(string search)
            => await this.db
                .Makes
                .Where(m => m.Name.ToLower().Contains(search.ToLower()))
                .CountAsync();
    }
}
