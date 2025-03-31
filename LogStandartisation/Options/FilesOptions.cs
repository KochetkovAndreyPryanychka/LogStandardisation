namespace LogStandartisation.Options;

public record FilesOptions
{
    public string OldLogsFile { get; set; }
    
    public string NewLogsFile { get; set; }
    
    public string PathFromExeToOutputFolder { get; set; }
        
    public string PathToOutputFolder { get; set; }
}