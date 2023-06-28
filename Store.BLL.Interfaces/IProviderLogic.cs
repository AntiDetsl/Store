using Store.Entities;

namespace Store.BLL.Interfaces
{
    public interface IProviderLogic
    {
        Task<IEnumerable<Provider>> GetAllAsync();

        Task<Provider> GetByIdAsync(int id);
    }
}
