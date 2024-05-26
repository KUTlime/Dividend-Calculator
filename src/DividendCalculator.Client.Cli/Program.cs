var builder = CoconaApp.CreateBuilder();

builder.Services.AddTransient<IDividendCalendarParser<string>, JsonDividendCalendarParser>();
builder.Services.AddTransient<IDividendParser<string>, JsonDividendPaymentParser>();
builder.Services.AddTransient<IOrdersParser<string>, JsonTradeOrdersParser>();

var app = builder.Build();

await app.RunAsync<ProcessDividendCommands>();
