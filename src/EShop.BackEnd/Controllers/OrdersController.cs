using EShop.BackEnd.Models;
using EShop.BackEnd.Services;
using EShop.Shared.Commons;
using EShop.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [Authorize("Bearer")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;

        public OrdersController(IOrderService orderService,
            UserManager<User> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAllOrders();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int orderId)
        {
            var result = await _orderService.GetOrdersById(orderId);
            return Ok(result);
        }

        [HttpGet("/user/{id}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var result = await _orderService.GetOrdersByUserId(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] OrderCreateRequest request)
        {
            try
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    return BadRequest(ErrorCodes.INVALID_REQUEST);
                }

                if (request.Lines == null || !request.Lines.Any())
                {
                    return BadRequest(ErrorCodes.ORDER_NO_PRODUCT_IN_CART);
                }
                if (ModelState.IsValid)
                {
                    var result = await _orderService.SaveOrderAsync(request, user);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(ErrorCodes.ORDER_MISSING_ADDRESS_OR_PHONENUMBER);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }
        }
    }
}
