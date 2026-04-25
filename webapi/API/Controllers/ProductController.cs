using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace API.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private IGenericRepository<Product> _productRepository { get; set; }
        public ProductController(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("getData")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetData()
        {
            return Ok(await _productRepository.GetDataAsync());
        }

        [HttpGet("Brandnames")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrandTypes()
        {
            BrandListSpecification brandspec = new BrandListSpecification();
            return Ok(await _productRepository.GetDataWithSpec(brandspec));
        }

        [HttpGet("ProductTypes")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetProductTypes()
        {
            TypeListSpecification typsSpec = new TypeListSpecification();
            return Ok(await _productRepository.GetDataWithSpec(typsSpec));
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await _productRepository.AddDataObj(product);
            await _productRepository.saveChanges();
            return CreatedAtAction("CreateProduct", new { id = product.Id });
        }

        [HttpPut("id:int")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (id != product.Id) return NotFound();
            await _productRepository.UpdateObj(product);
            if (await _productRepository.saveChanges()) return NoContent();
            return product;
        }

        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult<Product>> DeleteProduct([FromQuery] int id)
        {
            var product = await _productRepository.GetDataByIdAsync(id);
            if (!_productRepository.DataExists(id)) return NotFound();
            _productRepository.DeleteObj(product);
            await _productRepository.saveChanges();
            return Ok("Deleted");
        }

        [HttpGet("productByBrandAndType")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProductByBrand([FromQuery] string? Brand, [FromQuery] string? Type, [FromQuery] string? Sort)
        {
            var spec = new ProductSpecification(Brand, Type, Sort);
            return Ok(await _productRepository.GetDataWithSpec(spec));
        }

        [HttpGet("GetEntityBySpecification")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetEntityBySpecification(ISpecification<Product> spec)
        {
            return Ok(await _productRepository.GetDataWithSpec(spec));
        }

        [HttpGet("DecryptDataBySalt")]
        public async Task<ActionResult<string>> DecryptDataBySalt([FromQuery] string EncryptedData, [FromQuery] string Salt)
        {
            Byte[] DataBytes = Convert.FromBase64String(EncryptedData);
            Byte[] SaltBytes = Convert.FromBase64String(Salt);
            return Ok();
        }
    }
}