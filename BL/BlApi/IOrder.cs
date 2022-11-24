using BO;

namespace BlApi;

public interface IOrder
{
    public IEnumerable<OrderForList> GetAll();
    public Order Get(int id);
    public Order ShipOrder(int id, DateTime shipDate);
    public Order DeliverOrder(int id, DateTime deliveryDate);
    public OrderTracking TrackOrder(int id);
    public void UpdateOrder(Order order);//for bonus - check this
}
