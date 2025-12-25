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
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            //if (product.Name.IsNullOrEmpty()) throw new KeyNotFoundException("Tên sản phẩm không được để trống!!!");
            //if (product.Price < 0) throw new Exception("Giá sản phẩm phải lớn hơn 0!!!");
            //if (product.Stock < 0) throw new Exception("Số lượng tồn kho không được âm!!!");
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
            await _repository.UpdateAsync(proUp);
        }
    }
}
