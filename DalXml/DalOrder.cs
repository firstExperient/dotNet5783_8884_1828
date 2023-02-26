using DalApi;
using DO;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Dal;
 internal class DalOrder:IOrder 
{
    string _path = "Orders.xml";
    string _configPath = "Config.xml";
    #region Add
    /// <summary>
    /// this function is used when there is a new order
    /// </summary>
    /// <param name="order">the new order to add</param>
    /// <returns> ID of the added order</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order order)
    {
        //config the id for the new order
        XElement Config = FilesManage.ReadXml(_configPath);
        order.ID = int.Parse(Config.Element("OrderId")!.Value);
        Config.Element("OrderId")!.Value = (order.ID + 1).ToString();


        List<Order?> orders = FilesManage.ReadList<Order?>(_path);
        orders.Add(order);
        FilesManage.SaveList(orders, _path);
        FilesManage.SaveXml(Config, _configPath);
        return order.ID;
    }

    #endregion

    #region Get

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(Func<Order?,bool> match)
    {
        return FilesManage.ReadList<Order?>(_path).Where(match).FirstOrDefault() ?? throw new NotFoundException("Order not found");
    }

    /// <summary>
    /// a function that returns all the orders
    /// </summary>
    /// <returns>a list of all orders</returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order?> GetAll(Func<Order?,bool>? match)
    {
        if (match == null)
            return FilesManage.ReadList<Order?>(_path);
        return FilesManage.ReadList<Order?>(_path).Where(match);
    }

    #endregion

    #region Update

    /// <summary>
    /// this function gets an order to update it's details
    /// </summary>
    /// <param name="order">the order to update</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order order)
    {
        List<Order?> orders = FilesManage.ReadList<Order?>(_path);
        
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
        FilesManage.SaveList(orders, _path);
        if (!flag) throw new NotFoundException("Order not found");
    }

    #endregion

    #region Delete

    /// <summary>
    /// this fuction delete's an order by the given ID
    /// </summary>
    /// <param name="id">the ID of the order to delete</param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        //read the list, and save again with only the orders with Id different than the parameter
        List<Order?> orders = FilesManage.ReadList<Order?>(_path);
        orders.RemoveAll(x => x?.ID == id);
        FilesManage.SaveList(orders, _path);
    }

    #endregion
}