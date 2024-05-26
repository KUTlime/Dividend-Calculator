namespace DividendCalculator.Business;

public class JsonTradeOrdersParser : IOrdersParser<string>
{
    public IEnumerable<StockTradeOrder> Parse(string json) =>
        JsonSerializer.Deserialize<StockTradeOrder[]>(json) ?? [];
}
