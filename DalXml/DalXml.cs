using Dal;
using DalApi;
using System.Security.Principal;

namespace Dal;
internal sealed class DalXml : IDal
{
    private DalXml() { }

    public IProduct Product { get; } = new DalProduct();

    public IOrder Order { get; } = new DalOrder();

    public IOrderItem OrderItem { get; } = new DalOrderItem();


    //using c# bulit in Lazy class to provide fully Lazy  and Thread Safe Initialization
    //(Lazy<T> is by deafult Thread Safe, and the value is not initalize untill the first access to the value property)
    private static readonly Lazy<IDal> _instance = new Lazy<IDal>(() => new DalXml());
    public static IDal Instance { get { return _instance.Value; } }
}
