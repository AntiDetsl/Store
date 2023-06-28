using Microsoft.EntityFrameworkCore;
using Store.DAL.Context;
using Store.DAL.Interfaces;
using Store.Entities;

namespace Store.DAL
{
    public class ProviderDao : IProviderDao
    {
        private readonly StoreDbContext _dbContext;

        public ProviderDao(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Provider>> GetAllAsync()
        {
            return await _dbContext.Providers.AsNoTracking().ToListAsync();
        }

        public async Task<Provider> GetByIdAsync(int id)
        {
            return await _dbContext.Providers.AsNoTracking().SingleAsync(p => p.Id == id);
        }
    }
}
