using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Infrastructure.Data
{
    public class storeContextSeed
    {
        private readonly StoreContext _storeContext;
        //storeContextSeed(StoreContext storeContext)
        //{
        //    _storeContext = storeContext;
        //}
        public static async Task Seeddata(StoreContext context)
        {
            if (!context.products.Any())
            {
                var products = await File.ReadAllTextAsync("../infrastructure/Data/SeedData/product.json");
                if (products != null)
                {
                    var productsData = JsonSerializer.Deserialize<List<Product>>(products);
                    if (productsData != null)  await context.AddRangeAsync(productsData);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
