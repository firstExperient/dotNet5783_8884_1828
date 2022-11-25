using BO;

namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// returns an order list of all the orders in the database. 
    /// </summary>
    /// <returns>list of OrderForList</returns>
    public IEnumerable<OrderForList> GetAll();

    /// <summary>
    /// returns the requested BO order, by its ID
    /// </summary>
    /// <returns>Order (BO.Order type)</returns>
    public Order Get(int id);

    /// <summary>
    /// updates the shipping date of the order
    /// </summary>
    /// <param name="id">id of order</param>
    /// <param name="shipDate">the date of the order shipping</param>
    /// <returns>the updated order</returns>
    public Order ShipOrder(int id, DateTime shipDate);

    /// <summary>
    /// updates the delivery date of the order
    /// </summary>
    /// <param name="id">id of order</param>
    /// <param name="shipDate">the date of the order shipping</param>
    /// <returns>the updated order</returns>
    public Order DeliverOrder(int id, DateTime deliveryDate);

    /// <summary>
    /// tracks the order by id,
    /// </summary>
    /// <param name="id">id of order</param>
    /// <returns>TrackOrder object of the asked order (by id)</returns>
    public OrderTracking TrackOrder(int id);

    /// <summary>
    /// this function allows the manager to update an order's details
    /// </summary>
    /// <param name="order">the order to update</param>
    public void UpdateOrder(Order order);
}
