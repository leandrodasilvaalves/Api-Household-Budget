using Household.Budget.UseCases.Categories.CreateCategories;
using Household.Budget.UseCases.Categories.EventHandlers.AttachSubcategory;
using Household.Budget.UseCases.Categories.EventHandlers.DetachSubcategory;
using Household.Budget.UseCases.Categories.EventHandlers.SubcategoryChangeCategory;
using Household.Budget.UseCases.Categories.GetCategoryById;
using Household.Budget.UseCases.Categories.GetSubcategoryById;
using Household.Budget.UseCases.Categories.ImportCategorySeed;
using Household.Budget.UseCases.Categories.ListCategories;
using Household.Budget.UseCases.Categories.UpdateCategory;
using Household.Budget.UseCases.Categories.UpdateSubcategory;
using Household.Budget.UseCases.Identity.CreateAdminUser;
using Household.Budget.UseCases.Identity.LoginUser;
using Household.Budget.UseCases.Identity.RegisterUser;
using Household.Budget.UseCases.MonthlyBudgets.CreateMonthlyBudget;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;
using Household.Budget.UseCases.Subcategories.ListSubcategories;
using Household.Budget.UseCases.Transactions.CreateTransaction;
using Household.Budget.UseCases.Transactions.GetTransactionById;
using Household.Budget.UseCases.Transactions.ListTransactions;
using Household.Budget.UseCases.Transactions.UpdateTransaction;

namespace Household.Budget.Api.Extensions;

public static class UseCasesExtensions
{
    public static void AddUseCases(this IServiceCollection services)
    {
        services.AddIdentity();
        services.AddCategories();
        services.AddSubcategories();
        services.AddTransactions();
        services.AddMonthlyBudget();
    }

    private static void AddIdentity(this IServiceCollection services)
    {
        services.AddScoped<IChangeUserPasswordHandler, ChangeUserPasswordHandler>();
        services.AddScoped<ICreateAdminUserHandler, CreateAdminUserHandler>();
        services.AddScoped<IGenerateAccessTokenRequestHandler, GenerateAccessTokenRequestHandler>();
        services.AddScoped<ILoginUserRequestHandler, LoginUserRequestHandler>();
        services.AddScoped<IRegisterUserHandler, RegisterUserHandler>();
    }

    private static void AddCategories(this IServiceCollection services)
    {
        services.AddSingleton<ISubcategoryChangeCategoryEventHandler, SubcategoryChangeCategoryEventHandler>();
        services.AddSingleton<IAttachSubcategoryEventHandler, AttachSubcategoryEventHandler>();
        services.AddSingleton<IDetachSubcategoryEventHandler, DetachSubcategoryEventHandler>();
        services.AddSingleton<ICreateCategoryHandler, CreateCategoryHandler>();
        services.AddSingleton<IGetCategoryByIdHandler, GetCategoryByIdHandler>();
        services.AddSingleton<IImportCategorySeedHandler, ImportCategorySeedHandler>();
        services.AddSingleton<IListCategoriesHandler, ListCategoriesHandler>();
        services.AddSingleton<IUpdateCategoryHandler, UpdateCategoryHandler>();
    }

    private static void AddSubcategories(this IServiceCollection services)
    {
        services.AddSingleton<ICreateSubcategoryHandler, CreateSubcategoryHandler>();
        services.AddSingleton<IGetSubcategoryByIdHandler, GetSubcategoryByIdHandler>();
        services.AddSingleton<IListSubcategoriesHandler, ListSubcategoriesHandler>();
        services.AddSingleton<IUpdateSubcategoryHandler, UpdateSubcategoryHandler>();
    }
    private static void AddTransactions(this IServiceCollection services)
    {
        services.AddSingleton<ICreateTransactionHandler, CreateTransactionHandler>();
        services.AddSingleton<IGetTransacationByIdHandler, GetTransacationByIdHandler>();
        services.AddSingleton<IListTransactionHandler, ListTransactionHandler>();
        services.AddSingleton<IUpdateTransactionHandler, UpdateTransactionHandler>();
    }

    private static void AddMonthlyBudget(this IServiceCollection services)
    {
        services.AddSingleton<ICreateMonthlyBudgetHandler, CreateMonthlyBudgetHandler>();
    }
}
