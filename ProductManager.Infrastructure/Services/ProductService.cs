using Microsoft.IdentityModel.Tokens;
using ProductManager.Core.Entities;
using ProductManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Infrastructure.Sevices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _repositoryCate;
        public ProductService(IProductRepository repository, ICategoryRepository repository1)
        {
            _repository = repository;
            _repositoryCate = repository1;
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            var category = await _repositoryCate.GetByIdAsync(product.CategoryId);
            if (category == null)
            {
                // Ném lỗi 404 để Middleware bắt
                throw new KeyNotFoundException($"Danh mục có ID {product.CategoryId} không tồn tại!");
            }
            return await _repository.AddAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var proDel = await _repository.GetByIdAsync(id);
            if (proDel == null) throw new KeyNotFoundException("Không tìm thấy sản phẩm để xóa!!!");
            await _repository.DeleteAsync(id);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            if (products.Count == 0) throw new KeyNotFoundException("Không tìm thấy sản phẩm nào cả!!!");
            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("Không tìm thấy sản phẩm !!!");
            return product;
        }

        public async Task UpdateProductAsync(int id, Product product)
        {
            var proUp = await _repository.GetByIdAsync(id);
            if (proUp == null) throw new KeyNotFoundException("Không tìm thấy sản phẩm để cập nhật!!!");
            proUp.Name = product.Name;
            proUp.Price = product.Price;
            proUp.Stock = product.Stock;
            var pro = await _repositoryCate.GetByIdAsync(id);
            if (pro == null) throw new KeyNotFoundException("Không tìm thấy danh mục để cập nhật!!!");
            proUp.CategoryId = product.CategoryId;
            await _repository.UpdateAsync(proUp);
        }
    }
}
