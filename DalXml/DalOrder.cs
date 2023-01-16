using DalApi;
using DO;
using System.IO;
using System.Xml.Serialization;

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
        
    }

    #endregion

    #region Get

    public Order Get(Func<Order?,bool> match)
    {
        XmlSerializer xs = new XmlSerializer(typeof(IEnumerable<Order?>));
        StreamReader sr = new StreamReader("xml/Orders");
        //קריאת האוביקט שנשמר
        IEnumerable<Order?> orders = xs.Deserialize(sr) as IEnumerable<Order?> ?? throw new Exception("fix this");
        sr.Close();
        return orders.Where(match).FirstOrDefault() ?? throw new Exception("not found");
    }

    /// <summary>
    /// a function that returns all the orders
    /// </summary>
    /// <returns>a list of all orders</returns>
    public IEnumerable<Order?> GetAll(Func<Order?,bool>? match)
    {
        XmlSerializer xs = new XmlSerializer(typeof(IEnumerable<Order?>));
        StreamReader sr = new StreamReader("xml/Orders");
        //קריאת האוביקט שנשמר
        IEnumerable<Order?> orders = xs.Deserialize(sr) as IEnumerable<Order?> ?? throw new Exception("fix this");
        sr.Close();
        //fix this
        return orders.Where(match);
    }

    #endregion

    #region Update

    /// <summary>
    /// this function gets an order to update it's details
    /// </summary>
    /// <param name="order">the order to update</param>
    public void Update(Order order)
    {
       
    }

    #endregion

    #region Delete

    /// <summary>
    /// this fuction delete's an order by the given ID
    /// </summary>
    /// <param name="id">the ID of the order to delete</param>
    public void Delete(int id)
    {
    }

    #endregion
 }