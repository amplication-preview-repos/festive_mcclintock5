namespace InvestmentProperties.APIs.Dtos;

public class Income
{
    public DateTime CreatedAt { get; set; }

    public string Id { get; set; }

    public string? InvestmentProperty { get; set; }

    public string? Name { get; set; }

    public string? Parent { get; set; }

    public double? Result { get; set; }

    public string? Unit { get; set; }

    public DateTime UpdatedAt { get; set; }

    public double? Value { get; set; }
}
