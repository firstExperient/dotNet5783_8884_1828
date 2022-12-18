using System;

namespace BO;

/// <summary>
/// exception for when a value that should be not-negative contain a negative number
/// </summary>
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


/// <summary>
/// exception for when an integrity damage has occured (like worng dates order)
/// </summary>
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

/// <summary>
/// exception for when a value that must be not null contains a null value
/// </summary>
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

/// <summary>
/// exception for when an item wasn't found
/// </summary>
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


/// <summary>
/// exception for when there is an attempt to add an item that already exists
/// </summary>
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


/// <summary>
/// exception for when there is an attempt to consume a product that is out of stock
/// </summary>
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

public class AccessToDataFailedException : Exception
{

    public AccessToDataFailedException(string msg) : base(msg)
    {

    }
    public override string ToString()
    {

        return $@"
        ERROR - AccessToDataFailedException:
        {Message}
        ";
    }
}
