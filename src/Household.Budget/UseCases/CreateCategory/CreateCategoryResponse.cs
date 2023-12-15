using Household.Budget.Contracts.Http.Responses;
using Household.Budget.Contracts.Models;

namespace Household.Budget.UseCases.CreateCategory;

public class CreateCategoryResponse : Response<Category>
{
    public CreateCategoryResponse(Category category) : base(category)
    {
    }
}