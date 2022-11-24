using BO;

namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// a function that returns a list of all the orders that are in the BO. 
    /// </summary>
    /// <returns>list of orders</returns>
    public IEnumerable<OrderForList> GetAll();

    /// <summary>
    /// a function that returns the requested BO order, by its ID
    /// </summary>
    /// <returns>list of orders</returns>
    public Order Get(int id);

    /// <summary>
    /// a function that updates the shipping date of the order
    /// </summary>
    /// <param name="id">id of order</param>
    /// <param name="shipDate">the date of the order shipping</param>
    /// <returns>the updated order</returns>
    public Order ShipOrder(int id, DateTime shipDate);

    /// <summary>
    /// a function that updates the delivery date of the order
    /// </summary>
    /// <param name="id">id of order</param>
    /// <param name="shipDate">the date of the order shipping</param>
    /// <returns>the updated order</returns>
    public Order DeliverOrder(int id, DateTime deliveryDate);

    /// <summary>
    /// a function for the maneger to manage orders
    /// </summary>
    /// <param name="id">id of order</param>
    /// <returns>order-tracking of the asked order (by id)</returns>
    public OrderTracking TrackOrder(int id);

    /// <summary>
    /// this function allows the manager to update an order's details
    /// </summary>
    /// <param name="order">the order to update</param>
    public void UpdateOrder(Order order);
}
