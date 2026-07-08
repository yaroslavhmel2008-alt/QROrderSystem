namespace QROrderSystem.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string name, string message) 
        : base($"{name} error: {message}")
    {
    }
}