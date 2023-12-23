using Microsoft.Extensions.Logging;
using NSubstitute;
using Household.Budget.Contracts.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Household.Budget.Tests;

public class DatabaseCreatorServiceTests
{
    private IDatabaseCreator _databaseCreator;
    private ILogger<DatabaseCreatorService> _logger;
    private IConfiguration _configuration;
    private IServiceScopeFactory _serviceScopeFactory;

    private DatabaseCreatorService _sut;

    public DatabaseCreatorServiceTests()
    {
        _databaseCreator = Substitute.For<IDatabaseCreator>();
        _logger = Substitute.For<ILogger<DatabaseCreatorService>>();
        _configuration = Substitute.For<IConfiguration>();
        _serviceScopeFactory = Substitute.For<IServiceScopeFactory>();
        _sut = new DatabaseCreatorService(_databaseCreator, _logger, _configuration, _serviceScopeFactory);
    }

    [Fact]
    public async Task ExecuteAsync_Should_Create_Database_And_Call_CreateRootUser()
    {
        // Arrange
        


        // Act
        await _sut.StartAsync(CancellationToken.None);

        // Assert
        _databaseCreator.Received(1).EnsureDatabaseIsCreated();
        _logger.Received(1).LogInformation("Database creator was executed successfully.");
    }

    // [Fact]
    // public async Task CreateRootUser_Should_Create_Root_User_When_Enabled()
    // {
    //     // Arrange
    //     _configuration.GetValue<bool>("Identity:RootUser:Enabled").Returns(true);

    //     var service = new DatabaseCreatorService(
    //         _databaseCreator,
    //         _logger,
    //         _configuration,
    //         _serviceScopeFactory
    //     );

    //     // Act
    //     await service.CreateRootUser(CancellationToken.None);

    //     // Assert
    //     // Add your assertions here using FluentAssertions
    // }
}