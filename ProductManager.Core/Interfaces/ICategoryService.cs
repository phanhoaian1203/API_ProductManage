using ProductManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> Addcategory(Category category);
        Task<Category> GetcategoryById(int id);
        Task UpdateCategory(int id, Category category);
        Task DeleteCategory(int id);
    }
}
