using FluentAssertions;

using Household.Budget.Api.Controllers.Middlewares;

using Microsoft.AspNetCore.Http;

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
            context.Response.StatusCode.Should().Be(200);
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
            context.Response.StatusCode.Should().Be(400);
            context.Response.ContentType.Should().Be("application/json; charset=utf-8");
        }
    }
}