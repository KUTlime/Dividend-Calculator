namespace DividendCalculator.Domain;

public record StockTradeOrder(DateTime Date, Stock Stock, decimal TotalAmount, int Units, decimal UnitPrice);
