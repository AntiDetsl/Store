using Microsoft.EntityFrameworkCore;
using Store.DAL.Context;
using Store.DAL.Interfaces;
using Store.Entities;

namespace Store.DAL
{
    public class OrderItemDao : IOrderItemDao
    {
        private readonly StoreDbContext _dbContext;
        public OrderItemDao(StoreDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(OrderItem item)
        {
            string? orderNumber = _dbContext.Orders.SingleOrDefault(o => o.Id == item.OrderId)?.Number;

            if (orderNumber == null || item.Name == orderNumber)
            {
                throw new ArgumentException("Item name can't be equal to order number", nameof(item));
            }
            else
            {
                _dbContext.Add(item);
                await _dbContext.SaveChangesAsync();

                return item.Id;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _dbContext.Items.SingleAsync(i => i.Id == id);

            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetAllNamesDistinct()
        {
            IEnumerable<string> names = await _dbContext.Items
                .AsNoTracking()
                .Select(i => i.Name)
                .Distinct()
                .ToListAsync();

            return names;
        }

        public async Task<IEnumerable<string>> GetAllUnitsDistinct()
        {
            IEnumerable<string> units = await _dbContext.Items
                .AsNoTracking()
                .Select(i => i.Unit)
                .Distinct()
                .ToListAsync();

            return units;
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            var item = await _dbContext.Items
                .AsNoTracking()
                .SingleAsync(i => i.Id == id);

            return item;
        }

        public async Task UpdateAsync(OrderItem item)
        {
            string? orderNumber = _dbContext.Orders.SingleOrDefault(o => o.Id == item.OrderId)?.Number;

            if (orderNumber == null || item.Name == orderNumber)
            {
                throw new ArgumentException("Item name can't be equal to order number", nameof(item));
            }
            else
            {
                _dbContext.Update(item);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
