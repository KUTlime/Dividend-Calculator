namespace DividendCalculator.Abstractions;

public interface IDividendParser<in TIn>
{
    public IEnumerable<DividendPayment> Parse(TIn inputData);
}
