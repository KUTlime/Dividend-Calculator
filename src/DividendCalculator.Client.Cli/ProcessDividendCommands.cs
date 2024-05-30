namespace DividendCalculator.Client.Cli;

public class ProcessDividendCommands
{
    private readonly IDividendCalendarParser<string> _dividendCalendarParser;
    private readonly IOrdersParser<string> _ordersParser;
    private readonly IDividendParser<string> _dividendParser;

    public ProcessDividendCommands(
        IDividendCalendarParser<string> dividendCalendarParser,
        IOrdersParser<string> ordersParser,
        IDividendParser<string> dividendParser)
    {
        _dividendCalendarParser = dividendCalendarParser;
        _ordersParser = ordersParser;
        _dividendParser = dividendParser;
    }

    public async Task Process(
        [Argument] string calendarJsonFilePath,
        [Argument] string ordersJsonFilePath,
        [Argument] string dividendsJsonFilePath,
        [FromService] ICoconaAppContextAccessor contextAccessor)
    {
        var calendarFilePath = new FileInfo(calendarJsonFilePath);
        var ordersFilePath = new FileInfo(ordersJsonFilePath);
        var dividendsFilePath = new FileInfo(dividendsJsonFilePath);
        var cancellationToken = contextAccessor.Current?.CancellationToken ?? default;
        string calendarJson = await File.ReadAllTextAsync(calendarFilePath.FullName, cancellationToken);
        string ordersJson = await File.ReadAllTextAsync(ordersFilePath.FullName, cancellationToken);
        string dividendsJson = await File.ReadAllTextAsync(dividendsFilePath.FullName, cancellationToken);
        var dividendCalendarItems = _dividendCalendarParser.Parse(calendarJson).ToList();
        var stockTradeOrders = _ordersParser.Parse(ordersJson).ToList();
        var dividendPayments = _dividendParser.Parse(dividendsJson).ToList();
        var results = DividendYieldCalculator.Calculate(dividendCalendarItems, stockTradeOrders, dividendPayments);

        results
            .ToList()
            .ForEach(PrintYields);
    }

    private static void PrintYields(DividendYearYieldResult result) =>
        Console.WriteLine($"| Stock: {result.Stock.Name,-20} | Total investment: {result.InvestmentAmount:C} | Year: {result.Year} | Yield: {result.YearYieldAmount:C} | Yield percentage: {result.YearYieldPercentage:P} ");
}
