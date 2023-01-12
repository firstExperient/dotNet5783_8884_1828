using DO;
using static Dal.DataSource;
using System.Diagnostics;
using DalApi;
using System.Text.RegularExpressions;

namespace Dal;

internal class DalOrderItem: IOrderItem
{
    #region Add
    /// <summary>
    /// this function is used when there is a new order-item
    /// </summary>
    /// <param name="orderItem"> ID of the added order-item</param>
    /// <returns>order-item ID of the added order-item</returns>
    public int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSource.Config.OrderItemId;
        DataSource.OrderItems.Add(orderItem);
        return orderItem.ID;
    }

    #endregion

    #region Get
    /// <summary>
    /// a function that returns the specific order-item that was asked
    /// </summary>
    /// <param name="id">ID of order-item to get</param>
    /// <returns>the order-item that has the given ID</returns>
    public OrderItem Get(Func<OrderItem?,bool> match)
    {
        return DataSource.OrderItems.Where(match).FirstOrDefault() ?? throw new NotFoundException("Order item not found");
    }

    /// <summary>
    /// a function that returns all the order-items
    /// </summary>
    /// <returns>an array of all order-items</returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?,bool>? match)
    {
        if (match == null)
            return new List<OrderItem?>(DataSource.OrderItems);
        return DataSource.OrderItems.Where(match);
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
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (DataSource.OrderItems[i]?.ID == orderItem.ID)
            {
                DataSource.OrderItems[i] = orderItem;
                flag = true;
                break;
            }
        }
        if (!flag) throw new NotFoundException("Order item not found");
    }

    #endregion

    #region Delete
    /// <summary>
    /// this fuction delete's an order-item by the given ID 
    /// </summary>
    /// <param name="id">the ID of the order-item to delete</param>
    public void Delete(int id)
    {
        DataSource.OrderItems.RemoveAll(x => x?.ID == id);
    }

    #endregion
}
