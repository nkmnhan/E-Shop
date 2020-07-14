using EShop.BackEnd.Controllers;
using EShop.BackEnd.Models;
using EShop.Shared.Commons;
using EShop.Shared.Requests;
using EShop.Shared.Response;
using EShop.Shared.ViewModels.Order;
using EShop.UnitTest.Fixture;
using EShop.UnitTest.Fixture.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace EShop.UnitTest
{
    [Collection(Constants.TestFixtureName)]
    public class OrdersControllerTest : BaseControllerTest
    {
        private readonly TestFixture _fixture;
        public OrdersControllerTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task OrdersController_Checkout_Success()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, _fixture.Users[0].UserName),
                new Claim(ClaimTypes.NameIdentifier, _fixture.Users[0].Id)
            }, "mock"));

            var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<User>>();
            var controller = new OrdersController(_fixture.OrderService, userManager)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var request = new OrderCreateRequest
            {
                Address = "ABC Street, XYZ City",
                PhoneNumber = "123 456 789",
                Lines = _fixture.Products.Select(x => new CartLineCreateRequest
                {
                    ProductId = x.Id,
                    Quantity = 10
                }).ToList()
            };

            var result = await controller.Checkout(request);
            var response = GetResponse<OrderCreateResponse>(result);

            Assert.True(response.Id > 0);
        }

        [Fact]
        public async Task OrdersController_GetAllOrders_Success()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, _fixture.Users[0].UserName),
                new Claim(ClaimTypes.NameIdentifier, _fixture.Users[0].Id)
            }, "mock"));

            var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<User>>();
            var controller = new OrdersController(_fixture.OrderService, userManager)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var createRequests = new List<OrderCreateRequest>
            {
                new OrderCreateRequest
                {
                    Address = "ABC Street, XYZ City",
                    PhoneNumber = "123 456 789",
                    Lines = _fixture.Products.Skip(0).Take(3).Select(x => new CartLineCreateRequest
                    {
                        ProductId = x.Id,
                        Quantity = 10
                    }).ToList()
                },
                new OrderCreateRequest
                {
                    Address = "DEF Street, OPR City",
                    PhoneNumber = "+84 111 222 333",
                    Lines = _fixture.Products.Skip(3).Take(1).Select(x => new CartLineCreateRequest
                    {
                        ProductId = x.Id,
                        Quantity = 4
                    }).ToList()
                },
            };

            foreach (var request in createRequests)
            {
                var createResult = await controller.Checkout(request);
                var createResponse = GetResponse<OrderCreateResponse>(createResult);
                Assert.True(createResponse.Id > 0);
            }

            var getResult = await controller.GetAll();
            var getRespone = GetCollectionResponse<OrderVm>(getResult);

            Assert.Equal(2, getRespone.Count());
            foreach (var request in createRequests)
            {
                var actualOrder = getRespone.FirstOrDefault(x => x.Address == request.Address
                                                         && x.PhoneNumber == request.PhoneNumber
                                                         && x.Lines.Count() == request.Lines.Count());
                Assert.NotNull(actualOrder);

                foreach (var line in request.Lines)
                {
                    var actualCartLine = actualOrder.Lines
                         .FirstOrDefault(x => x.Product.Id == line.ProductId
                                        && x.Quantity == line.Quantity);
                    Assert.NotNull(actualCartLine);
                }
            }
        }

        [Fact]
        public async Task OrdersController_GetByUserId_Success()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, _fixture.Users[1].UserName),
                new Claim(ClaimTypes.NameIdentifier, _fixture.Users[1].Id)
            }, "mock"));

            var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<User>>();
            var controller = new OrdersController(_fixture.OrderService, userManager)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var createRequests = new List<OrderCreateRequest>
            {
                new OrderCreateRequest
                {
                    Address = "ABC Street, XYZ City",
                    PhoneNumber = "123 456 789",
                    Lines = _fixture.Products.Skip(0).Take(3).Select(x => new CartLineCreateRequest
                    {
                        ProductId = x.Id,
                        Quantity = 10
                    }).ToList()
                },
                new OrderCreateRequest
                {
                    Address = "DEF Street, OPR City",
                    PhoneNumber = "+84 111 222 333",
                    Lines = _fixture.Products.Skip(3).Take(1).Select(x => new CartLineCreateRequest
                    {
                        ProductId = x.Id,
                        Quantity = 4
                    }).ToList()
                },
            };

            foreach (var request in createRequests)
            {
                var createResult = await controller.Checkout(request);
                var createResponse = GetResponse<OrderCreateResponse>(createResult);
                Assert.True(createResponse.Id > 0);
            }

            var getResult = await controller.GetByUserId(_fixture.Users[1].Id);
            var getRespone = GetCollectionResponse<OrderVm>(getResult);

            Assert.True(getRespone.Count() == 2);
            foreach (var request in createRequests)
            {
                var actualOrder = getRespone.FirstOrDefault(x => x.Address == request.Address
                                                         && x.PhoneNumber == request.PhoneNumber
                                                         && x.Lines.Count() == request.Lines.Count());
                Assert.NotNull(actualOrder);

                foreach (var line in request.Lines)
                {
                    var actualCartLine = actualOrder.Lines
                         .FirstOrDefault(x => x.Product.Id == line.ProductId
                                        && x.Quantity == line.Quantity);
                    Assert.NotNull(actualCartLine);
                }
            }
        }

        [Fact]
        public async Task OrdersController_GetById_Success()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, _fixture.Users[0].UserName),
                new Claim(ClaimTypes.NameIdentifier, _fixture.Users[0].Id)
            }, "mock"));

            var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<User>>();
            var controller = new OrdersController(_fixture.OrderService, userManager)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var request = new OrderCreateRequest
            {
                Address = "ABC Street, XYZ City",
                PhoneNumber = "123 456 789",
                Lines = _fixture.Products.Select(x => new CartLineCreateRequest
                {
                    ProductId = x.Id,
                    Quantity = 10
                }).ToList()
            };

            var createResult = await controller.Checkout(request);
            var createResponse = GetResponse<OrderCreateResponse>(createResult);

            Assert.True(createResponse.Id > 0);

            var getResult = await controller.GetById(createResponse.Id);
            var getResponse = GetResponse<OrderVm>(getResult);

            Assert.Equal(createResponse.Id, getResponse.OrderID);
        }

        [Fact]
        public async Task OrdersController_Checkout_Invalid_Request()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "User"),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            }, "mock"));

            var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<User>>();
            var controller = new OrdersController(_fixture.OrderService, userManager)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var request = new OrderCreateRequest
            {
                Address = "ABC Street, XYZ City",
                PhoneNumber = "123 456 789",
                Lines = _fixture.Products.Select(x => new CartLineCreateRequest
                {
                    ProductId = x.Id,
                    Quantity = 10
                }).ToList()
            };

            var result = await controller.Checkout(request);

            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(badResult.Value.ToString(), ErrorCodes.INVALID_REQUEST);
        }

        [Fact]
        public async Task OrdersController_Checkout_Order_No_Product_In_Cart()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, _fixture.Users[0].UserName),
                new Claim(ClaimTypes.NameIdentifier, _fixture.Users[0].Id)
            }, "mock"));

            var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<User>>();
            var controller = new OrdersController(_fixture.OrderService, userManager)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };

            var request = new OrderCreateRequest
            {
                Address = "ABC Street, XYZ City",
                PhoneNumber = "123 456 789"
            };

            var result = await controller.Checkout(request);

            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(badResult.Value.ToString(), ErrorCodes.ORDER_NO_PRODUCT_IN_CART);
        }

        [Fact]
        public async Task OrdersController_Checkout_Order_Missing_Address_Or_Phonenumber()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, _fixture.Users[0].UserName),
                new Claim(ClaimTypes.NameIdentifier, _fixture.Users[0].Id)
            }, "mock"));

            var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<User>>();
            var controller = new OrdersController(_fixture.OrderService, userManager)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };
            controller.ModelState.AddModelError("fakeError", "fakeError");

            var request = new OrderCreateRequest
            {
                Address = null,
                PhoneNumber = null,
                Lines = _fixture.Products.Select(x => new CartLineCreateRequest
                {
                    ProductId = x.Id,
                    Quantity = 10
                }).ToList()
            };

            var result = await controller.Checkout(request);

            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(badResult.Value.ToString(), ErrorCodes.ORDER_MISSING_ADDRESS_OR_PHONENUMBER);
        }
    }
}
