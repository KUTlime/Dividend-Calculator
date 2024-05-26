namespace DividendCalculator.Client.Cli;

public record CommonParameters(
    [Option('c', Description = "Specifies the dividend calendar JSON file.")]
    FileInfo CalendarJsonFilePath) : ICommandParameterSet;
