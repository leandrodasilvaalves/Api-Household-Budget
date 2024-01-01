using System.Text.Json.Serialization;

using Household.Budget.Api.Config;
using Household.Budget.Api.Controllers.Filters;
using Household.Budget.Api.HealthCheck;
using Household.Budget.Api.Middlewares;
using Household.Budget.Infra.Extensions;

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck("RavenDb", new RavenDbHealthCheck(builder.Configuration));

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddControllers(options => options.Filters.Add<AddUserClaimsFilter>())
    .AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
      options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddInfra(builder.Configuration);
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ConfigureJwt(builder.Configuration);

var app = builder.Build();

app.UserExceptionHandling();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/hc");

app.Run();