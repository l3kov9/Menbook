namespace Menbook.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;

    public class CarService : ICarService
    {
        private readonly MenbookDbContext db;

        public CarService(MenbookDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CarMakeListingServiceModel>> AllAsync(int page = 1, int pageSize = 10)
            => await this.db
                .Makes
                .OrderBy(m=>m.Name)
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .ProjectTo<CarMakeListingServiceModel>()
                .ToListAsync();

        public async Task ChangeModelImgAsync(string make, string model, string imageUrl)
        {
            var result = this.db
                    .Models
                    .Where(m => m.Name == model && m.Make.Name == make)
                    .FirstOrDefault();

            if(result == null)
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
    }
}
