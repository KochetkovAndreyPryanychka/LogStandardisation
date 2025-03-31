using LogStandartisation.Options;
using Microsoft.Extensions.Options;

namespace LogStandartisation;

public class FormattingLogs
{
    private readonly FilesOptions _filesOptions;

    public FormattingLogs(IOptions<FilesOptions> filesOptions)
    {
        _filesOptions = filesOptions.Value;
    }

    public async Task Run()
    {
        var logs = File.ReadLinesAsync(_filesOptions.OldLogsFile);
        var newLogs = new List<string>();
        
        await foreach (var log in logs)
        {
            newLogs.Add(LogParser.Parse(log).ToString());
        }

        var outputFilePath = _filesOptions.PathFromExeToOutputFolder + _filesOptions.NewLogsFile;       //Можно использовать _filesOptions.PathToOutputFolder вместо _filesOptions.PathFromExeToOutputFolder для асболютного пути до папки с новыми логами
        await File.WriteAllLinesAsync(outputFilePath, newLogs);
    }
}