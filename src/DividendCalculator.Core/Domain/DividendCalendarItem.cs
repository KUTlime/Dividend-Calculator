namespace DividendCalculator.Core.Domain;

public record DividendCalendarItem(Stock Stock, DateTime ExDividendDate, DateTime RecordDate);
