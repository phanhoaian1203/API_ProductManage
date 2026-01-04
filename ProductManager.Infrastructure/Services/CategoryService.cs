using ProductManager.Core.Entities;
using ProductManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Category> Addcategory(Category category)
        {
            return await _repository.AddAsync(category);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException("Không tìm thấy danh mục để xóa");
            
            await _repository.DeleteAsync(id);

        }

        public async Task<List<Category>> GetAllCategories()
        {
            var categories = await _repository.GettAllAsync();
            if (categories.Count == 0) throw new KeyNotFoundException("Không tìm thấy danh mục nảo");
            return categories;
        }

        public async Task<Category> GetcategoryById(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException("Không tìm thấy danh mục nào");
            return category;
        }

        public async Task UpdateCategory(int id, Category category)
        {
            var cateUp = await _repository.GetByIdAsync(id);
            if (cateUp == null) throw new KeyNotFoundException("Không tìm thấy danh mục nào");
            cateUp.Name = category.Name;
            await _repository.UpdateAsync(cateUp);
        }
    }
}
