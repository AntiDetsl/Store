using Store.BLL.Interfaces;
using Store.DAL.Interfaces;
using Store.Entities;

namespace Store.BLL
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderDao _orderDao;

        public OrderLogic(IOrderDao orderDao)
        {
            _orderDao = orderDao;
        }

        public async Task<int> AddAsync(Order order)
        {
            return await _orderDao.AddAsync(order);
        }

        public async Task DeleteAsync(int id)
        {
            await _orderDao.DeleteAsync(id);
        }

        public async Task<ICollection<Order>> GetAllAsync()
        {
            return await _orderDao.GetAllAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _orderDao.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Order order)
        {
            await _orderDao.UpdateAsync(order);
        }

        //Paging
        
        public async Task<IEnumerable<Order>> PageAsync(int page, int pageSize)
        {
            return await _orderDao.PageAsync(page, pageSize);
        }

        public async Task<int> CountTotalItemsAsync()
        {
            return await _orderDao.CountTotalItemsAsync();
        }
    }
}
