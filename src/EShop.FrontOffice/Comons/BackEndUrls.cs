namespace EShop.FrontOffice.Comons
{
    public static class BackEndUrls
    {
        public static string SaveOrder() => "api/orders";
        public static string GetProductByIdUrl(int productId) => $"api/products/{productId}";

        public static string GetProductsUrl(int categoryId, int pageIndex, string searchContent)
            => $"api/products?categoryId={categoryId}&&pageIndex={pageIndex}&&searchContent={searchContent}";

        public static string GetCategoryUrl() => $"api/categories";
    }
}
