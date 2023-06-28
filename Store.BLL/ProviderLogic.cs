using Store.BLL.Interfaces;
using Store.DAL.Interfaces;
using Store.Entities;

namespace Store.BLL
{
    public class ProviderLogic : IProviderLogic
    {
        private readonly IProviderDao _providerDao;

        public ProviderLogic(IProviderDao providerDao)
        {
            _providerDao = providerDao;
        }

        public async Task<IEnumerable<Provider>> GetAllAsync()
        {
            return await _providerDao.GetAllAsync();
        }

        public async Task<Provider> GetByIdAsync(int id)
        {
            return await _providerDao.GetByIdAsync(id);
        }
    }
}
