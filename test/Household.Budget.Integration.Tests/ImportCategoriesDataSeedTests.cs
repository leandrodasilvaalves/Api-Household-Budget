using System.Net;

using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Images;
using DotNet.Testcontainers.Networks;

using FluentAssertions;

using Household.Budget.Domain.Data;
using Household.Budget.Infra.Extensions;
using Household.Budget.UseCases.Categories.ImportCategorySeed;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Household.Budget.Integration.Tests;

public class ImportCategoriesDataSeedTests
{
    private INetwork? Network { get; set; }
    private IContainer? MongoDb { get; set; }
    private IContainer? RabbitMQ { get; set; }
    private IContainer? ApiContainer { get; set; }
    private IFutureDockerImage? ApiImage { get; set; }
    private List<ImportCategorySeedRequest> CategoriesConfig { get; set; } = [];
    private string? RootUserId { get; set; } = "";
    private ICategoryData? CategoryData { get; set; }
    private ISubcategoryData? SubcategoryData { get; set; }

    [Fact]
    public async Task ShouldImportCategoriesDataSeedAsync()
    {
        await SetupEnvironmentAsync();
        await AssertCategoriesAsync();
    }

    private async Task AssertCategoriesAsync()
    {
        var categoriesDb = await CategoryData?.GetAllAsync(CategoriesConfig.Count, 1, RootUserId, CancellationToken.None);

        categoriesDb.TotalResult.Should().Be(CategoriesConfig.Count);
        CategoriesConfig.ForEach(categoryConfig =>
        {
            var categoryDb = categoriesDb.Items.FirstOrDefault(x => x.Name == categoryConfig.Name);
            categoryDb?.Subcategories.Count.Should().Be(categoryConfig.SubCategories.Count);
        });
    }

    private async Task SetupEnvironmentAsync()
    {
        await CreateNetwork();
        await CreateMongoContainerAsync();
        await CreateRabbitContainerAsync();
        await CreateApiImageAsync();
        await CreateApiContainerAsync();
        ReadAppSettingsJson();
    }

    private async Task CreateNetwork()
    {
        Network = new NetworkBuilder()
            .WithName("household.budget.test")
            .WithCleanUp(true)
            .Build();

        await Network
            .CreateAsync()
            .ConfigureAwait(false);
    }

    private async Task CreateRabbitContainerAsync()
    {
        RabbitMQ = new ContainerBuilder()
            .WithName("rabbit")
            .WithImage("rabbitmq:3-management")
            .WithPortBinding(5672, 5672)
            .WithPortBinding(15672, 15672)
            .WithNetwork(Network)
            .WithWaitStrategy(
                Wait.ForUnixContainer()
                .UntilPortIsAvailable(15672))
            .WithCleanUp(true)
            .Build();

        await RabbitMQ
            .StartAsync()
            .ConfigureAwait(false);
    }

    private async Task CreateMongoContainerAsync()
    {
        var environment = new Dictionary<string, string>
        {
            {"MONGO_INITDB_ROOT_USERNAME", "root"},
            {"MONGO_INITDB_ROOT_PASSWORD", "rootpassword"},
        };

        MongoDb = new ContainerBuilder()
            .WithName("mongo")
            .WithImage("mongo")
            .WithPortBinding(27017, 27017)
            .WithEnvironment(environment)
            .WithNetwork(Network)
            .WithWaitStrategy(
                 Wait.ForUnixContainer()
                .UntilPortIsAvailable(27017))
            .WithCleanUp(true)
            .Build();

        await MongoDb
            .StartAsync()
            .ConfigureAwait(false);
    }

    private async Task CreateApiImageAsync()
    {
        ApiImage = new ImageFromDockerfileBuilder()
            .WithDockerfileDirectory(CommonDirectoryPath.GetSolutionDirectory(), "src/Household.Budget")
            .WithDockerfile("Dockerfile")
            .WithName($"household.buget:test.{DateTime.Now:yyMMddHHmm}")
            .WithCleanUp(true)
            .Build();

        await ApiImage
            .CreateAsync()
            .ConfigureAwait(false);
    }

    private async Task CreateApiContainerAsync()
    {
        ApiContainer = new ContainerBuilder()
            .WithName("household.buget.tests")
            .WithImage(ApiImage?.FullName)
            .WithPortBinding(5215, 80)
            .WithNetwork(Network)
            .WithEnvironment("RavenSettings__DatabaseName", "Household.Budget.Test")
            .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Docker")
            .WithWaitStrategy(
                Wait.ForUnixContainer()
                    .UntilHttpRequestIsSucceeded(request =>
                        request
                            .ForPort(5215)
                            .ForPath("/hc/ready")
                            .ForStatusCode(HttpStatusCode.OK)))
            .WithCleanUp(true)
            .Build();

        await ApiContainer
            .StartAsync()
            .ConfigureAwait(false);
    }

    private void ReadAppSettingsJson()
    {
        var config = new ConfigurationBuilder()
         .SetBasePath(AppContext.BaseDirectory)
         .AddJsonFile("appsettings.json")
         .AddJsonFile("appsettings.Test.json")
         .Build();

        CategoriesConfig = config.GetSection("Seed:Categories:Data").Get<List<ImportCategorySeedRequest>>() ?? [];
        RootUserId = config.GetSection("Identity:RootUser:Request:UserId").Get<string>();

        var services = new ServiceCollection()
            .AddMongo(config)
            .BuildServiceProvider();

        CategoryData = services.GetService<ICategoryData>();
        SubcategoryData = services.GetService<ISubcategoryData>();
    }
}