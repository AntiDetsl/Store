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
            try
            {
                return await _itemDao.AddAsync(item);
            }
            catch (ArgumentException)
            {
                return -1;
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _itemDao.DeleteAsync(id);
        }

        public async Task<IEnumerable<string>> GetAllNamesDistinct()
        {
            return await _itemDao.GetAllNamesDistinct();
        }

        public async Task<IEnumerable<string>> GetAllUnitsDistinct()
        {
            return await _itemDao.GetAllUnitsDistinct();
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _itemDao.GetByIdAsync(id);
        }

        public async Task<bool> TryUpdateAsync(OrderItem item)
        {
            try
            {
                await _itemDao.UpdateAsync(item);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}
