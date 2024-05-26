namespace DividendCalculator.Business;

public class JsonDividendCalendarParser : IDividendCalendarParser<string>
{
    public IEnumerable<DividendCalendarItem> Parse(string json) =>
        JsonSerializer.Deserialize<DividendCalendarItem[]>(json) ?? [];
}
