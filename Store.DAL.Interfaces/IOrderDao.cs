using Store.Entities;
using Store.Entities.Filters;

namespace Store.DAL.Interfaces
{
    public interface IOrderDao
    {
        Task<int> AddAsync(Order order);

        Task DeleteAsync(int id);

        Task<Order> UpdateAsync(Order order);

        Task<Order> GetByIdAsync(int id);

        Task<ICollection<Order>> GetAllAsync();

        Task<IEnumerable<string>> GetNumbersDistinct();

        //Paging
        Task<IEnumerable<Order>> PageAsync(int page, int pageSize, OrderFilters filters);

        Task<int> CountTotalItemsAsync(OrderFilters filters);
    }
}
