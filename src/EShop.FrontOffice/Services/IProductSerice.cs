using EShop.Shared.ViewModels.Product;
using System.Threading.Tasks;

namespace EShop.FrontOffice.Services
{
    public interface IProductSerice
    {
        public Task<ProductsListVm> GetProducts(int categoryId, int pageIndex, string searchContent);
        public Task<ProductVm> GetProductById(int productId);
    }
}
