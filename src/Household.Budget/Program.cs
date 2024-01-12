using Household.Budget.Api.Extensions;
using Household.Budget.Api.Middlewares;
using Household.Budget.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.UseKeyVault();
builder.Services.AddKissLog(builder.Configuration);
builder.Services.AddHealthChecks(builder.Configuration);
builder.Services.ConfigureApi();
builder.Services.AddSwaggerGen();
builder.Services.AddInfra(builder.Configuration);
builder.Services.AddHostedServices();
builder.Services.AddUseCases();
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ConfigureJwt(builder.Configuration);

var app = builder.Build();

app.UseKissLog(app.Configuration);
app.UserExceptionHandling();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHealthCheck();

app.Run();