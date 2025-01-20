using InvestmentProperties.APIs.Common;
using InvestmentProperties.APIs.Dtos;

namespace InvestmentProperties.APIs;

public interface IInvestmentPropertiesService
{
    /// <summary>
    /// Create one InvestmentProperty
    /// </summary>
    public Task<InvestmentProperty> CreateInvestmentProperty(
        InvestmentPropertyCreateInput investmentproperty
    );

    /// <summary>
    /// Delete one InvestmentProperty
    /// </summary>
    public Task DeleteInvestmentProperty(InvestmentPropertyWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many States
    /// </summary>
    public Task<List<InvestmentProperty>> InvestmentProperties(
        InvestmentPropertyFindManyArgs findManyArgs
    );

    /// <summary>
    /// Meta data about InvestmentProperty records
    /// </summary>
    public Task<MetadataDto> InvestmentPropertiesMeta(InvestmentPropertyFindManyArgs findManyArgs);

    /// <summary>
    /// Get one InvestmentProperty
    /// </summary>
    public Task<InvestmentProperty> InvestmentProperty(InvestmentPropertyWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one InvestmentProperty
    /// </summary>
    public Task UpdateInvestmentProperty(
        InvestmentPropertyWhereUniqueInput uniqueId,
        InvestmentPropertyUpdateInput updateDto
    );

    /// <summary>
    /// Connect multiple Incomes records to InvestmentProperty
    /// </summary>
    public Task ConnectIncomes(
        InvestmentPropertyWhereUniqueInput uniqueId,
        IncomeWhereUniqueInput[] incomesId
    );

    /// <summary>
    /// Disconnect multiple Incomes records from InvestmentProperty
    /// </summary>
    public Task DisconnectIncomes(
        InvestmentPropertyWhereUniqueInput uniqueId,
        IncomeWhereUniqueInput[] incomesId
    );

    /// <summary>
    /// Find multiple Incomes records for InvestmentProperty
    /// </summary>
    public Task<List<Income>> FindIncomes(
        InvestmentPropertyWhereUniqueInput uniqueId,
        IncomeFindManyArgs IncomeFindManyArgs
    );

    /// <summary>
    /// Update multiple Incomes records for InvestmentProperty
    /// </summary>
    public Task UpdateIncomes(
        InvestmentPropertyWhereUniqueInput uniqueId,
        IncomeWhereUniqueInput[] incomesId
    );

    /// <summary>
    /// Get a State record for State
    /// </summary>
    public Task<InvestmentProperty> GetState(InvestmentPropertyWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple States records to State
    /// </summary>
    public Task ConnectStates(
        InvestmentPropertyWhereUniqueInput uniqueId,
        InvestmentPropertyWhereUniqueInput[] investmentPropertiesId
    );

    /// <summary>
    /// Disconnect multiple States records from State
    /// </summary>
    public Task DisconnectStates(
        InvestmentPropertyWhereUniqueInput uniqueId,
        InvestmentPropertyWhereUniqueInput[] investmentPropertiesId
    );

    /// <summary>
    /// Find multiple States records for State
    /// </summary>
    public Task<List<InvestmentProperty>> FindStates(
        InvestmentPropertyWhereUniqueInput uniqueId,
        InvestmentPropertyFindManyArgs InvestmentPropertyFindManyArgs
    );

    /// <summary>
    /// Update multiple States records for State
    /// </summary>
    public Task UpdateStates(
        InvestmentPropertyWhereUniqueInput uniqueId,
        InvestmentPropertyWhereUniqueInput[] investmentPropertiesId
    );
}
