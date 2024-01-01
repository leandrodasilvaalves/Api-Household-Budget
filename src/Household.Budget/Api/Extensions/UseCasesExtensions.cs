using Household.Budget.UseCases.Categories.CreateCategories;
using Household.Budget.UseCases.Categories.EventHandlers;
using Household.Budget.UseCases.Categories.GetCategoryById;
using Household.Budget.UseCases.Categories.GetSubcategoryById;
using Household.Budget.UseCases.Categories.ImportCategorySeed;
using Household.Budget.UseCases.Categories.ListCategories;
using Household.Budget.UseCases.Categories.UpdateCategory;
using Household.Budget.UseCases.Categories.UpdateSubcategory;
using Household.Budget.UseCases.Identity.CreateAdminUser;
using Household.Budget.UseCases.Identity.LoginUser;
using Household.Budget.UseCases.Identity.RegisterUser;
using Household.Budget.UseCases.Subcategories.CreateSubcategory;
using Household.Budget.UseCases.Subcategories.ListSubcategories;

namespace Household.Budget.Api.Extensions;

public static class UseCasesExtensions
{
    public static void AddUseCases(this IServiceCollection services)
    {
        services.AddIdentity();
        services.AddCategories();
        services.AddSubcategories();
    }

    private static void AddIdentity(this IServiceCollection services)
    {
        services.AddSingleton<IChangeUserPasswordHandler, ChangeUserPasswordHandler>();
        services.AddSingleton<ICreateAdminUserHandler, CreateAdminUserHandler>();
        services.AddSingleton<IGenerateAccessTokenRequestHandler, GenerateAccessTokenRequestHandler>();
        services.AddSingleton<ILoginUserRequestHandler, LoginUserRequestHandler>();
        services.AddSingleton<IRegisterUserHandler, RegisterUserHandler>();
    }

    private static void AddCategories(this IServiceCollection services)
    {
        services.AddSingleton<ISubcategoryChangeCategoryEventHandler, SubcategoryChangeCategoryEventHandler>();
        services.AddSingleton<IAttachSubCategoryEventHandler, AttachSubCategoryEventHandler>();
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
}