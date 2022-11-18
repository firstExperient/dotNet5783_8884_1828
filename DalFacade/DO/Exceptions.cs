namespace DO;

public class NotFoundException : Exception
{
    public NotFoundException(string msg) : base(msg)
    {

    }
}

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string msg) : base(msg)
    {

    }
}