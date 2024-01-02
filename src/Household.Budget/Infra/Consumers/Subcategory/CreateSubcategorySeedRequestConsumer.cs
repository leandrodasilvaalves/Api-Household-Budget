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

        public Task Consume(ConsumeContext<CreateSubcategoryRequest> context) =>
            _createSubcategoryHandler.HandleAsync(context.Message, context.CancellationToken);
            //TODO: lancar exception em caso de erro
    }
}