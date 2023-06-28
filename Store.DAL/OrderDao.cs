using Microsoft.EntityFrameworkCore;
using Store.DAL.Context;
using Store.DAL.Interfaces;
using Store.Entities;
using System.Threading.Tasks;

namespace Store.DAL
{
    public class OrderDao : IOrderDao
    {
        private readonly StoreDbContext _dbContext;

        public OrderDao(StoreDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(Order order)
        {
            _dbContext.Add(order);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _dbContext.Orders
                .Include(o => o.Items)
                .SingleOrDefaultAsync(o => o.Id == id);

            if(order == null) 
            {
                throw new ArgumentNullException(nameof(id), "Wrong ID. Order not found.");
            }
            
            _dbContext.Remove(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Order>> GetAllAsync()
        {
            var orders = _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Include(o => o.Provider);
            return await orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var order = await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .Include(o => o.Provider)
                .SingleAsync(o => o.Id == id);

            return order;
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            if(order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            _dbContext.Update(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }
    }
}
