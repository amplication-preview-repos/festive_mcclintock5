using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentProperties.Infrastructure.Models;

[Table("InvestmentProperties")]
public class InvestmentPropertyDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? IncomeStatements { get; set; }

    public List<IncomeDbModel>? Incomes { get; set; } = new List<IncomeDbModel>();

    [StringLength(1000)]
    public string? OperatingCosts { get; set; }

    [StringLength(1000)]
    public string? PropertyCosts { get; set; }

    public string? StateId { get; set; }

    [ForeignKey(nameof(StateId))]
    public InvestmentPropertyDbModel? State { get; set; } = null;

    public List<InvestmentPropertyDbModel>? States { get; set; } =
        new List<InvestmentPropertyDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
