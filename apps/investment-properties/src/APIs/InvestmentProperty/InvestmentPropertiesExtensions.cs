using InvestmentProperties.APIs.Dtos;
using InvestmentProperties.Infrastructure.Models;

namespace InvestmentProperties.APIs.Extensions;

public static class InvestmentPropertiesExtensions
{
    public static InvestmentProperty ToDto(this InvestmentPropertyDbModel model)
    {
        return new InvestmentProperty
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            IncomeStatements = model.IncomeStatements,
            Incomes = model.Incomes?.Select(x => x.Id).ToList(),
            OperatingCosts = model.OperatingCosts,
            PropertyCosts = model.PropertyCosts,
            State = model.StateId,
            States = model.States?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static InvestmentPropertyDbModel ToModel(
        this InvestmentPropertyUpdateInput updateDto,
        InvestmentPropertyWhereUniqueInput uniqueId
    )
    {
        var investmentProperty = new InvestmentPropertyDbModel
        {
            Id = uniqueId.Id,
            IncomeStatements = updateDto.IncomeStatements,
            OperatingCosts = updateDto.OperatingCosts,
            PropertyCosts = updateDto.PropertyCosts
        };

        if (updateDto.CreatedAt != null)
        {
            investmentProperty.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.State != null)
        {
            investmentProperty.StateId = updateDto.State;
        }
        if (updateDto.UpdatedAt != null)
        {
            investmentProperty.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return investmentProperty;
    }
}
