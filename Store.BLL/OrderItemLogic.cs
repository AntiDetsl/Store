using Store.BLL.Interfaces;
using Store.DAL.Interfaces;
using Store.Entities;

namespace Store.BLL
{
    public class OrderItemLogic : IOrderItemLogic
    {
        private readonly IOrderItemDao _itemDao;

        public OrderItemLogic(IOrderItemDao itemDao) 
        {
            _itemDao = itemDao;
        }

        public async Task<int> AddAsync(OrderItem item)
        {
            return await _itemDao.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            await _itemDao.DeleteAsync(id);
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _itemDao.GetByIdAsync(id);
        }

        public async Task UpdateAsync(OrderItem item)
        {
            await _itemDao.UpdateAsync(item);
        }
    }
}
