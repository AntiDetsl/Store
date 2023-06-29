using Store.Entities;

namespace Store.DAL.Interfaces
{
    public interface IOrderDao
    {
        Task<int> AddAsync(Order order);

        Task DeleteAsync(int id);

        Task<Order> UpdateAsync(Order order);

        Task<Order> GetByIdAsync(int id);

        Task<ICollection<Order>> GetAllAsync();

        //Paging
        Task<IEnumerable<Order>> PageAsync(int page, int pageSize);

        Task<int> CountTotalItemsAsync();
    }
}
