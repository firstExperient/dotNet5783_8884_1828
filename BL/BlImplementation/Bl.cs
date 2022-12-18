using BlApi;
namespace BlImplementation;

internal sealed class Bl : IBl
{
    public IProduct Product => new Product();
    public ICart Cart => new Cart();
    public IOrder Order => new Order();
}
