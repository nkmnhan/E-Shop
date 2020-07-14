using EShop.Shared.Requests;
using EShop.Shared.Response;
using EShop.Shared.ViewModels.Product;
using System.Threading.Tasks;

namespace EShop.BackEnd.Services
{
    public interface IProductService
    {
        Task<ProductCreateResponse> AddAsync(ProductCreateRequest product);
        Task<ProductVm> FindByIdAsync(int? productId);
        Task<ProductUpdateResponse> UpdateAsync(ProductUpdateRequest product);
        Task DeleteAsync(int productId);
        Task<ProductsListVm> SearchProductAsync(int categoryId, int pageIndex, string searchContent);
    }
}
