using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace EShop.UnitTest.Fixture
{
    public class BaseControllerTest
    {
        protected TResponse GetResponse<TResponse>(IActionResult actionResult)
            where TResponse : class
        {
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            return Assert.IsType<TResponse>(okResult.Value);
        }

        protected IEnumerable<TResponse> GetCollectionResponse<TResponse>(IActionResult actionResult)
            where TResponse : class
        {
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            return Assert.IsAssignableFrom<IEnumerable<TResponse>>(okResult.Value);
        }
    }
}
