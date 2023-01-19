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
        //fix this
        List<Order?> orders = FilesManage.ReadList<Order?>("Orders.xml");
        orders.Add(order);
        FilesManage.SaveList(orders, "Orders.xml");
        return order.ID;
    }

    #endregion

    #region Get

    public Order Get(Func<Order?,bool> match)
    {
        return FilesManage.ReadList<Order?>("Orders.xml").Where(match).FirstOrDefault() ?? throw new NotFoundException("Order not found");
    }

    /// <summary>
    /// a function that returns all the orders
    /// </summary>
    /// <returns>a list of all orders</returns>
    public IEnumerable<Order?> GetAll(Func<Order?,bool>? match)
    {
        if (match == null)
            return FilesManage.ReadList<Order?>("Orders.xml");
        return FilesManage.ReadList<Order?>("Orders.xml").Where(match);
    }

    #endregion

    #region Update

    /// <summary>
    /// this function gets an order to update it's details
    /// </summary>
    /// <param name="order">the order to update</param>
    public void Update(Order order)
    {
        List<Order?> orders = FilesManage.ReadList<Order?>("Orders.xml");
        
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
        FilesManage.SaveList(orders, "Orders.xml");
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
        List<Order?> orders = FilesManage.ReadList<Order?>("Orders.xml");
        orders.RemoveAll(x => x?.ID == id);
        FilesManage.SaveList<Order?>(orders, "Orders.xml");
    }

    #endregion
}