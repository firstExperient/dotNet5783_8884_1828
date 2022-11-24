namespace DO;

public class NotFoundException : Exception
{
    public NotFoundException(string msg) : base(msg)
    {

    }
    public override string ToString()
    {
        return $@"
          database NotFoundException:
          {Message}
        ";
    }
}

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string msg) : base(msg)
    {

    }
    public override string ToString()
    {
        return $@"
          database AlreadyExistsException:
          {Message}
        ";
    }
}