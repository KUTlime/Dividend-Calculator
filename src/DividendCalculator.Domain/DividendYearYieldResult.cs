namespace DividendCalculator.Domain;

public record DividendYearYieldResult(
    Stock Stock,
    int Year,
    decimal InvestmentAmount,
    decimal YearYieldAmount,
    decimal YearYieldPercentage);
