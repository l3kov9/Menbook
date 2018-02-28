namespace Menbook.Services.Implementations
{
    using Data;
    using Microsoft.EntityFrameworkCore;
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

        public async Task<IDictionary<int, int>> AllRatesIdsAsync(string id)
            => this.db
                .Ratings
                .Where(r => r.UserId == id)
                .Select(r => new { r.ModelId, r.Rate })
                .ToDictionary(r => r.ModelId, r => r.Rate);

        public async Task<IEnumerable<int>> FavouriteCarIdsByUserIdAsync(string id)
            => await this.db
                .UserCars
                .Where(uc => uc.UserId == id)
                .Select(uc => uc.ModelId)
                .ToListAsync();
    }
}
