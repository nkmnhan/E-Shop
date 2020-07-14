using EShop.Shared.ViewModels.Common;
using System.Collections.Generic;

namespace EShop.Shared.ViewModels.Product
{
    public class ProductsListVm
    {
        public IEnumerable<ProductVm> Products { get; set; }
        public PagingVm PagingInfo { get; set; }
    }
}
