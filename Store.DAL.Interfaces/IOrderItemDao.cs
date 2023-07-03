using Store.Entities;

namespace Store.DAL.Interfaces
{
    public interface IOrderItemDao
    {
        Task<int> AddAsync(OrderItem item);

        Task DeleteAsync(int id);

        Task<OrderItem> GetByIdAsync(int id);

        Task UpdateAsync(OrderItem item);

        Task<IEnumerable<string>> GetAllNamesDistinct();

        Task<IEnumerable<string>> GetAllUnitsDistinct();
    }
}
