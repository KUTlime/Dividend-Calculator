namespace DividendCalculator.Business;

public class DividendYieldCalculator
{
    public static IEnumerable<DividendYearYieldResult> Calculate(
        IEnumerable<DividendCalendarItem> calendarItems,
        IReadOnlyList<StockTradeOrder> orders,
        IReadOnlyList<DividendPayment> dividendPayments) => calendarItems
            .Select(i => CalculateStockYields(i, orders, dividendPayments))
            .GroupBy(r => new { r.Stock.Name, r.Year })
            .Select(g => new DividendYearYieldResult(
                g.First().Stock,
                g.Key.Year,
                g.Average(o => o.InvestmentAmount),
                g.Sum(d => d.YearYieldAmount),
                CalculateAveragedYearYieldReturn(g)));

    private static DividendYearYieldResult CalculateStockYields(
        DividendCalendarItem stockDividendCalendarItem,
        IReadOnlyList<StockTradeOrder> orders,
        IReadOnlyList<DividendPayment> dividendPayments)
    {
        _ = dividendPayments;
        var sharesInHoldingDuringRecordDate = orders
            .Where(o => o.IsTheSameStock(stockDividendCalendarItem.Stock))
            .Where(o => o.Date <= stockDividendCalendarItem.ExDividendDate)
            .Sum(o => o.Units) switch
        {
            > 0 => orders
                   .Where(o => o.IsTheSameStock(stockDividendCalendarItem.Stock))
                   .Where(o => o.Date <= stockDividendCalendarItem.RecordDate)
                   .ToList(),
            _ => [],
        };
        decimal yieldAmount = sharesInHoldingDuringRecordDate.Sum(o => o.Units) * stockDividendCalendarItem.DividendPerShare;
        decimal totalInvestment = sharesInHoldingDuringRecordDate.Sum(o => o.TotalAmount);
        return new(
            stockDividendCalendarItem.Stock,
            stockDividendCalendarItem.RecordDate.Year,
            totalInvestment,
            yieldAmount,
            yieldAmount / totalInvestment);
    }

    private static decimal CalculateAveragedYearYieldReturn(IEnumerable<DividendYearYieldResult> results)
    {
        var dividends = results.ToImmutableList();
        decimal averagedYieldReturn = dividends.Sum(d => d.InvestmentAmount * d.YearYieldAmount) / dividends.Sum(d => d.InvestmentAmount);
        return averagedYieldReturn / dividends.Average(d => d.InvestmentAmount) * dividends.Count;
    }
}
