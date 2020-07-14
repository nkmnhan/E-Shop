using EShop.BackEnd.Services;
using EShop.Shared.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EShop.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brand;
        public BrandsController(IBrandService brand)
        {
            _brand = brand;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _brand.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _brand.FindByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BrandCreateRequest request)
        {
            var result = await _brand.AddAsync(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] BrandUpdateRequest request)
        {
            var result = await _brand.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _brand.DeleteAsync(id);
            return Ok();
        }
    }
}
