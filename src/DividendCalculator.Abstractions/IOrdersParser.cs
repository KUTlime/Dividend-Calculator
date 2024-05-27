namespace DividendCalculator.Abstractions;

public interface IOrdersParser<in TIn>
{
    public IEnumerable<StockTradeOrder> Parse(TIn inputData);
}
