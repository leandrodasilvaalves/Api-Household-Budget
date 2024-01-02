using Household.Budget.UseCases.Subcategories.CreateSubcategory;

namespace Household.Budget.Contracts.Exceptions
{
    public class CreateSubcategoryException : AbstractConsumersExceptions<CreateSubcategoryResponse>
    {
        public CreateSubcategoryException(CreateSubcategoryResponse response) : base(response) { }
    }
}