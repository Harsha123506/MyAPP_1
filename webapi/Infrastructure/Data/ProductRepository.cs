using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;

        public ProductRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        async Task IProductRepository.AddProduct(Product product)
        {
            await _storeContext.products.AddAsync(product);
        }

        void IProductRepository.DeleteProduct(Product product)
        {
            _storeContext.products.Remove(product);
        }

        async Task<Product> IProductRepository.GetProductByIdAsync(int id)
        {

            return await _storeContext.products.FindAsync(id);
        }

        async Task<IReadOnlyList<String>> IProductRepository.GetBrandNamesAsync()
        {
            return await _storeContext.products.Select(p => p.Brand).ToListAsync();
        }

        async Task<IReadOnlyList<String>> IProductRepository.GetTypesAsync()
        {
            return await _storeContext.products.Select(p => p.Type).Distinct().ToListAsync();
        }

        async Task<IReadOnlyList<Product>> IProductRepository.GetProductsAsync()
        {
            return await _storeContext.products.ToListAsync<Product>();
        }

        bool IProductRepository.productExists(int id)
        {
            return _storeContext.products.Any(p => p.Id == id);
        }

        async Task<bool> IProductRepository.saveChanges()
        {
            return await _storeContext.SaveChangesAsync() > 0;
        }

        void IProductRepository.UpdateProduct(Product product)
        {
            _storeContext.Entry(product).State = EntityState.Modified;
        }

        async Task<IReadOnlyList<Product>> IProductRepository.GetProductsByBrandAndTypeAsync(string? Brand, string? Type, string? Sort)
        {
            IReadOnlyList<Product> Data;
            if (string.IsNullOrEmpty(Brand) && string.IsNullOrEmpty(Type))
            {
                Data = await _storeContext.products.ToListAsync();
            }
            else if (string.IsNullOrEmpty(Brand))
            {
                Data = await _storeContext.products.Where(p => p.Type == Type).ToListAsync();
            }
            else if (string.IsNullOrEmpty(Type))
            {
                Data = await _storeContext.products.Where(p => p.Brand == Brand).ToListAsync();
            }
            else
            {
                Data = await _storeContext.products.Where(p => p.Brand == Brand && p.Type == Type).ToListAsync();
            }
            if (!string.IsNullOrEmpty(Sort?.ToLower()))
            {
                Data = Sort switch {
                    "asc" => Data = Data.OrderBy(p => p.Price).ToList(),
                    "desc" => Data = Data.OrderByDescending(p => p.Price).ToList(),
                    _ => Data
                };
            }
            return Data;
        }
    }
}
