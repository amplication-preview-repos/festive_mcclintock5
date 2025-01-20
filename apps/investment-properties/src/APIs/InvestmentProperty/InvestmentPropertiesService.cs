using InvestmentProperties.Infrastructure;

namespace InvestmentProperties.APIs;

public class InvestmentPropertiesService : InvestmentPropertiesServiceBase
{
    public InvestmentPropertiesService(InvestmentPropertiesDbContext context)
        : base(context) { }
}
