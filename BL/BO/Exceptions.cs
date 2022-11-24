﻿namespace BO;

public class NegativeNumberException : Exception
{
    public NegativeNumberException(string msg) : base(msg)
    {

    }
}

public class IntegrityDamageException : Exception
{
    public IntegrityDamageException(string msg) : base(msg)
    {

    }
}

public class NullValueException : Exception
{
    public NullValueException(string msg) : base(msg)
    {

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
        
        
        return Message;
    }
}

public class OutOfStockException : Exception
{
    public OutOfStockException(string msg) : base(msg)
    {

    }
}

public class NotYetShippedException : Exception
{
    public NotYetShippedException(string msg) : base(msg)
    {

    }
}

public class NotYetDeliveredException : Exception
{
    public NotYetDeliveredException(string msg) : base(msg)
    {

    }
}