using InvestmentProperties.APIs;

namespace InvestmentProperties;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IIncomesService, IncomesService>();
        services.AddScoped<IInvestmentPropertiesService, InvestmentPropertiesService>();
    }
}
