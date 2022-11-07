using DO;
namespace Dal;

public class DalOrder
{
    #region Add
    /// <summary>
    /// this function is used when there is a new order
    /// </summary>
    /// <param name="order">the new order to add</param>
    /// <returns> ID of the added order</returns>
    public int Add(Order order)
    {
        if (DataSource.Config.OrdersIndex == 99) throw new Exception("Erorr! Orders array is full");
        order.ID = DataSource.Config.OrderId;
        DataSource.Orders[DataSource.Config.OrdersIndex] = order;
        DataSource.Config.OrdersIndex++;
        return order.ID;
    }

    #endregion

    #region Get

    /// <summary>
    /// a function that returns the specific order that was asked
    /// </summary>
    /// <param name="id">ID of order to get</param>
    /// <returns>the order that has the given ID</returns>
    public Order Get(int id)
    {
        for (int i = 0; i < DataSource.Config.OrdersIndex; i++)
        {
            if (DataSource.Orders[i].ID == id) return DataSource.Orders[i];
        }
        throw new Exception("Order not found");
    }

    /// <summary>
    /// a function that returns all the orders
    /// </summary>
    /// <returns>an array of all orders</returns>
    public Order[] GetAll()
    {
        Order[] orders = new Order[DataSource.Config.OrdersIndex];
        for (int i = 0; i < DataSource.Config.OrdersIndex; i++)
        {
            orders[i] = DataSource.Orders[i];
        }
        return orders;
    }

    #endregion

    #region Update

    /// <summary>
    /// this function gets an order to update it's details
    /// </summary>
    /// <param name="order">the order to update</param>
    public void Update(Order order)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.OrdersIndex; i++)
        {
            if (DataSource.Orders[i].ID == order.ID)
            {
                DataSource.Orders[i] = order;
                flag = true;
                break;
            }
        }
        if (!flag) throw new Exception("Order not found");
    }

    #endregion

    #region Delete

    /// <summary>
    /// this fuction delete's an order by the given ID
    /// </summary>
    /// <param name="id">the ID of the order to delete</param>
    public void Delete(int id)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.OrdersIndex; i++)
        {
            if (DataSource.Orders[i].ID == id)
            {
                flag = true;
                DataSource.Orders[i] = DataSource.Orders[DataSource.Config.OrdersIndex - 1];
                DataSource.Config.OrdersIndex--;
                break;
            }
        }
        if (!flag) throw new Exception("Order not found");
    }

    #endregion
}
