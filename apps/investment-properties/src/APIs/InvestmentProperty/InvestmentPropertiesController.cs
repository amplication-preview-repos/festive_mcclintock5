using Microsoft.AspNetCore.Mvc;

namespace InvestmentProperties.APIs;

[ApiController()]
public class InvestmentPropertiesController : InvestmentPropertiesControllerBase
{
    public InvestmentPropertiesController(IInvestmentPropertiesService service)
        : base(service) { }
}
