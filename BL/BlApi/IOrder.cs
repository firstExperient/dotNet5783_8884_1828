using BO;

namespace BlApi;

public interface IOrder
{
    public IEnumerable<OrderForList> GetAll();
    public Order Get(int id);
    public Order ShipOrder(int id);
    public Order DeliverOrder(int id);
    public OrderTracking TrackOrder(int id);
    public void UpdateOrder(Order order);//for bonus - check this
}
