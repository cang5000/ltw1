using System.Collections.Generic;
using WebBanQuanAo.Models;

namespace WebBanQuanAo.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>>
        GetAllAsync(); Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product); Task UpdateAsync(Product product); Task DeleteAsync(int id);

    }
}