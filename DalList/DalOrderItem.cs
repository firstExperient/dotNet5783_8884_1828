
using DO;

namespace Dal;

public class DalOrderItem
{

    #region Add
    public int Add(OrderItem orderItem)
    {
        if (DataSource.Config.OrderItemsIndex == 199) throw new Exception("Erorr! OrderItems array is full");
        orderItem.ID = DataSource.Config.OrderItemId;
        DataSource.OrderItems[DataSource.Config.OrderItemsIndex] = orderItem;
        DataSource.Config.OrderItemsIndex++;
        return orderItem.ID;
    }

    #endregion

    #region Get
    public OrderItem Get(int id)
    {
        for (int i = 0; i < DataSource.Config.OrderItemsIndex; i++)
        {
            if (DataSource.OrderItems[i].ID == id) return DataSource.OrderItems[i];
        }
        throw new Exception("Order item not found");
    }

    public OrderItem[] GetAll()
    {
        OrderItem[] orderItems = new OrderItem[DataSource.Config.OrderItemsIndex];
        for (int i = 0; i < DataSource.Config.OrderItemsIndex; i++)
        {
            orderItems[i] = DataSource.OrderItems[i];
        }
        return orderItems;
    }

    public OrderItem GetItemByIds(int orderId,int productId)
    {
        for (int i = 0; i < DataSource.Config.OrderItemsIndex; i++)
        {
            if (DataSource.OrderItems[i].OrderId == orderId && DataSource.OrderItems[i].ProductId == productId) 
                return DataSource.OrderItems[i];
        }
        throw new Exception("Order item not found");
    }

    public OrderItem[] GetAllItemsInOrder(int orderId)
    {
        OrderItem[] orderItems = Array.FindAll(DataSource.OrderItems, (orderItem) => orderItem.OrderId == orderId);
        return orderItems;
    }

    #endregion

    #region Update

    public void Update(OrderItem orderItem)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.OrderItemsIndex; i++)
        {
            if (DataSource.OrderItems[i].ID == orderItem.ID)
            {
                DataSource.OrderItems[i] = orderItem;
                flag = true;
                break;
            }
        }
        if (!flag) throw new Exception("Order item not found");
    }

    #endregion

    #region Delete

    public void Delete(int id)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.OrderItemsIndex; i++)
        {
            if (DataSource.OrderItems[i].ID == id)
            {
                flag = true;
                for (; i < DataSource.Config.OrderItemsIndex - 1; i++)
                {
                    DataSource.OrderItems[i] = DataSource.OrderItems[i + 1];
                }
                DataSource.Config.OrderItemsIndex--;
                break;
            }
        }
        if (!flag) throw new Exception("Order item not found");
    }

    #endregion

}
