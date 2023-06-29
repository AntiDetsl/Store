﻿using Store.Entities;

namespace Store.BLL.Interfaces
{
    public interface IOrderLogic
    {
        Task<int> AddAsync(Order order);
        
        Task<ICollection<Order>> GetAllAsync();

        Task DeleteAsync(int id);

        Task<Order> GetByIdAsync(int id);

        Task UpdateAsync(Order order);

        //Paging

        Task<IEnumerable<Order>> PageAsync(int page, int pageSize);

        Task<int> CountTotalItemsAsync();
    }
}
