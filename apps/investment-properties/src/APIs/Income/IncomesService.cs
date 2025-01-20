using InvestmentProperties.Infrastructure;

namespace InvestmentProperties.APIs;

public class IncomesService : IncomesServiceBase
{
    public IncomesService(InvestmentPropertiesDbContext context)
        : base(context) { }
}
