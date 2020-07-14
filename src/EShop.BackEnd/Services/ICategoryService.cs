using EShop.Shared.Requests;
using EShop.Shared.Response;
using EShop.Shared.ViewModels.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.BackEnd.Services
{
    public interface ICategoryService
    {
        Task<CategoryCreateResponse> AddAsync(CategoryCreateRequest request);
        Task<IEnumerable<CategoryVm>> GetAllAsync();
        Task<CategoryVm> FindByIdAsync(int? categoryId);
        Task<CategoryUpdateResponse> UpdateAsync(CategoryUpdateRequest request);
        Task DeleteAsync(int categoryId);
    }
}
