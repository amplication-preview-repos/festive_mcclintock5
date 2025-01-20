using InvestmentProperties.APIs.Common;
using InvestmentProperties.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentProperties.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class IncomeFindManyArgs : FindManyInput<Income, IncomeWhereInput> { }
