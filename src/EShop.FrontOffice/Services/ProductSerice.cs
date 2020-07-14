using EShop.FrontOffice.Comons;
using EShop.Shared.ViewModels.Product;
using System.Net.Http;
using System.Threading.Tasks;

namespace EShop.FrontOffice.Services
{
    public class ProductSerice : IProductSerice
    {
        private readonly IApiServive _apiServive;
        public ProductSerice(IApiServive apiServive)
        {
            _apiServive = apiServive;
        }

        public async Task<ProductVm> GetProductById(int productId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, BackEndUrls.GetProductByIdUrl(productId));

            return await _apiServive.SendAsync<ProductVm>(request);
        }

        public async Task<ProductsListVm> GetProducts(int categoryId, int pageIndex, string searchContent)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, BackEndUrls.GetProductsUrl(categoryId, pageIndex, searchContent));

            return await _apiServive.SendAsync<ProductsListVm>(request);
        }
    }
}
