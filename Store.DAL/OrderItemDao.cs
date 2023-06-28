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
            _dbContext.Add(item);
            await _dbContext.SaveChangesAsync();

            return item.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _dbContext.Items.SingleAsync(i => i.Id == id);

            _dbContext.Remove(item);
            await _dbContext.SaveChangesAsync();
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
            _dbContext.Update(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
