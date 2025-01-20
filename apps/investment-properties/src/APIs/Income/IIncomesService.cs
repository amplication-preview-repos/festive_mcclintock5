using InvestmentProperties.APIs.Common;
using InvestmentProperties.APIs.Dtos;

namespace InvestmentProperties.APIs;

public interface IIncomesService
{
    /// <summary>
    /// Create one Income
    /// </summary>
    public Task<Income> CreateIncome(IncomeCreateInput income);

    /// <summary>
    /// Delete one Income
    /// </summary>
    public Task DeleteIncome(IncomeWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Incomes
    /// </summary>
    public Task<List<Income>> Incomes(IncomeFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Income records
    /// </summary>
    public Task<MetadataDto> IncomesMeta(IncomeFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Income
    /// </summary>
    public Task<Income> Income(IncomeWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Income
    /// </summary>
    public Task UpdateIncome(IncomeWhereUniqueInput uniqueId, IncomeUpdateInput updateDto);

    /// <summary>
    /// Get a InvestmentProperty record for Income
    /// </summary>
    public Task<InvestmentProperty> GetInvestmentProperty(IncomeWhereUniqueInput uniqueId);
}
