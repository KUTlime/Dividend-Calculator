namespace DividendCalculator.Business;

public class JsonDividendPaymentParser : IDividendParser<string>
{
    public IEnumerable<DividendPayment> Parse(string json) => JsonSerializer.Deserialize<DividendPayment[]>(json) ?? [];
}
