using Household.Budget.Contracts.Data;
using NSubstitute;
using Household.Budget.Contracts.Models;
using Household.Budget.Contracts.Errors;
using FluentAssertions;
using Household.Budget.UseCases.Categories.CreateCategories;

namespace Household.Budget.UnitTests.UseCases.CreateCategories
{
    public class CreateCategoryHandlerTests
    {
        private readonly ICategoryRepository _repository;
        private readonly CreateCategoryHandler _handler;

        public CreateCategoryHandlerTests()
        {
            _repository = Substitute.For<ICategoryRepository>();
            _handler = new CreateCategoryHandler(_repository);
        }

        [Fact]
        public async Task Handle_WithValidRequest_CallsCreateAsyncAndReturnsCreatedResponse()
        {
            // Arrange
            var request = new CreateCategoryRequest("Category Name");
            var cancellationToken = new CancellationToken();

            // Act
            var response = await _handler.Handle(request, cancellationToken);

            // Assert
            await _repository.Received(1).CreateAsync(Arg.Any<Category>(), cancellationToken);
            response.Should().BeOfType<CreateCategoryResponse>();
        }

        [Fact]
        public async Task Handle_WithInvalidRequest_ReturnsErrorResponse()
        {
            // Arrange
            var invalidRequest = new CreateCategoryRequest("");
            var cancellationToken = new CancellationToken();

            // Act
            invalidRequest.Validate();
            var response = await _handler.Handle(invalidRequest, cancellationToken);

            // Assert
            response.Should().BeOfType<CreateCategoryResponse>();
            response.Errors.Should().Contain(error => error.Key == CategoryKnownErrors.CATEGORY_NAME_IS_REQUIRED.Key);
        }
    }
}