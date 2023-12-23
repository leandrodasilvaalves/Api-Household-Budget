using MediatR;

namespace Household.Budget.UseCases.Categories.ListCategories;

public class ListCategoriesRequest : Request, IRequest<ListCategoriesResponse>
{
    public ListCategoriesRequest(int pageSize = 20, int pageNumber = 1)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }

    public int PageSize { get; set; }
    public int PageNumber { get; set; }

    public override void Validate()
    {
        throw new NotImplementedException();
    }
}
