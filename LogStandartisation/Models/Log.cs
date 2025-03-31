using LogStandartisation.Enums;

namespace LogStandartisation.Models;

public record Log(DateTimeOffset CreatedAt, LogLevelEnum LogLevel, string Message, string? InvokedBy = null)
{
    public override string ToString() =>
        $"{CreatedAt:dd-MM-yyyy}\t" +
        $"{CreatedAt.TimeOfDay.ToString().TrimEnd('0')}\t" +
        $"{LogLevel.ToString()}\t" +
        $"{InvokedBy ?? "DEFAULT"}\t" +
        $"{Message}";
}