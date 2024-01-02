using Household.Budget.Contracts.Exceptions;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;

using MassTransit;

namespace Household.Budget.Infra.Consumers.Subcategory
{
    public class CreateSubcategorySeedRequestConsumer : IConsumer<CreateSubcategoryRequest>
    {
        private readonly ICreateSubcategoryHandler _createSubcategoryHandler;

        public CreateSubcategorySeedRequestConsumer(ICreateSubcategoryHandler createSubcategoryHandler)
        {
            _createSubcategoryHandler = createSubcategoryHandler ?? throw new ArgumentNullException(nameof(createSubcategoryHandler));
        }

        public async Task Consume(ConsumeContext<CreateSubcategoryRequest> context)
        {
            var response = await  _createSubcategoryHandler.HandleAsync(context.Message, context.CancellationToken);
            if(response.IsSuccess is false)
                throw new CreateSubcategoryException(response);
        }
    }
}