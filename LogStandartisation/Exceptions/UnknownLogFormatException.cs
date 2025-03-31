namespace LogStandartisation.Exceptions;

public class UnknownLogFormatException : Exception
{
    public UnknownLogFormatException(string message = "") : base(message) {}
}