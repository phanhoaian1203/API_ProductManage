using ProductManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GettAllAsync();
        Task<Category> AddAsync(Category category);
        Task<Category> GetByIdAsync(int id);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
