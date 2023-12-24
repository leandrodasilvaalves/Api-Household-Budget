using MediatR;

namespace Household.Budget.UseCases.Categories.ListCategories;

public class ListCategoriesRequest : Request, IRequest<ListCategoriesResponse>
{
    public int PageSize { get; set; } = 20;
    public int PageNumber { get; set; } = 1;

    public override void Validate()
        => AddNotifications(new ListCategoriesRequestContract(this));
}
