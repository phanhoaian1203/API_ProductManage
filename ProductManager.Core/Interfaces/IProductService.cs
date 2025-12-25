using ProductManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Core.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> AddProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int id);
        Task DeleteProductAsync(int id);
        Task UpdateProductAsync(int id, Product product);
    }
}
