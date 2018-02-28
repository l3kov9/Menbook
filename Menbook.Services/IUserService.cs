namespace Menbook.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<IEnumerable<int>> FavouriteCarIdsByUserIdAsync(string id);

        Task<IDictionary<int, int>> AllRatesIdsAsync(string id);
    }
}
