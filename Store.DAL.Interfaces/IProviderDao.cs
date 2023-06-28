using Store.Entities;

namespace Store.DAL.Interfaces
{
    public interface IProviderDao
    {
        Task<IEnumerable<Provider>> GetAllAsync();

        Task<Provider> GetByIdAsync(int id);
    }
}
