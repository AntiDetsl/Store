using Store.BLL.Interfaces;
using Store.DAL.Interfaces;
using Store.Entities;
using Store.Entities.Filters;

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
            try
            {
                return await _orderDao.AddAsync(order);
            }
            catch(ArgumentException)
            {
                return -1;
            }
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

        public async Task<bool> TryUpdateAsync(Order order)
        {
            try
            {
                await _orderDao.UpdateAsync(order);
                return true;
            }
            catch(ArgumentException)
            {
                return false;
            }
        }

        //Paging
        
        public async Task<IEnumerable<Order>> PageAsync(int page, int pageSize, OrderFilters filters)
        {
            return await _orderDao.PageAsync(page, pageSize, filters);
        }

        public async Task<int> CountTotalItemsAsync(OrderFilters filters)
        {
            return await _orderDao.CountTotalItemsAsync(filters);
        }

        public Task<IEnumerable<string>> GetNumbersDistinct()
        {
            return _orderDao.GetNumbersDistinct();
        }
    }
}
