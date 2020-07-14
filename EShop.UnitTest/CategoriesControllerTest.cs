using EShop.BackEnd.Controllers;
using EShop.Shared.Requests;
using EShop.Shared.Response;
using EShop.Shared.ViewModels.Category;
using EShop.UnitTest.Fixture;
using EShop.UnitTest.Fixture.Commons;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EShop.UnitTest
{
    [Collection(Constants.TestFixtureName)]
    public class CategoriesControllerTest : BaseControllerTest
    {
        private readonly TestFixture _fixture;
        public CategoriesControllerTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CategoriesController_GetAll_Success()
        {
            var categories = new List<CategoryCreateRequest> {
                new CategoryCreateRequest { Name = "Test category1" },
                new CategoryCreateRequest { Name = "Test category2" }
            };

            var controller = new CategoriesController(_fixture.CategoryService);
            foreach (var category in categories)
            {
                await controller.Post(category);
            }

            var getResult = await controller.GetAll();
            var response = GetCollectionResponse<CategoryVm>(getResult);

            Assert.NotNull(response);
            foreach (var category in categories)
            {
                Assert.Contains(response, x => x.Name == category.Name);
            }
        }

        [Fact]
        public async Task CategoriesController_Get_Success()
        {
            var category = new CategoryCreateRequest { Name = "Test category3" };

            var controller = new CategoriesController(_fixture.CategoryService);
            var createResult = await controller.Post(category);
            var createResponse = GetResponse<CategoryCreateResponse>(createResult);

            Assert.Equal(category.Name, createResponse.Name);

            var getResult = await controller.Get(createResponse.Id);
            var getResponse = GetResponse<CategoryVm>(getResult);

            Assert.Equal(createResponse.Id, getResponse.Id);
            Assert.Equal(createResponse.Name, getResponse.Name);
        }

        [Fact]
        public async Task CategoriesController_Post_Success()
        {
            var category = new CategoryCreateRequest { Name = "Test category4" };

            var controller = new CategoriesController(_fixture.CategoryService);
            var result = await controller.Post(category);

            var response = GetResponse<CategoryCreateResponse>(result);
            Assert.Equal(category.Name, response.Name);
        }

        [Fact]
        public async Task CategoriesController_Put_Success()
        {
            var createRequest = new CategoryCreateRequest { Name = "Test category5" };

            var controller = new CategoriesController(_fixture.CategoryService);
            var createResult = await controller.Post(createRequest);
            var createResponse = GetResponse<CategoryCreateResponse>(createResult);

            Assert.Equal(createRequest.Name, createResponse.Name);

            var updateRequest = new CategoryUpdateRequest
            {
                Id = createResponse.Id,
                Name = "New Name"
            };
            var updateResult = await controller.Put(updateRequest);
            var updateResponse = GetResponse<CategoryUpdateResponse>(updateResult);

            Assert.Equal(updateRequest.Id, updateResponse.Id);
            Assert.Equal(updateRequest.Name, updateResponse.Name);
        }

        [Fact]
        public async Task CategoriesController_Delete_Success()
        {
            var createRequest = new CategoryCreateRequest { Name = "Test category6" };

            var controller = new CategoriesController(_fixture.CategoryService);
            var createResult = await controller.Post(createRequest);
            var createResponse = GetResponse<CategoryCreateResponse>(createResult);

            Assert.Equal(createRequest.Name, createResponse.Name);

            var deleteResult = await controller.Delete(createResponse.Id);

            Assert.IsType<OkResult>(deleteResult);

            var getResult = await controller.Get(createResponse.Id);
            var getOkResult = Assert.IsType<OkObjectResult>(getResult);
            Assert.Null(getOkResult.Value);
        }
    }
}
