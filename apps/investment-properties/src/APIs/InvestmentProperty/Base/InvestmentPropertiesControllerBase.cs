using InvestmentProperties.APIs;
using InvestmentProperties.APIs.Common;
using InvestmentProperties.APIs.Dtos;
using InvestmentProperties.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentProperties.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class InvestmentPropertiesControllerBase : ControllerBase
{
    protected readonly IInvestmentPropertiesService _service;

    public InvestmentPropertiesControllerBase(IInvestmentPropertiesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one InvestmentProperty
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<InvestmentProperty>> CreateInvestmentProperty(
        InvestmentPropertyCreateInput input
    )
    {
        var investmentProperty = await _service.CreateInvestmentProperty(input);

        return CreatedAtAction(
            nameof(InvestmentProperty),
            new { id = investmentProperty.Id },
            investmentProperty
        );
    }

    /// <summary>
    /// Delete one InvestmentProperty
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteInvestmentProperty(
        [FromRoute()] InvestmentPropertyWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteInvestmentProperty(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many States
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<InvestmentProperty>>> InvestmentProperties(
        [FromQuery()] InvestmentPropertyFindManyArgs filter
    )
    {
        return Ok(await _service.InvestmentProperties(filter));
    }

    /// <summary>
    /// Meta data about InvestmentProperty records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> InvestmentPropertiesMeta(
        [FromQuery()] InvestmentPropertyFindManyArgs filter
    )
    {
        return Ok(await _service.InvestmentPropertiesMeta(filter));
    }

    /// <summary>
    /// Get one InvestmentProperty
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<InvestmentProperty>> InvestmentProperty(
        [FromRoute()] InvestmentPropertyWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.InvestmentProperty(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one InvestmentProperty
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateInvestmentProperty(
        [FromRoute()] InvestmentPropertyWhereUniqueInput uniqueId,
        [FromQuery()] InvestmentPropertyUpdateInput investmentPropertyUpdateDto
    )
    {
        try
        {
            await _service.UpdateInvestmentProperty(uniqueId, investmentPropertyUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Incomes records to InvestmentProperty
    /// </summary>
    [HttpPost("{Id}/incomes")]
    public async Task<ActionResult> ConnectIncomes(
        [FromRoute()] InvestmentPropertyWhereUniqueInput uniqueId,
        [FromQuery()] IncomeWhereUniqueInput[] incomesId
    )
    {
        try
        {
            await _service.ConnectIncomes(uniqueId, incomesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Incomes records from InvestmentProperty
    /// </summary>
    [HttpDelete("{Id}/incomes")]
    public async Task<ActionResult> DisconnectIncomes(
        [FromRoute()] InvestmentPropertyWhereUniqueInput uniqueId,
        [FromBody()] IncomeWhereUniqueInput[] incomesId
    )
    {
        try
        {
            await _service.DisconnectIncomes(uniqueId, incomesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Incomes records for InvestmentProperty
    /// </summary>
    [HttpGet("{Id}/incomes")]
    public async Task<ActionResult<List<Income>>> FindIncomes(
        [FromRoute()] InvestmentPropertyWhereUniqueInput uniqueId,
        [FromQuery()] IncomeFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindIncomes(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Incomes records for InvestmentProperty
    /// </summary>
    [HttpPatch("{Id}/incomes")]
    public async Task<ActionResult> UpdateIncomes(
        [FromRoute()] InvestmentPropertyWhereUniqueInput uniqueId,
        [FromBody()] IncomeWhereUniqueInput[] incomesId
    )
    {
        try
        {
            await _service.UpdateIncomes(uniqueId, incomesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a State record for State
    /// </summary>
    [HttpGet("{Id}/state")]
    public async Task<ActionResult<List<InvestmentProperty>>> GetState(
        [FromRoute()] InvestmentPropertyWhereUniqueInput uniqueId
    )
    {
        var investmentProperty = await _service.GetState(uniqueId);
        return Ok(investmentProperty);
    }

    /// <summary>
    /// Connect multiple States records to State
    /// </summary>
    [HttpPost("{Id}/states")]
    public async Task<ActionResult> ConnectStates(
        [FromRoute()] InvestmentPropertyWhereUniqueInput uniqueId,
        [FromQuery()] InvestmentPropertyWhereUniqueInput[] investmentPropertiesId
    )
    {
        try
        {
            await _service.ConnectStates(uniqueId, investmentPropertiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple States records from State
    /// </summary>
    [HttpDelete("{Id}/states")]
    public async Task<ActionResult> DisconnectStates(
        [FromRoute()] InvestmentPropertyWhereUniqueInput uniqueId,
        [FromBody()] InvestmentPropertyWhereUniqueInput[] investmentPropertiesId
    )
    {
        try
        {
            await _service.DisconnectStates(uniqueId, investmentPropertiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple States records for State
    /// </summary>
    [HttpGet("{Id}/states")]
    public async Task<ActionResult<List<InvestmentProperty>>> FindStates(
        [FromRoute()] InvestmentPropertyWhereUniqueInput uniqueId,
        [FromQuery()] InvestmentPropertyFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindStates(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple States records for State
    /// </summary>
    [HttpPatch("{Id}/states")]
    public async Task<ActionResult> UpdateStates(
        [FromRoute()] InvestmentPropertyWhereUniqueInput uniqueId,
        [FromBody()] InvestmentPropertyWhereUniqueInput[] investmentPropertiesId
    )
    {
        try
        {
            await _service.UpdateStates(uniqueId, investmentPropertiesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
