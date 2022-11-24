using System;

namespace BO;

public class NegativeNumberException : Exception
{
    public NegativeNumberException(string msg) : base(msg)
    {

    }
    public override string ToString()
    {
        
        return $@"
        ERROR - NegativeNumberException:
        {Message}
        "; 
    }
}

public class IntegrityDamageException : Exception
{
    public IntegrityDamageException(string msg) : base(msg)
    {

    }

    public override string ToString()
    {

        return $@"
        ERROR - IntegrityDamageException:
        {Message}
        "; 
    }
}

public class NullValueException : Exception
{
    public NullValueException(string msg) : base(msg)
    {

    }

    public override string ToString()
    {

        return $@"
        ERROR - NullValueException:
        {Message}
        "; 
    }
}

public class NotFoundException : Exception
{
    public NotFoundException(string msg) : base(msg)
    {

    }
    public NotFoundException(string msg ,Exception innerException):base(msg ,innerException)
    {
    }

    public override string ToString()
    {
        string innerException = (InnerException != null) ? "Inner exception:" + InnerException : "";
        return $@"
        ERROR - NotFoundException:
        {Message}
        {innerException}
        "; 
    }
}

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string msg) : base(msg)
    {

    }
    public AlreadyExistsException(string msg, Exception innerException) : base(msg, innerException)
    {
    }

    public override string ToString()
    {
        string innerException = (InnerException != null) ? "Inner exception:" + InnerException : "";
        return $@"
        ERROR - AlreadyExistsException:
        {Message}
        {innerException}
        ";
    }
}

public class OutOfStockException : Exception
{
    public OutOfStockException(string msg) : base(msg)
    {

    }

    public override string ToString()
    {

        return $@"
        ERROR - OutOfStockException:
        {Message}
        ";
    }
}

