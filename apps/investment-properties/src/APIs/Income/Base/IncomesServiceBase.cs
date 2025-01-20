using InvestmentProperties.APIs;
using InvestmentProperties.APIs.Common;
using InvestmentProperties.APIs.Dtos;
using InvestmentProperties.APIs.Errors;
using InvestmentProperties.APIs.Extensions;
using InvestmentProperties.Infrastructure;
using InvestmentProperties.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentProperties.APIs;

public abstract class IncomesServiceBase : IIncomesService
{
    protected readonly InvestmentPropertiesDbContext _context;

    public IncomesServiceBase(InvestmentPropertiesDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Income
    /// </summary>
    public async Task<Income> CreateIncome(IncomeCreateInput createDto)
    {
        var income = new IncomeDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            Parent = createDto.Parent,
            Result = createDto.Result,
            Unit = createDto.Unit,
            UpdatedAt = createDto.UpdatedAt,
            Value = createDto.Value
        };

        if (createDto.Id != null)
        {
            income.Id = createDto.Id;
        }
        if (createDto.InvestmentProperty != null)
        {
            income.InvestmentProperty = await _context
                .InvestmentProperties.Where(investmentProperty =>
                    createDto.InvestmentProperty.Id == investmentProperty.Id
                )
                .FirstOrDefaultAsync();
        }

        _context.Incomes.Add(income);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<IncomeDbModel>(income.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Income
    /// </summary>
    public async Task DeleteIncome(IncomeWhereUniqueInput uniqueId)
    {
        var income = await _context.Incomes.FindAsync(uniqueId.Id);
        if (income == null)
        {
            throw new NotFoundException();
        }

        _context.Incomes.Remove(income);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Incomes
    /// </summary>
    public async Task<List<Income>> Incomes(IncomeFindManyArgs findManyArgs)
    {
        var incomes = await _context
            .Incomes.Include(x => x.InvestmentProperty)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return incomes.ConvertAll(income => income.ToDto());
    }

    /// <summary>
    /// Meta data about Income records
    /// </summary>
    public async Task<MetadataDto> IncomesMeta(IncomeFindManyArgs findManyArgs)
    {
        var count = await _context.Incomes.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Income
    /// </summary>
    public async Task<Income> Income(IncomeWhereUniqueInput uniqueId)
    {
        var incomes = await this.Incomes(
            new IncomeFindManyArgs { Where = new IncomeWhereInput { Id = uniqueId.Id } }
        );
        var income = incomes.FirstOrDefault();
        if (income == null)
        {
            throw new NotFoundException();
        }

        return income;
    }

    /// <summary>
    /// Update one Income
    /// </summary>
    public async Task UpdateIncome(IncomeWhereUniqueInput uniqueId, IncomeUpdateInput updateDto)
    {
        var income = updateDto.ToModel(uniqueId);

        if (updateDto.InvestmentProperty != null)
        {
            income.InvestmentProperty = await _context
                .InvestmentProperties.Where(investmentProperty =>
                    updateDto.InvestmentProperty == investmentProperty.Id
                )
                .FirstOrDefaultAsync();
        }

        _context.Entry(income).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Incomes.Any(e => e.Id == income.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a InvestmentProperty record for Income
    /// </summary>
    public async Task<InvestmentProperty> GetInvestmentProperty(IncomeWhereUniqueInput uniqueId)
    {
        var income = await _context
            .Incomes.Where(income => income.Id == uniqueId.Id)
            .Include(income => income.InvestmentProperty)
            .FirstOrDefaultAsync();
        if (income == null)
        {
            throw new NotFoundException();
        }
        return income.InvestmentProperty.ToDto();
    }
}
