using Flunt.Notifications;

using Household.Budget.Contracts.Http.Responses;


namespace Household.Budget.UseCases.Categories.ImportCategorySeed;

public class ImportCategorySeedResponse : Response<ImportCategorySeedRequest>
{
    public ImportCategorySeedResponse(ImportCategorySeedRequest data) : base(data) { }

    public ImportCategorySeedResponse(IEnumerable<Notification> errors) : base(errors) { }
}