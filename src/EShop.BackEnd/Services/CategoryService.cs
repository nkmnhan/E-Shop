using AutoMapper;
using EShop.BackEnd.Data;
using EShop.BackEnd.Models;
using EShop.Shared.Requests;
using EShop.Shared.Response;
using EShop.Shared.ViewModels.Category;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.BackEnd.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryCreateResponse> AddAsync(CategoryCreateRequest request)
        {
            var category = _mapper.Map<Category>(request);

            _context.Add(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryCreateResponse>(category);
        }

        public async Task DeleteAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<CategoryVm> FindByIdAsync(int? categoryId)
        {
            var result = await _context.Categories.FindAsync(categoryId);
            return _mapper.Map<CategoryVm>(result);
        }

        public async Task<IEnumerable<CategoryVm>> GetAllAsync()
        {
            var result = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryVm>>(result);
        }

        public async Task<CategoryUpdateResponse> UpdateAsync(CategoryUpdateRequest request)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            category.Name = request.Name;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryUpdateResponse>(category);
        }
    }
}
