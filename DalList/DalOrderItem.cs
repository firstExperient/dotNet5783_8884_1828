using DO;
using static Dal.DataSource;
using System.Diagnostics;
using DalApi;
namespace Dal;

public class DalOrderItem /*: IOrderItem*/
{
    #region Add
    /// <summary>
    /// this function is used when there is a new order-item
    /// </summary>
    /// <param name="orderItem"> ID of the added order-item</param>
    /// <returns>order-item ID of the added order-item</returns>
    public int Add(OrderItem orderItem)
    {
        if (DataSource.Config.OrderItemsIndex == 199) throw new Exception("Erorr! OrderItems array is full");
        orderItem.ID = DataSource.Config.OrderItemId;
        DataSource.OrderItems.Insert(DataSource.Config.OrderItemsIndex, orderItem);
        DataSource.Config.OrderItemsIndex++;
        return orderItem.ID;
    }

    #endregion

    #region Get
    /// <summary>
    /// a function that returns the specific order-item that was asked
    /// </summary>
    /// <param name="id">ID of order-item to get</param>
    /// <returns>the order-item that has the given ID</returns>
    public OrderItem Get(int id)
    {
        for (int i = 0; i < DataSource.Config.OrderItemsIndex; i++)
        {
            if (DataSource.OrderItems.ElementAt(i).ID == id) return DataSource.OrderItems.ElementAt(i);
        }
        throw new Exception("Order item not found");
    }

    /// <summary>
    /// a function that returns all the order-items
    /// </summary>
    /// <returns>an array of all order-items</returns>
    public List<OrderItem> GetAll()
    {
        List<OrderItem> orderItems = new List<OrderItem>(DataSource.Config.OrderItemsIndex);
        for (int i = 0; i < DataSource.Config.OrderItemsIndex; i++)
        {
            orderItems[i] = DataSource.OrderItems[i];
        }
        return orderItems;
    }

    /// <summary>
    /// a function that returns the order-item that has the same orderID and productID as given in params
    /// </summary>
    /// <param name="orderId">ID of order to get in the order-item</param>
    /// <param name="productId">ID of watch to get in the order-item</param>
    /// <returns>the order-item that has the same IDs as asked</returns> 
    public OrderItem GetItemByIds(int orderId,int productId)
    {
        for (int i = 0; i < DataSource.Config.OrderItemsIndex; i++)
        {
            if (DataSource.OrderItems.ElementAt(i).OrderId == orderId && DataSource.OrderItems.ElementAt(i).ProductId == productId) 
                return DataSource.OrderItems.ElementAt(i);
        }
        throw new Exception("Order item not found");
    }

    /// <summary>
    /// this function returns all the items in the order that has the same ID as given in params
    /// </summary>
    /// <param name="orderId">ID of order</param>
    /// <returns>an array of all the items in the specific order</returns>
    public List<OrderItem> GetAllItemsInOrder(int orderId)
    {
        List<OrderItem> orderItems = DataSource.OrderItems.FindAll(orderItem => orderItem.OrderId == orderId);
        return orderItems;
    }

    #endregion

    #region Update
    /// <summary>
    /// this function gets an order-item to update it's details
    /// </summary>
    /// <param name="orderItem">the order-item to update</param>
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
    /// <summary>
    /// this fuction delete's an order-item by the given ID 
    /// </summary>
    /// <param name="id">the ID of the order-item to delete</param>
    public void Delete(int id)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.OrderItemsIndex; i++)
        {
            if (DataSource.OrderItems[i].ID == id)
            {
                flag = true;
                DataSource.OrderItems[i] = DataSource.OrderItems[DataSource.Config.OrderItemsIndex - 1];
                DataSource.Config.OrderItemsIndex--;
                break;
            }
        }
        if (!flag) throw new Exception("Order item not found");
    }

    #endregion
}
