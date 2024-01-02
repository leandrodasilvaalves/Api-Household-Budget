using Household.Budget.Contracts.Events;
using Household.Budget.Contracts.Exceptions;
using Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;

using MassTransit;

namespace Household.Budget.Infra.Consumers;

public class SubcategoryWasCreatedConsumer : IConsumer<SubcategoryWasCreated>
{
    private readonly IAttachSubcategoryEventHandler _handler;

    public SubcategoryWasCreatedConsumer(IAttachSubcategoryEventHandler handler) =>
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    public async Task Consume(ConsumeContext<SubcategoryWasCreated> context)
    {
        var response = await _handler.HandleAsync(context.Message, context.CancellationToken);
        if (response.IsSuccess == false)
            throw new AttachSubcategoryExecption(response);
    }
}