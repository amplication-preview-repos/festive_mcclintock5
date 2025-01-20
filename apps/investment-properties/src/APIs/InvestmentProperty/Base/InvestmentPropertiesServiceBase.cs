using InvestmentProperties.APIs;
using InvestmentProperties.APIs.Common;
using InvestmentProperties.APIs.Dtos;
using InvestmentProperties.APIs.Errors;
using InvestmentProperties.APIs.Extensions;
using InvestmentProperties.Infrastructure;
using InvestmentProperties.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentProperties.APIs;

public abstract class InvestmentPropertiesServiceBase : IInvestmentPropertiesService
{
    protected readonly InvestmentPropertiesDbContext _context;

    public InvestmentPropertiesServiceBase(InvestmentPropertiesDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one InvestmentProperty
    /// </summary>
    public async Task<InvestmentProperty> CreateInvestmentProperty(
        InvestmentPropertyCreateInput createDto
    )
    {
        var investmentProperty = new InvestmentPropertyDbModel
        {
            CreatedAt = createDto.CreatedAt,
            IncomeStatements = createDto.IncomeStatements,
            OperatingCosts = createDto.OperatingCosts,
            PropertyCosts = createDto.PropertyCosts,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            investmentProperty.Id = createDto.Id;
        }
        if (createDto.Incomes != null)
        {
            investmentProperty.Incomes = await _context
                .Incomes.Where(income => createDto.Incomes.Select(t => t.Id).Contains(income.Id))
                .ToListAsync();
        }

        if (createDto.State != null)
        {
            investmentProperty.State = await _context
                .InvestmentProperties.Where(investmentProperty =>
                    createDto.State.Id == investmentProperty.Id
                )
                .FirstOrDefaultAsync();
        }

        if (createDto.States != null)
        {
            investmentProperty.States = await _context
                .InvestmentProperties.Where(investmentProperty =>
                    createDto.States.Select(t => t.Id).Contains(investmentProperty.Id)
                )
                .ToListAsync();
        }

        _context.InvestmentProperties.Add(investmentProperty);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<InvestmentPropertyDbModel>(investmentProperty.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one InvestmentProperty
    /// </summary>
    public async Task DeleteInvestmentProperty(InvestmentPropertyWhereUniqueInput uniqueId)
    {
        var investmentProperty = await _context.InvestmentProperties.FindAsync(uniqueId.Id);
        if (investmentProperty == null)
        {
            throw new NotFoundException();
        }

        _context.InvestmentProperties.Remove(investmentProperty);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many States
    /// </summary>
    public async Task<List<InvestmentProperty>> InvestmentProperties(
        InvestmentPropertyFindManyArgs findManyArgs
    )
    {
        var investmentProperties = await _context
            .InvestmentProperties.Include(x => x.State)
            .Include(x => x.Incomes)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return investmentProperties.ConvertAll(investmentProperty => investmentProperty.ToDto());
    }

    /// <summary>
    /// Meta data about InvestmentProperty records
    /// </summary>
    public async Task<MetadataDto> InvestmentPropertiesMeta(
        InvestmentPropertyFindManyArgs findManyArgs
    )
    {
        var count = await _context.InvestmentProperties.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one InvestmentProperty
    /// </summary>
    public async Task<InvestmentProperty> InvestmentProperty(
        InvestmentPropertyWhereUniqueInput uniqueId
    )
    {
        var investmentProperties = await this.InvestmentProperties(
            new InvestmentPropertyFindManyArgs
            {
                Where = new InvestmentPropertyWhereInput { Id = uniqueId.Id }
            }
        );
        var investmentProperty = investmentProperties.FirstOrDefault();
        if (investmentProperty == null)
        {
            throw new NotFoundException();
        }

        return investmentProperty;
    }

    /// <summary>
    /// Update one InvestmentProperty
    /// </summary>
    public async Task UpdateInvestmentProperty(
        InvestmentPropertyWhereUniqueInput uniqueId,
        InvestmentPropertyUpdateInput updateDto
    )
    {
        var investmentProperty = updateDto.ToModel(uniqueId);

        if (updateDto.Incomes != null)
        {
            investmentProperty.Incomes = await _context
                .Incomes.Where(income => updateDto.Incomes.Select(t => t).Contains(income.Id))
                .ToListAsync();
        }

        if (updateDto.State != null)
        {
            investmentProperty.State = await _context
                .InvestmentProperties.Where(investmentProperty =>
                    updateDto.State == investmentProperty.Id
                )
                .FirstOrDefaultAsync();
        }

        if (updateDto.States != null)
        {
            investmentProperty.States = await _context
                .InvestmentProperties.Where(investmentProperty =>
                    updateDto.States.Select(t => t).Contains(investmentProperty.Id)
                )
                .ToListAsync();
        }

        _context.Entry(investmentProperty).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.InvestmentProperties.Any(e => e.Id == investmentProperty.Id))
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
    /// Connect multiple Incomes records to InvestmentProperty
    /// </summary>
    public async Task ConnectIncomes(
        InvestmentPropertyWhereUniqueInput uniqueId,
        IncomeWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .InvestmentProperties.Include(x => x.Incomes)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Incomes.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Incomes);

        foreach (var child in childrenToConnect)
        {
            parent.Incomes.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Incomes records from InvestmentProperty
    /// </summary>
    public async Task DisconnectIncomes(
        InvestmentPropertyWhereUniqueInput uniqueId,
        IncomeWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .InvestmentProperties.Include(x => x.Incomes)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Incomes.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Incomes?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Incomes records for InvestmentProperty
    /// </summary>
    public async Task<List<Income>> FindIncomes(
        InvestmentPropertyWhereUniqueInput uniqueId,
        IncomeFindManyArgs investmentPropertyFindManyArgs
    )
    {
        var incomes = await _context
            .Incomes.Where(m => m.InvestmentPropertyId == uniqueId.Id)
            .ApplyWhere(investmentPropertyFindManyArgs.Where)
            .ApplySkip(investmentPropertyFindManyArgs.Skip)
            .ApplyTake(investmentPropertyFindManyArgs.Take)
            .ApplyOrderBy(investmentPropertyFindManyArgs.SortBy)
            .ToListAsync();

        return incomes.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Incomes records for InvestmentProperty
    /// </summary>
    public async Task UpdateIncomes(
        InvestmentPropertyWhereUniqueInput uniqueId,
        IncomeWhereUniqueInput[] childrenIds
    )
    {
        var investmentProperty = await _context
            .InvestmentProperties.Include(t => t.Incomes)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (investmentProperty == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Incomes.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        investmentProperty.Incomes = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a State record for State
    /// </summary>
    public async Task<InvestmentProperty> GetState(InvestmentPropertyWhereUniqueInput uniqueId)
    {
        var investmentProperty = await _context
            .InvestmentProperties.Where(investmentProperty => investmentProperty.Id == uniqueId.Id)
            .Include(investmentProperty => investmentProperty.State)
            .FirstOrDefaultAsync();
        if (investmentProperty == null)
        {
            throw new NotFoundException();
        }
        return investmentProperty.State.ToDto();
    }

    /// <summary>
    /// Connect multiple States records to State
    /// </summary>
    public async Task ConnectStates(
        InvestmentPropertyWhereUniqueInput uniqueId,
        InvestmentPropertyWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .InvestmentProperties.Include(x => x.States)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .InvestmentProperties.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.States);

        foreach (var child in childrenToConnect)
        {
            parent.States.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple States records from State
    /// </summary>
    public async Task DisconnectStates(
        InvestmentPropertyWhereUniqueInput uniqueId,
        InvestmentPropertyWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .InvestmentProperties.Include(x => x.States)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .InvestmentProperties.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.States?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple States records for State
    /// </summary>
    public async Task<List<InvestmentProperty>> FindStates(
        InvestmentPropertyWhereUniqueInput uniqueId,
        InvestmentPropertyFindManyArgs investmentPropertyFindManyArgs
    )
    {
        var investmentProperties = await _context
            .InvestmentProperties.Where(m => m.StateId == uniqueId.Id)
            .ApplyWhere(investmentPropertyFindManyArgs.Where)
            .ApplySkip(investmentPropertyFindManyArgs.Skip)
            .ApplyTake(investmentPropertyFindManyArgs.Take)
            .ApplyOrderBy(investmentPropertyFindManyArgs.SortBy)
            .ToListAsync();

        return investmentProperties.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple States records for State
    /// </summary>
    public async Task UpdateStates(
        InvestmentPropertyWhereUniqueInput uniqueId,
        InvestmentPropertyWhereUniqueInput[] childrenIds
    )
    {
        var investmentProperty = await _context
            .InvestmentProperties.Include(t => t.States)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (investmentProperty == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .InvestmentProperties.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        investmentProperty.States = children;
        await _context.SaveChangesAsync();
    }
}
