namespace InvestmentProperties.APIs.Dtos;

public class InvestmentPropertyCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? IncomeStatements { get; set; }

    public List<Income>? Incomes { get; set; }

    public string? OperatingCosts { get; set; }

    public string? PropertyCosts { get; set; }

    public InvestmentProperty? State { get; set; }

    public List<InvestmentProperty>? States { get; set; }

    public DateTime UpdatedAt { get; set; }
}
