using EShop.Shared.ViewModels.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.FrontOffice.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryVm>> GetCategories();
    }
}
