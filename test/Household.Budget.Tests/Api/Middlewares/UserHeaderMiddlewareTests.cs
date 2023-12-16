using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Household.Budget.Api.Controllers.Middlewares;

using Microsoft.AspNetCore.Http;

using Xunit;

namespace Household.Budget.Tests.Api.Middlewares
{
    public class UserHeaderMiddlewareTests
    {
        [Fact]
        public async Task Invoke_WhenUserHeaderIsPresent_ShouldCallNext()
        {
            // Arrange
            var middleware = new UserHeaderMiddleware(next: context => Task.CompletedTask);
            var context = new DefaultHttpContext();
            context.Request.Headers[KnownHeaders.USER_HEADER] = "some-value";

            // Act
            await middleware.Invoke(context);

            // Assert
            Assert.Equal(200, context.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_WhenUserHeaderIsNotPresent_ShouldReturnErrorResponse()
        {
            // Arrange
            var middleware = new UserHeaderMiddleware(next: context => Task.CompletedTask);
            var context = new DefaultHttpContext();

            // Act
            await middleware.Invoke(context);

            // Assert
            Assert.Equal(400, context.Response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", context.Response.ContentType);
        }
    }
}