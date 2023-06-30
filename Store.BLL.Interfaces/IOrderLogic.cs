using Store.Entities;
using Store.Entities.Filters;

namespace Store.BLL.Interfaces
{
    public interface IOrderLogic
    {
        Task<int> AddAsync(Order order);
        
        Task<ICollection<Order>> GetAllAsync();

        Task DeleteAsync(int id);

        Task<Order> GetByIdAsync(int id);

        Task UpdateAsync(Order order);

        Task<IEnumerable<string>> GetNumbersDistinct();

        //Paging

        Task<IEnumerable<Order>> PageAsync(int page, int pageSize, OrderFilters filters);

        Task<int> CountTotalItemsAsync(OrderFilters filters);
    }
}
