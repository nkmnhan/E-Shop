using AutoMapper;
using EShop.BackEnd.Data;
using EShop.BackEnd.Models;
using EShop.Shared.Requests;
using EShop.Shared.Response;
using EShop.Shared.ViewModels.Order;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.BackEnd.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderVm>> GetAllOrders()
        {
            var orders = await _context.Orders
                    .Include(o => o.Lines)
                    .ThenInclude(l => l.Product)
                    .ToListAsync();

            return _mapper.Map<IEnumerable<OrderVm>>(orders);
        }

        public async Task<OrderVm> GetOrdersById(int orderId)
        {
            var orders = await _context.Orders
                    .Include(o => o.Lines)
                    .ThenInclude(l => l.Product)
                    .FirstOrDefaultAsync(x => x.OrderID == orderId);

            return _mapper.Map<OrderVm>(orders);
        }

        public async Task<IEnumerable<OrderVm>> GetOrdersByUserId(string userId)
        {
            var orders = await _context.Orders
                        .Where(x => x.User.Id == userId)
                        .Include(o => o.Lines)
                        .ThenInclude(l => l.Product)
                        .ToListAsync();

            return _mapper.Map<IEnumerable<OrderVm>>(orders);
        }

        public async Task<OrderCreateResponse> SaveOrderAsync(OrderCreateRequest request, User user)
        {
            var order = _mapper.Map<Order>(request);
            order.User = user;

            foreach (var line in order.Lines)
            {
                line.Product = await _context.Products.FindAsync(line.Product.Id);
            }

            _context.AttachRange(order.Lines);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new OrderCreateResponse { Id = order.OrderID };
        }
    }
}
