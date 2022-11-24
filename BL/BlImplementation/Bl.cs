using BlApi;
using System.Security.Principal;


namespace BlImplementation;

sealed public class Bl:IBl
{
    public IProduct Product => new Product();
    public ICart Cart => new Cart();
    public IOrder Order => new Order();

}
