using EShop.FrontOffice.Comons;
using EShop.Shared.ViewModels.Category;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EShop.FrontOffice.Services
{
    public class CategorySerice : ICategoryService
    {
        private readonly IApiServive _apiServive;
        public CategorySerice(IApiServive apiServive)
        {
            _apiServive = apiServive;
        }
        public async Task<IEnumerable<CategoryVm>> GetCategories()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, BackEndUrls.GetCategoryUrl());
            return await _apiServive.SendAsync<IEnumerable<CategoryVm>>(request);
        }
    }
}
