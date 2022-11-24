using BlApi;
using Dal;

namespace BlImplementation
{
    internal class Order:IOrder
    {
       
        public IEnumerable<BO.OrderForList> GetAll()
        {
            return new List<BO.OrderForList>();
        }

        public BO.Order Get(int id)
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
            return new BO.Order();
        }

        public BO.Order ShipOrder(int id)
        {
            return new BO.Order();
        }

        public BO.Order DeliverOrder(int id)
        {
            return new BO.Order();
        }

        public BO.OrderTracking TrackOrder(int id)
        {
            return new BO.OrderTracking();
        }

        public void UpdateOrder(BO.Order order)
        {

        }
    }
}
