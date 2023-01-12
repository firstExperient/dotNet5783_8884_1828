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
        order.ID = DataSource.Config.OrderId;
        DataSource.Orders.Add(order);
        return order.ID;
    }

    #endregion

    #region Get

    /// <summary>
    /// a function that returns the specific order that was asked
    /// </summary>
    /// <param name="id">ID of order to get</param>
    /// <returns>the order that has the given ID</returns>
    //public Order Get(int id)
    //{
    //    for (int i = 0; i < DataSource.Orders.Count; i++)
    //    {
    //        if (DataSource.Orders[i].HasValue &&  DataSource.Orders[i]!.Value.ID == id) return (Order)DataSource.Orders[i]!;
    //    }
    //    throw new NotFoundException("Order not found");
    //}

    public Order Get(Func<Order?,bool> match)
    {
        return DataSource.Orders.Where(match).FirstOrDefault() ?? throw new NotFoundException("Order not found");
    }

    /// <summary>
    /// a function that returns all the orders
    /// </summary>
    /// <returns>a list of all orders</returns>
    public IEnumerable<Order?> GetAll(Func<Order?,bool>? match)
    {
        if(match == null)
            return new List<Order?>(DataSource.Orders);
        return DataSource.Orders.Where(match);
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
        for (int i = 0; i < DataSource.Orders.Count; i++)
        {
            if (DataSource.Orders[i]?.ID == order.ID)
            {
                DataSource.Orders[i] = order;
                flag = true;
                break;
            }
        }
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
        DataSource.Orders.RemoveAll(x => x?.ID == id);
    }

    #endregion
 }