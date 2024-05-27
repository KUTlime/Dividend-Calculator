namespace DividendCalculator.Business;

internal static class StockTradeOrderExtensions
{
    internal static bool IsTheSameStock(this StockTradeOrder tradeOrder, Stock stock) => string.CompareOrdinal(tradeOrder.Stock.Name, stock.Name) is 0;
}
