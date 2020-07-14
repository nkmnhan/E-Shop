using EShop.BackEnd.Services;
using EShop.Shared.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EShop.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _category;
        public CategoriesController(ICategoryService category)
        {
            _category = category;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _category.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _category.FindByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryCreateRequest request)
        {
            var result = await _category.AddAsync(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CategoryUpdateRequest request)
        {
            var result = await _category.UpdateAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _category.DeleteAsync(id);
            return Ok();
        }
    }
}
