using EShop.Shared.Requests;
using EShop.Shared.Response;
using EShop.Shared.ViewModels.Brand;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.BackEnd.Services
{
    public interface IBrandService
    {
        Task<BrandCreateResponse> AddAsync(BrandCreateRequest request);
        Task<IEnumerable<BrandVm>> GetAllAsync();
        Task<BrandVm> FindByIdAsync(int? brandId);
        Task<BrandUpdateResponse> UpdateAsync(BrandUpdateRequest request);
        Task DeleteAsync(int brandId);
    }
}
