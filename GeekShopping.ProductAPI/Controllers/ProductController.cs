using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new
                ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            var products = await _productRepository.FindAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductVO>> FindById(long id)
        {
            var product = await _productRepository.FindById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
    }
}
