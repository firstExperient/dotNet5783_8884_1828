using DalApi;
using DO;

namespace Dal;
 internal class DalOrder:IOrder 
{
    #region Add
    /// <summary>
    /// this function is used when there is a new order
    /// </summary>
    /// <param name="order">the new order to add</param>
    /// <returns> ID of the added order</returns>
    public int Add(Order order)
    {
        List<Order?> orders = (List<Order?>)FilesManage<Order?>.ReadList("Orders.xml");
        orders.Add(order);
        FilesManage<Order?>.SaveList(orders, "Orders.xml");
        return order.ID;
    }

    #endregion

    #region Get

    public Order Get(Func<Order?,bool> match)
    {
        return FilesManage<Order?>.ReadList("Orders.xml").Where(match).FirstOrDefault() ?? throw new Exception("not found");
    }

    /// <summary>
    /// a function that returns all the orders
    /// </summary>
    /// <returns>a list of all orders</returns>
    public IEnumerable<Order?> GetAll(Func<Order?,bool>? match)
    {
        return FilesManage<Order?>.ReadList("Orders.xml").Where(match);
    }

    #endregion

    #region Update

    /// <summary>
    /// this function gets an order to update it's details
    /// </summary>
    /// <param name="order">the order to update</param>
    public void Update(Order order)
    {
        List<Order?> orders = (List<Order?>)FilesManage<Order?>.ReadList("Orders.xml");
        
        bool flag = false;
        for (int i = 0; i < orders.Count; i++)
        {
            if (orders[i]?.ID == order.ID)
            {
                orders[i] = order;
                flag = true;
                break;
            }
        }
        FilesManage<Order?>.SaveList(orders, "Orders.xml");
        if (!flag) throw new NotFoundException("Order not found");
    }

    #endregion

    #region Delete

    /// <summary>
    /// this fuction delete's an order by the given ID
    /// </summary>
    /// <param name="id">the ID of the order to delete</param>
    public void Delete(int id)
    {
        //read the list, and save again with only the orders with Id different than the parameter
        FilesManage<Order?>.SaveList((List<Order?>)FilesManage<Order?>.ReadList("Orders.xml").Where(x => x?.ID != id), "Orders.xml");
    }

    #endregion
}