

namespace DalApi;

internal interface IDal
{
    IProduct Product { get; }
    IOrder order { get; }
    IOrderItem OrderItem { get; }
}
