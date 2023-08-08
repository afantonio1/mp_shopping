using Microsoft.AspNetCore.Mvc;
using MpShopping.Web.Models.Data.Dto;
using MpShopping.Web.Models.Interfaces;

namespace MpShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> FindAll()
        {
            var products = await _productRepository.FindAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> FindById(long id)
        {
            var product = await _productRepository.FindById(id);
            if (product.Id <= 0) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create([FromBody] ProductDTO productDto)
        {
            if (productDto == null) return BadRequest();
            var product = await _productRepository.Create(productDto);
            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<ProductDTO>> Update([FromBody] ProductDTO productDto)
        {
            if (productDto == null) return BadRequest();
            var product = await _productRepository.Update(productDto);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _productRepository.Delete(id);
            if (status == false) return NotFound();

            return Ok(status);
        }
    }
}