using AutoMapper;
using EShop.BackEnd.Data;
using EShop.BackEnd.Models;
using EShop.Shared.Requests;
using EShop.Shared.Response;
using EShop.Shared.ViewModels.Brand;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShop.BackEnd.Services
{
    public class BrandServive : IBrandService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BrandServive(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BrandCreateResponse> AddAsync(BrandCreateRequest request)
        {
            var brand = _mapper.Map<Brand>(request);

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();

            return _mapper.Map<BrandCreateResponse>(brand);

        }

        public async Task DeleteAsync(int brandId)
        {
            var brand = await _context.Brands.FindAsync(brandId);
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
        }

        public async Task<BrandVm> FindByIdAsync(int? brandId)
        {
            var result = await _context.Brands.FindAsync(brandId);
            return _mapper.Map<BrandVm>(result);
        }

        public async Task<IEnumerable<BrandVm>> GetAllAsync()
        {
            var result = await _context.Brands.ToListAsync();
            return _mapper.Map<IEnumerable<BrandVm>>(result);
        }

        public async Task<BrandUpdateResponse> UpdateAsync(BrandUpdateRequest request)
        {
            var brand = await _context.Brands.FindAsync(request.Id);
            brand.Name = request.Name;

            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();

            return _mapper.Map<BrandUpdateResponse>(brand);
        }
    }
}
