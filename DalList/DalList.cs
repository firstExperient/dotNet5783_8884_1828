using DalApi;
using System.Security.Principal;

namespace Dal;

internal sealed class DalList : IDal
{
    private DalList() { }

    public IProduct Product => new DalProduct();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();

    public static IDal Instance { get; } = new DalList();
}