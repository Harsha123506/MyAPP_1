using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<String>> GetBrandNamesAsync();
        Task<IReadOnlyList<String>> GetTypesAsync();
        Task AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        bool productExists(int id);
        Task<bool> saveChanges();
        Task<IReadOnlyList<Product>> GetProductsByBrandAndTypeAsync(string? brand,string? Type, string? Sort);
    }
}
