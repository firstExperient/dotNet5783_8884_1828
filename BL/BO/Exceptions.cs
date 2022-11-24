using System;

namespace BO;

/// <summary>
/// a class for all exceptions:
/// </summary>
public class NegativeNumberException : Exception
{

    /// <summary>
    /// exception for when user entered a negative number (usually for ID's)
    /// </summary>
    /// <param name="msg">the messege the user will rescive</param>
    public NegativeNumberException(string msg) : base(msg)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>a string of the error</returns>
    public override string ToString()
    {
        
        return $@"
        ERROR - NegativeNumberException:
        {Message}
        "; 
    }
}


/// <summary>
/// exception for when an integrity damage has occured
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
/// exception for when a value is null
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
/// exception for when an item has'nt found
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
/// exception for when an item already exists
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
/// exception for when an item is out of stock
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

