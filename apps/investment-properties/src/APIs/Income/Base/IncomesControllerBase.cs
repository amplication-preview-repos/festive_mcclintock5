using InvestmentProperties.APIs;
using InvestmentProperties.APIs.Common;
using InvestmentProperties.APIs.Dtos;
using InvestmentProperties.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentProperties.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class IncomesControllerBase : ControllerBase
{
    protected readonly IIncomesService _service;

    public IncomesControllerBase(IIncomesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Income
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Income>> CreateIncome(IncomeCreateInput input)
    {
        var income = await _service.CreateIncome(input);

        return CreatedAtAction(nameof(Income), new { id = income.Id }, income);
    }

    /// <summary>
    /// Delete one Income
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteIncome([FromRoute()] IncomeWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteIncome(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Incomes
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Income>>> Incomes([FromQuery()] IncomeFindManyArgs filter)
    {
        return Ok(await _service.Incomes(filter));
    }

    /// <summary>
    /// Meta data about Income records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> IncomesMeta(
        [FromQuery()] IncomeFindManyArgs filter
    )
    {
        return Ok(await _service.IncomesMeta(filter));
    }

    /// <summary>
    /// Get one Income
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Income>> Income([FromRoute()] IncomeWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Income(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Income
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateIncome(
        [FromRoute()] IncomeWhereUniqueInput uniqueId,
        [FromQuery()] IncomeUpdateInput incomeUpdateDto
    )
    {
        try
        {
            await _service.UpdateIncome(uniqueId, incomeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a InvestmentProperty record for Income
    /// </summary>
    [HttpGet("{Id}/investmentProperty")]
    public async Task<ActionResult<List<InvestmentProperty>>> GetInvestmentProperty(
        [FromRoute()] IncomeWhereUniqueInput uniqueId
    )
    {
        var investmentProperty = await _service.GetInvestmentProperty(uniqueId);
        return Ok(investmentProperty);
    }
}
