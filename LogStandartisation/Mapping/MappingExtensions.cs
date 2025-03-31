using LogStandartisation.Enums;

namespace LogStandartisation.Mapping;

public static class MappingExtensions
{
    public static LogLevelEnum ToLogLevelEnum(this string logFormat) =>
        logFormat switch
        {
            "INFO" => LogLevelEnum.Info,
            "INFORMATION" => LogLevelEnum.Info,
            "WARN" => LogLevelEnum.Warn,
            "WARNING" => LogLevelEnum.Warn,
            "ERROR" => LogLevelEnum.Error,
            "DEBUG" => LogLevelEnum.Debug,
            _ => LogLevelEnum.Unknown
        };
}