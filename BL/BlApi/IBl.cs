namespace BlApi;

public interface IBl
{
    /// <summary>
    /// Center all of the Product methods
    /// </summary>
    public IProduct Product { get; }

    /// <summary>
    /// Center all of the Cart methods
    /// </summary>
    public ICart Cart { get; }

    /// <summary>
    /// Center all of the Order methods
    /// </summary>
    public IOrder Order { get; }
}
