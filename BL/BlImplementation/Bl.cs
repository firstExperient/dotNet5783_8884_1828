using BlApi;
namespace BlImplementation;

sealed public class Bl:IBl
{
    public IProduct Product => new Product();
    public ICart Cart => new Cart();
    public IOrder Order => new Order();
}
