namespace LogStandartisation.Exceptions;

public class InvalidTimeFormat : Exception
{
    public InvalidTimeFormat(string message = "") : base(message) {}
}