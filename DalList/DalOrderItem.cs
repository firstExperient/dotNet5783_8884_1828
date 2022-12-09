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
    //public OrderItem Get(int id)
    //{
    //    for (int i = 0; i < DataSource.OrderItems.Count; i++)
    //    {
    //        if (DataSource.OrderItems[i].HasValue &&  DataSource.OrderItems[i]!.Value.ID == id) return (OrderItem)DataSource.OrderItems[i]!;
    //    }
    //    throw new NotFoundException("Order item not found");
    //}
    public OrderItem Get(Predicate<OrderItem?> match)
    {
        OrderItem? orderItem = DataSource.OrderItems.Find(match);
        if(orderItem == null) throw new NotFoundException("Order item not found");
        return (OrderItem)orderItem!;
    }

    /// <summary>
    /// a function that returns all the order-items
    /// </summary>
    /// <returns>an array of all order-items</returns>
    public IEnumerable<OrderItem?> GetAll(Predicate<OrderItem?>? match)
    {
        if (match == null)
            return new List<OrderItem?>(DataSource.OrderItems);
        return DataSource.OrderItems.FindAll(match);
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
            if (DataSource.OrderItems[i].HasValue && DataSource.OrderItems[i]!.Value.ID == orderItem.ID)
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
        bool flag = false;
        int i = 0;
        for (; i < DataSource.OrderItems.Count; i++)
        {
            if (DataSource.OrderItems[i].HasValue && DataSource.OrderItems[i]!.Value.ID == id)
            {
                flag = true;
                break;
            }
        }
        if (!flag) throw new NotFoundException("Order item not found");
        else
            DataSource.OrderItems.RemoveAt(i);
    }

    #endregion
}
