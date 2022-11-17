
using DO;
namespace DalApi;

internal interface IOrderItem:ICrud<OrderItem>
{
    OrderItem GetItemByIds(int orderId, int productId);

    OrderItem[] GetAllItemsInOrder(int orderId);
}
