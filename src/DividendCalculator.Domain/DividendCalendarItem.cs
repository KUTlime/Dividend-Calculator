namespace DividendCalculator.Domain;

public record DividendCalendarItem(Stock Stock, DateTime ExDividendDate, DateTime RecordDate, decimal DividendPerShare);
