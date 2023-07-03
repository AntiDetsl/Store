using Microsoft.EntityFrameworkCore;
using Store.DAL.Context;
using Store.Entities.Filters;
using Store.DAL.Interfaces;
using Store.Entities;

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
            bool temp = await _dbContext.Orders
                .AnyAsync(o => order.Number == o.Number 
                && order.ProviderId == o.ProviderId);

            if(temp)
            {
                throw new ArgumentException("There should not be two orders " +
                    "from the same provider with the same number", nameof(order));
            }

            _dbContext.Add(order);
            await _dbContext.SaveChangesAsync();

            return order.Id;
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

        public async Task UpdateAsync(Order order)
        {
            bool temp = await _dbContext.Orders
                .AnyAsync(o => order.Id != o.Id
                && order.Number == o.Number
                && order.ProviderId == o.ProviderId);

            if (temp)
            {
                throw new ArgumentException("There should not be two orders " +
                    "from the same provider with the same number", nameof(order));
            }

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            _dbContext.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> PageAsync(int page, int pageSize, OrderFilters filters)
        {
            var query = Filter(filters);

            var paginatedData = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return paginatedData;
        }

        public async Task<int> CountTotalItemsAsync(OrderFilters filters)
        {
            return await Filter(filters).CountAsync();
        }


        public async Task<IEnumerable<string>> GetNumbersDistinct()
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Select(o=>o.Number)
                .Distinct()
                .ToListAsync();
        }

        private IQueryable<Order> Filter(OrderFilters filters)
        {
            var query = GetAllSortedByIdDesc();

            if(filters.Providers != null && filters.Providers.Any())
            {
                query = query.Where(order => filters.Providers.Contains(order.Provider.Name));
            }

            if (filters.Numbers != null && filters.Numbers.Any())
            {
                query = query.Where(order => filters.Numbers.Contains(order.Number));
            }

            query = query.Where(order => order.Date >= filters.StartDate &&
                order.Date <= filters.EndDate);

            if (filters.ItemFilters != null)
            {
                query = FilterByItems(query, filters.ItemFilters);
            }

            return query;
        }

        private IQueryable<Order> FilterByItems(IQueryable<Order> query, ItemFilters filters) 
        {
            if(filters.Names != null && filters.Names.Any())
            {
                query = query.Where(order => order.Items
                .Any(item => filters.Names.Contains(item.Name)));
            }

            if(filters.Units != null && filters.Units.Any())
            {
                query = query.Where(order => order.Items
                .Any(item => filters.Units.Contains(item.Unit)));
            }

            return query;
        }

        private IQueryable<Order> GetAllSortedByIdDesc()
        {
            var orders = _dbContext.Orders
                .OrderByDescending(o => o.Id)
                .AsNoTracking()
                .Include(o => o.Items)
                .Include(o => o.Provider)
                .AsQueryable();
            return orders;
        }
    }
}
