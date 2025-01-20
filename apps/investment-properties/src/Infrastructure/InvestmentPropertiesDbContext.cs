using InvestmentProperties.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace InvestmentProperties.Infrastructure;

public class InvestmentPropertiesDbContext : DbContext
{
    public InvestmentPropertiesDbContext(DbContextOptions<InvestmentPropertiesDbContext> options)
        : base(options) { }

    public DbSet<InvestmentPropertyDbModel> InvestmentProperties { get; set; }

    public DbSet<IncomeDbModel> Incomes { get; set; }
}
