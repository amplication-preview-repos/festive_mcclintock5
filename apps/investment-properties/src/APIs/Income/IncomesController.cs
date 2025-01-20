using Microsoft.AspNetCore.Mvc;

namespace InvestmentProperties.APIs;

[ApiController()]
public class IncomesController : IncomesControllerBase
{
    public IncomesController(IIncomesService service)
        : base(service) { }
}
