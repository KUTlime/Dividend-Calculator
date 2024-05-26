namespace DividendCalculator.Core.Domain;

public record Stock(string Name, IEnumerable<string> AlternativeNames);
