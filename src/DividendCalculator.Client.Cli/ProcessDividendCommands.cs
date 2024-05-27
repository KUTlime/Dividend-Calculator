namespace DividendCalculator.Client.Cli;

public class ProcessDividendCommands
{
    private readonly IDividendCalendarParser<string> _dividendCalendarParser;
    private readonly IOrdersParser<string> _ordersParser;

    public ProcessDividendCommands(
        IDividendCalendarParser<string> dividendCalendarParser,
        IOrdersParser<string> ordersParser)
    {
        _dividendCalendarParser = dividendCalendarParser;
        _ordersParser = ordersParser;
    }

    public async Task Add(
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
        var dividendPayments = _dividendCalendarParser.Parse(dividendsJson).ToList();
        Console.WriteLine($"{calendarJsonFilePath}");
        Console.WriteLine($"{dividendCalendarItems.Count}");
        Console.WriteLine($"{stockTradeOrders.Count}");
        Console.WriteLine($"{dividendPayments.Count}");
        await Task.Run(() => { }, cancellationToken);
    }
}
