namespace InvestmentProperties.APIs.Dtos;

public class InvestmentPropertyWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? IncomeStatements { get; set; }

    public List<string>? Incomes { get; set; }

    public string? OperatingCosts { get; set; }

    public string? PropertyCosts { get; set; }

    public string? State { get; set; }

    public List<string>? States { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
