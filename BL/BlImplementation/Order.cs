using BlApi;
using BO;
using Dal;

namespace BlImplementation
{
    internal class Order
    {
       
        public IEnumerable<OrderForList> GetAll()
        {
            return new List<Order>(DataSource.Orders);
        }

        public Order Get(int id)
        {
            if (id > 0)
            {
                try
                {

                }
                catch (Exception)
                {
                    //fix this
                    throw;
                }
            }
        }

        public Order ShipOrder(int id)
        {

        }

        public Order DeliverOrder(int id)
        {

        }

        public OrderTracking TrackOrder(int id)
        {

        }

        public void UpdateOrder(Order order)
        {

        }
    }
}
