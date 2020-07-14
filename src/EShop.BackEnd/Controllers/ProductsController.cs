using EShop.BackEnd.Services;
using EShop.Shared.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EShop.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> SearchProduct(int categoryId, int pageIndex = 1, string searchContent = "")
        {
            var result = await _productService.SearchProductAsync(categoryId, pageIndex, searchContent);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productService.FindByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCreateRequest request)
        {
            var result = await _productService.AddAsync(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductUpdateRequest request)
        {
            var result = await _productService.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }
    }
}
