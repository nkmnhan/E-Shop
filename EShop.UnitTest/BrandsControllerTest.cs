using EShop.BackEnd.Controllers;
using EShop.Shared.Requests;
using EShop.Shared.Response;
using EShop.Shared.ViewModels.Brand;
using EShop.UnitTest.Fixture;
using EShop.UnitTest.Fixture.Commons;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EShop.UnitTest
{
    [Collection(Constants.TestFixtureName)]
    public class BrandsControllerTest : BaseControllerTest
    {
        private readonly TestFixture _fixture;
        public BrandsControllerTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task BrandsController_GetAll_Success()
        {
            var brands = new List<BrandCreateRequest> {
                new BrandCreateRequest { Name = "Test brand1" },
                new BrandCreateRequest { Name = "Test brand2" }
            };

            var controller = new BrandsController(_fixture.BrandService);
            foreach (var brand in brands)
            {
                await controller.Post(brand);
            }

            var result = await controller.GetAll();
            var brandVms = GetCollectionResponse<BrandVm>(result);

            Assert.NotNull(brandVms);
            foreach (var brand in brands)
            {
                Assert.Contains(brandVms, x => x.Name == brand.Name);
            }
        }

        [Fact]
        public async Task BrandsController_Get_Success()
        {
            var brand = new BrandCreateRequest { Name = "Test brand3" };

            var controller = new BrandsController(_fixture.BrandService);
            var createResult = await controller.Post(brand);
            var createResponse = GetResponse<BrandCreateResponse>(createResult);

            Assert.Equal(brand.Name, createResponse.Name);

            var getResult = await controller.Get(createResponse.Id);
            var brandVm = GetResponse<BrandVm>(getResult);

            Assert.Equal(createResponse.Id, brandVm.Id);
            Assert.Equal(brand.Name, brandVm.Name);
        }

        [Fact]
        public async Task BrandsController_Post_Success()
        {
            var brand = new BrandCreateRequest { Name = "Test brand4" };

            var controller = new BrandsController(_fixture.BrandService);
            var result = await controller.Post(brand);
            var response = GetResponse<BrandCreateResponse>(result);

            Assert.Equal(brand.Name, response.Name);
        }

        [Fact]
        public async Task BrandsController_Put_Success()
        {
            var createRequest = new BrandCreateRequest { Name = "Test brand5" };

            var controller = new BrandsController(_fixture.BrandService);
            var createResult = await controller.Post(createRequest);
            var createResponse = GetResponse<BrandCreateResponse>(createResult);

            Assert.Equal(createRequest.Name, createResponse.Name);

            var updateRequest = new BrandUpdateRequest
            {
                Id = createResponse.Id,
                Name = "New Name"
            };
            var updateResult = await controller.Put(updateRequest);
            var updateResponse = GetResponse<BrandUpdateResponse>(updateResult);

            Assert.Equal(updateRequest.Id, updateResponse.Id);
            Assert.Equal(updateRequest.Name, updateResponse.Name);
        }

        [Fact]
        public async Task BrandsController_Delete_Success()
        {
            var createRequest = new BrandCreateRequest { Name = "Test brand6" };

            var controller = new BrandsController(_fixture.BrandService);
            var createResult = await controller.Post(createRequest);

            var createResponse = GetResponse<BrandCreateResponse>(createResult);
            Assert.Equal("Test brand6", createResponse.Name);

            var deleteResult = await controller.Delete(createResponse.Id);
            Assert.IsType<OkResult>(deleteResult);

            var getResult = await controller.Get(createResponse.Id);
            var getOkResult = Assert.IsType<OkObjectResult>(getResult);
            Assert.Null(getOkResult.Value);
        }
    }
}
