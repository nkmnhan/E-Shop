using AutoMapper;
using EShop.BackEnd.Commons;
using EShop.BackEnd.Data;
using EShop.BackEnd.Models;
using EShop.Shared.Requests;
using EShop.Shared.Response;
using EShop.Shared.ViewModels.Common;
using EShop.Shared.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.BackEnd.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context,
                              IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductVm> FindByIdAsync(int? productId)
        {
            var result = await _context.Products.Where(x => x.Id == productId)
                .Include(x => x.Brand)
                .Include(x => x.ProductCategories)
                .ThenInclude(x => x.Category)
                .FirstOrDefaultAsync();
            return _mapper.Map<ProductVm>(result);
        }

        public async Task<ProductCreateResponse> AddAsync(ProductCreateRequest request)
        {
            var product = _mapper.Map<Product>(request);

            product.Brand = await _context.Brands.FindAsync(product.Brand.Id);
            _context.Add(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductCreateResponse>(product);
        }

        public async Task<ProductUpdateResponse> UpdateAsync(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);

            product.Brand = await _context.Brands.FindAsync(request.BrandId);

            var productCategory = await _context.ProductCategory
                                                .Where(pc => pc.ProductId == product.Id)
                                                .ToListAsync();
            _context.ProductCategory
                    .RemoveRange(productCategory);

            product.ProductCategories = _context.Categories
                                                .Where(x => request.CategoryIds
                                                                   .Contains(x.Id))
                                                .Select(x => new ProductCategory { Category = x })
                                                .ToList();

            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;
            product.ImageUrl = request.ImageUrl;

            _context.Products.Update(product);

            await _context.SaveChangesAsync();

            return _mapper.Map<ProductUpdateResponse>(product);
        }

        public async Task DeleteAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductsListVm> SearchProductAsync(int categoryId, int pageIndex, string searchContent)
        {
            IQueryable<ProductCategory> query = _context.ProductCategory;
            if (categoryId > 0)
            {
                query = query.Where(pc => pc.CategoryId == categoryId);
            }

            if (!string.IsNullOrEmpty(searchContent))
            {
                query = query.Where(pc => pc.Product.Name.ToLower().Contains(searchContent.ToLower()));
            }

            var productAmount = query.Select(x => x.Product);

            var products = await query.Select(x => x.Product)
                              .OrderBy(x => x.Id)
                              .Skip((pageIndex - 1) * Constants.PageSize)
                              .Take(Constants.PageSize)
                              .ToListAsync();

            return new ProductsListVm
            {
                Products = _mapper.Map<IEnumerable<ProductVm>>(products),
                PagingInfo = new PagingVm
                {
                    CurrentPage = pageIndex,
                    ItemsPerPage = Constants.PageSize,
                    TotalItems = productAmount.Count()
                }
            };
        }
    }
}
