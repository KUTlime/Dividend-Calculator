namespace DividendCalculator.Abstractions;

public interface IDividendCalendarParser<in TIn>
{
    public IEnumerable<DividendCalendarItem> Parse(TIn inputData);
}
