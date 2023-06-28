using Store.Entities;

namespace Store.BLL.Interfaces
{
    public interface IOrderItemLogic
    {
        Task<int> AddAsync(OrderItem item);

        Task DeleteAsync(int id);

        Task<OrderItem> GetByIdAsync(int id);

        Task UpdateAsync(OrderItem item);
    }
}
