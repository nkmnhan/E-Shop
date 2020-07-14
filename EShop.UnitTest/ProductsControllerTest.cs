using EShop.BackEnd.Controllers;
using EShop.Shared.Requests;
using EShop.Shared.Response;
using EShop.Shared.ViewModels.Product;
using EShop.UnitTest.Fixture;
using EShop.UnitTest.Fixture.Commons;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EShop.UnitTest
{
    [Collection(Constants.TestFixtureName)]
    public class ProductsControllerTest : BaseControllerTest
    {
        private readonly TestFixture _fixture;
        public ProductsControllerTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ProductsController_SearchProduct_Success()
        {
            var products = new List<ProductCreateRequest> {
                new ProductCreateRequest {
                    Name = "Test product1",
                    BrandId = _fixture.Brands[0].Id,
                    CategoryIds = new List<int>{ _fixture.Categories[0].Id}
                },
                new ProductCreateRequest {
                    Name = "Test product2",
                    BrandId = _fixture.Brands[0].Id,
                    CategoryIds = new List<int>{ _fixture.Categories[0].Id}
                }
            };

            var controller = new ProductsController(_fixture.ProductService);
            foreach (var product in products)
            {
                await controller.Post(product);
            }

            var searchResult = await controller.SearchProduct(0, 1, "Test product");
            var searchResponse = GetResponse<ProductsListVm>(searchResult);

            Assert.Equal(2, searchResponse.Products.Count());
            foreach (var productVm in searchResponse.Products)
            {
                var result = products.Where(x => x.Name == productVm.Name
                                              && x.BrandId == productVm.BrandId).FirstOrDefault();
                Assert.Equal(productVm.Categories.Select(x => x.Id), result.CategoryIds);
            }
        }

        [Fact]
        public async Task ProductsController_Get_Success()
        {
            var product = new ProductCreateRequest
            {
                Name = "Test product3",
                BrandId = _fixture.Brands[0].Id,
                Price = 123,
                ImageUrl = "http://image",
                Description = "description",
                CategoryIds = new List<int> { _fixture.Categories[0].Id }
            };

            var controller = new ProductsController(_fixture.ProductService);
            var creatResult = await controller.Post(product);
            var createResponse = GetResponse<ProductCreateResponse>(creatResult);

            Assert.Equal("Test product3", createResponse.Name);

            var getResult = await controller.Get(createResponse.Id);
            var getResponse = GetResponse<ProductVm>(getResult);

            Assert.Equal(getResponse.BrandId, product.BrandId);
            Assert.Equal(getResponse.Price, product.Price);
            Assert.Equal(getResponse.ImageUrl, product.ImageUrl);
            Assert.Equal(getResponse.Description, product.Description);
            Assert.Equal(getResponse.Categories.Select(x => x.Id), product.CategoryIds);
        }

        [Fact]
        public async Task ProductsController_Post_Success()
        {
            var product = new ProductCreateRequest
            {
                Name = "Test product4",
                Price = 123,
                ImageUrl = "http://image",
                Description = "description",
                BrandId = _fixture.Brands[0].Id,
                CategoryIds = new List<int> { _fixture.Categories[0].Id }
            };

            var controller = new ProductsController(_fixture.ProductService);
            var result = await controller.Post(product);
            var response = GetResponse<ProductCreateResponse>(result);

            Assert.Equal(product.Name, response.Name);
            Assert.Equal(product.Price, response.Price);
            Assert.Equal(product.ImageUrl, response.ImageUrl);
            Assert.Equal(product.Description, response.Description);
            Assert.Equal(product.BrandId, response.BrandId);
            Assert.Equal(product.CategoryIds, response.CategoryIds);
        }

        [Fact]
        public async Task ProductsController_Put_Success()
        {
            var createRequest = new ProductCreateRequest
            {
                Name = "Test product5",
                Price = 123,
                Description = "product5",
                ImageUrl = "http://image",
                BrandId = _fixture.Brands[0].Id,
                CategoryIds = new List<int> { _fixture.Categories[0].Id }
            };

            var controller = new ProductsController(_fixture.ProductService);
            var createResult = await controller.Post(createRequest);
            var createResponse = GetResponse<ProductCreateResponse>(createResult);

            Assert.Equal("Test product5", createResponse.Name);

            var updateRequest = new ProductUpdateRequest
            {
                Id = createResponse.Id,
                Name = "New Name",
                Price = 456,
                Description = "new new",
                ImageUrl = "http://new-image",
                BrandId = _fixture.Brands[2].Id,
                CategoryIds = new List<int> { _fixture.Categories[2].Id,
                                              _fixture.Categories[3].Id }
            };

            var updateResult = await controller.Put(updateRequest);
            var updateResponse = GetResponse<ProductUpdateResponse>(updateResult);

            Assert.Equal(updateRequest.Id, updateResponse.Id);
            Assert.Equal(updateRequest.Price, updateResponse.Price);
            Assert.Equal(updateRequest.ImageUrl, updateResponse.ImageUrl);
            Assert.Equal(updateRequest.BrandId, updateResponse.BrandId);
            Assert.Equal(updateRequest.CategoryIds, updateResponse.CategoryIds);
        }

        [Fact]
        public async Task ProductsController_Delete_Success()
        {
            var createRequest = new ProductCreateRequest { Name = "Test product6" };

            var controller = new ProductsController(_fixture.ProductService);
            var createResult = await controller.Post(createRequest);
            var createResponse = GetResponse<ProductCreateResponse>(createResult);

            Assert.Equal("Test product6", createResponse.Name);

            var deleteResult = await controller.Delete(createResponse.Id);
            Assert.IsType<OkResult>(deleteResult);

            var getResult = await controller.Get(createResponse.Id);
            var getOkResult = Assert.IsType<OkObjectResult>(getResult);
            Assert.Null(getOkResult.Value);
        }
    }
}
