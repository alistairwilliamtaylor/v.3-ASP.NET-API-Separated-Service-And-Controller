namespace FirstWebApp.Exceptions;

public class ForeignKeyDoesNotExistException : Exception
{
    public ForeignKeyDoesNotExistException() { }
    public ForeignKeyDoesNotExistException(string message) : base(message){ }
    public ForeignKeyDoesNotExistException(string message, Exception inner) : base(message, inner) { }
}