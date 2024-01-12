using KissLog.AspNetCore;
using KissLog.CloudListeners.Auth;
using KissLog.CloudListeners.RequestLogsListener;
using KissLog.Formatters;

namespace Household.Budget.Api.Extensions;

public static class KissLogExtensions
{
    public static void AddKissLog(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<KissLogOptions>(config.GetSection(KissLogOptions.SectionName));
        var options = config.GetSection(KissLogOptions.SectionName).Get<KissLogOptions>() ?? new();

        if (options.Enabled)
        {
            services.AddLogging(provider => provider.AddKissLog(options =>
            options.Formatter = (FormatterArgs args) =>
            {
                if (args.Exception == null)
                    return args.DefaultValue;

                string exceptionStr = new ExceptionFormatter().Format(args.Exception, args.Logger);
                return string.Join(Environment.NewLine, new[] { args.DefaultValue, exceptionStr });
            }));
        }
    }

    public static void UseKissLog(this IApplicationBuilder app, IConfiguration config)
    {
        var kissOptions = config.GetSection(KissLogOptions.SectionName).Get<KissLogOptions>() ?? new();
        if (kissOptions.Enabled)
        {
            app.UseKissLogMiddleware(options => 
                options.Listeners.Add(new RequestLogsApiListener(
                    new Application(kissOptions.OrganizationId, kissOptions.ApplicationId))
                {
                    ApiUrl = kissOptions.ApiUrl
                }));
        }
    }
}

public class KissLogOptions
{
    public const string SectionName = "KissLog";

    public bool Enabled { get; set; }
    public string OrganizationId { get; set; } = "";
    public string ApplicationId { get; set; } = "";
    public string ApiUrl { get; set; } = "";
}