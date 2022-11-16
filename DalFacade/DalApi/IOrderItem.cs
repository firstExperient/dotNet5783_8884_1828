
using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    OrderItem GetItemByIds(int orderId, int productId);
    OrderItem[] GetAllItemsInOrder(int orderId);
}
