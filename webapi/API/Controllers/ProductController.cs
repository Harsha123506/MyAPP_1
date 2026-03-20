using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private StoreContext StoreContext { get; set; }
        public ProductController(StoreContext store)
        { 
            StoreContext = store;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetData()
        {
            var products = await StoreContext.products.ToListAsync();
            return products;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await StoreContext.products.AddAsync(product);
            await StoreContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("id:int")]
        public async Task<ActionResult<Product>> UpdateProduct(int id , Product product)
        {
            if (id != product.Id) return NotFound();
            StoreContext.Entry(product).State = EntityState.Modified;  //Updates all fields if not specified as Nulls.
            await StoreContext.SaveChangesAsync();
            return product;
        }


    }
}