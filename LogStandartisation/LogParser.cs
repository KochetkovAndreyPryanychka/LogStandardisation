using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using LogStandartisation.Enums;
using LogStandartisation.Exceptions;
using LogStandartisation.Mapping;
using LogStandartisation.Models;

namespace LogStandartisation;

public static class LogParser
{
    public static Log Parse(string inputString) =>
        GetLogFormat(inputString) switch
        {
            LogFormatEnum.Unknown => throw new UnknownLogFormatException(),
            LogFormatEnum.SpacesOnly => ParseSpaceOnlyFormatLog(inputString),
            LogFormatEnum.WithSlash => ParseWithSlashFormatLog(inputString),
            _ => throw new UnknownLogFormatException()
        };

    private static LogFormatEnum GetLogFormat(string logString) => 
        DateTimeOffset.TryParse(logString.Split('|')[0], out _) ?
            LogFormatEnum.WithSlash : 
            LogFormatEnum.SpacesOnly;

    private static Log ParseSpaceOnlyFormatLog(string logString)
    {
        var logComponents = logString.Split(' ');

        var logTime = GetTime(logComponents[..2]);
        var logLevel = logComponents[2].ToLogLevelEnum();
        var logMessage = logComponents[3..].Aggregate("", (i, c) => i + c);

        return new Log(logTime, logLevel, logMessage);
    }
    
    private static Log ParseWithSlashFormatLog(string logString)
    {
        var logComponents = 
            logString
                .Split('|')
                .Select(s => s.TrimStart())
                .Select(s => s.TrimEnd())
                .ToArray();

        var logTime = GetTime(logComponents[0]);
        var logLevel = logComponents[1].ToLogLevelEnum();
        var logInvokedBy = logComponents[3];
        var logMessage = logComponents[4..].Aggregate("", (i, c) => i + c);

        return new Log(logTime, logLevel, logMessage, logInvokedBy);
    }

    private static DateTimeOffset GetTime(string timeString) =>
        DateTimeOffset.TryParse(timeString, out var logTime)
            ? logTime
            : throw new InvalidTimeFormat();
    
    private static DateTimeOffset GetTime(IEnumerable<string> components) =>
        DateTimeOffset.TryParse(components.Aggregate("", (i, c) => i + " " + c), out var logTime)
            ? logTime
            : throw new InvalidTimeFormat();
}