namespace Menbook.Services.Implementations
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly MenbookDbContext db;

        public UserService(MenbookDbContext db)
        {
            this.db = db;
        }

        public async Task<IDictionary<int, int>> AllRatedBeerIdsAsync(string id)
            => await this.db
                .BeerRatings
                .Where(br => br.UserId == id)
                .Select(br => new { br.BeerId, br.Rate })
                .ToDictionaryAsync(br => br.BeerId, br => br.Rate);

        public async Task<IDictionary<int, int>> AllRatesIdsAsync(string id)
            => await this.db
                .Ratings
                .Where(r => r.UserId == id)
                .Select(r => new { r.ModelId, r.Rate })
                .ToDictionaryAsync(r => r.ModelId, r => r.Rate);

        public async Task<int> CurrentUserAgeByIdAsync(string id)
            => await this.db
                .Users
                .Where(u => u.Id == id)
                .Select(u => (int)(DateTime.UtcNow.Year - u.Birthdate.Year))
                .FirstOrDefaultAsync();

        public async Task<string> CurrentUserNameByIdAsync(string id)
            => await this.db
                .Users
                .Where(u => u.Id == id)
                .Select(u => u.UserName)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<int>> FavouriteCarIdsByUserIdAsync(string id)
            => await this.db
                .UserCars
                .Where(uc => uc.UserId == id)
                .Select(uc => uc.ModelId)
                .ToListAsync();
    }
}
