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
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository repository)
        {
            _categoryRepository = repository;
        }
        public async Task<Category> Addcategory(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException("Không tìm thấy danh mục để xóa");
            
            await _categoryRepository.DeleteAsync(id);

        }

        public async Task<List<Category>> GetAllCategories()
        {
            var categories = await _categoryRepository.GettAllAsync();
            if (categories.Count == 0) throw new KeyNotFoundException("Không tìm thấy danh mục nảo");
            return categories;
        }

        public async Task<Category> GetcategoryById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null) throw new KeyNotFoundException("Không tìm thấy danh mục nào");
            return category;
        }

        public async Task UpdateCategory(int id, Category category)
        {
            var cateUp = await _categoryRepository.GetByIdAsync(id);
            if (cateUp == null) throw new KeyNotFoundException("Không tìm thấy danh mục nào");
            cateUp.Name = category.Name;
            await _categoryRepository.UpdateAsync(cateUp);
        }
    }
}
