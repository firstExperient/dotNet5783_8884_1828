using DalApi;
namespace Dal;

internal sealed class DalList : IDal
{
    private DalList() { }

    public IProduct Product { get; } = new DalProduct();
    public IOrder Order { get; } = new DalOrder();
    public IOrderItem OrderItem { get; } = new DalOrderItem();


    //using c# bulit in Lazy class to provide fully Lazy  and Thread Safe Initialization
    //(Lazy<T> is by deafult Thread Safe, and the value is not initalize untill the first access to the value property)
    private static readonly Lazy<IDal> _instance = new Lazy<IDal>(() => new DalList());
    public static IDal Instance { get { return _instance.Value; } }
}