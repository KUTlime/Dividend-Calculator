namespace DividendCalculator.Domain;

public record DividendPayment(DateTime Date, Stock Stock, decimal TotalAmount, decimal AmountPerUnit);
