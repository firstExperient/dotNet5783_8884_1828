
using DO;
namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    OrderItem GetItemByIds(int orderId, int productId);

    IEnumerable<OrderItem> GetAllItemsInOrder(int orderId);
}
