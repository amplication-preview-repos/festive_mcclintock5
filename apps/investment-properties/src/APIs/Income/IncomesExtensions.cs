using InvestmentProperties.APIs.Dtos;
using InvestmentProperties.Infrastructure.Models;

namespace InvestmentProperties.APIs.Extensions;

public static class IncomesExtensions
{
    public static Income ToDto(this IncomeDbModel model)
    {
        return new Income
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            InvestmentProperty = model.InvestmentPropertyId,
            Name = model.Name,
            Parent = model.Parent,
            Result = model.Result,
            Unit = model.Unit,
            UpdatedAt = model.UpdatedAt,
            Value = model.Value,
        };
    }

    public static IncomeDbModel ToModel(
        this IncomeUpdateInput updateDto,
        IncomeWhereUniqueInput uniqueId
    )
    {
        var income = new IncomeDbModel
        {
            Id = uniqueId.Id,
            Name = updateDto.Name,
            Parent = updateDto.Parent,
            Result = updateDto.Result,
            Unit = updateDto.Unit,
            Value = updateDto.Value
        };

        if (updateDto.CreatedAt != null)
        {
            income.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.InvestmentProperty != null)
        {
            income.InvestmentPropertyId = updateDto.InvestmentProperty;
        }
        if (updateDto.UpdatedAt != null)
        {
            income.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return income;
    }
}
