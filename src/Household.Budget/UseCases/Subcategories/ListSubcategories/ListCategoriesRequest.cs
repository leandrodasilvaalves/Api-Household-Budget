using MediatR;

namespace Household.Budget.UseCases.Subcategories.ListSubcategories;

public class ListSubcategoriesRequest : Request, IRequest<ListSubcategoriesResponse>
{
    public int PageSize { get; set; } = 20;
    public int PageNumber { get; set; } = 1;

    public override void Validate()
        => AddNotifications(new ListSubcategoriesRequestContract(this));
}
