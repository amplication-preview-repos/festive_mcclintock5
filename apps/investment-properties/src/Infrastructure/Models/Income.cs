using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentProperties.Infrastructure.Models;

[Table("Incomes")]
public class IncomeDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? InvestmentPropertyId { get; set; }

    [ForeignKey(nameof(InvestmentPropertyId))]
    public InvestmentPropertyDbModel? InvestmentProperty { get; set; } = null;

    [StringLength(1000)]
    public string? Name { get; set; }

    [StringLength(1000)]
    public string? Parent { get; set; }

    [Range(0, 99999999999)]
    public double? Result { get; set; }

    [StringLength(1000)]
    public string? Unit { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [Range(0, 99999999999)]
    public double? Value { get; set; }
}
