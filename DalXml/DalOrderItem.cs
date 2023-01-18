using DO;
//using static DalXml.DataSource;
using System.Diagnostics;
using DalApi;
using System.Text.RegularExpressions;
using System.Linq;

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
        List<OrderItem?> orderItems = (List<OrderItem?>)FilesManage<OrderItem?>.ReadList("OrderItems.xml");
        orderItems.Add(orderItem);
        FilesManage<OrderItem?>.SaveList(orderItems, "OrderItems.xml");
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
        return FilesManage<OrderItem?>.ReadList("OrderItems.xml").Where(match).FirstOrDefault() ?? throw new Exception("not found");
    }

    /// <summary>
    /// a function that returns all the order-items
    /// </summary>
    /// <returns>an array of all order-items</returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?,bool>? match)
    {
        return FilesManage<OrderItem?>.ReadList("OrderItems.xml").Where(match);
    }
    #endregion

    #region Update
    /// <summary>
    /// this function gets an order-item to update it's details
    /// </summary>
    /// <param name="orderItem">the order-item to update</param>
    public void Update(OrderItem orderItem)
    {
        List<OrderItem?> orderItems = (List<OrderItem?>)FilesManage<OrderItem?>.ReadList("OrderItemss.xml");

        bool flag = false;
        for (int i = 0; i < orderItems.Count; i++)
        {
            if (orderItems[i]?.ID == orderItem.ID)
            {
                orderItems[i] = orderItem;
                flag = true;
                break;
            }
        }
        FilesManage<OrderItem?>.SaveList(orderItems, "OrderItems.xml");
        if (!flag) throw new NotFoundException("OrderItem not found");
    }

    #endregion

    #region Delete
    /// <summary>
    /// this fuction delete's an order-item by the given ID 
    /// </summary>
    /// <param name="id">the ID of the order-item to delete</param>
    public void Delete(int id)
    {
        //read the list, and save again with only the order-items with Id different than the parameter
        FilesManage<OrderItem?>.SaveList(FilesManage<OrderItem?>.ReadList("OrderItems.xml").Where(x => x?.ID != id), "OrderItems.xml");
    }

    #endregion
}