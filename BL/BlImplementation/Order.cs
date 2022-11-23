using BlApi;
using BO;
using Dal;
using DO;

namespace BlImplementation
{
    internal class Order
    {
        private DalApi.IDal Dal = new DalList();

        public IEnumerable<OrderForList> GetAll()
        {
            IEnumerable<OrderForList> list = new List<OrderForList>();
            foreach (Order order in Dal.Order)
            {

            }
            return list;
        }

        public BO.Order Get(int id)
        {
            if (id > 0) throw new Exception();//fix this
            {
                BO.Order order;
                try
                {
                    DO.Order dalOrder = Dal.Order.Get(id);

                    order = new BO.Order()
                    {
                        ID = dalOrder.ID,
                        CustomerName = dalOrder.CustomerName,
                        CustomerEmail = dalOrder.CustomerEmail,
                        CustomerAdress = dalOrder.CustomerAdress,
                        OrderDate = dalOrder.OrderDate,
                        // Status = 
                        ShipDate = dalOrder.ShipDate,
                        DeliveryDate = dalOrder.DeliveryDate,
                        // Items = 
                        TotalPrice = 20.0 // זו הכוונה?
                    };

                } catch (Exception)
                {
                    //fix this
                    throw;
                }
                return order;
            }
        }

        public Order ShipOrder(int id)
        {

            // do like this (and change of course)
            bool flag = false;
            for (int i = 0; i < DataSource.Orders.Count; i++)
            {
                if (DataSource.Orders[i].ID == order.ID)
                {
                    DataSource.Orders[i] = order;
                    flag = true;
                    break;
                }
            }
            if (!flag) throw new AlreadyExistsException("Order not found");


            //or like this (and change of course)
            BO.Order order = BO.Order.Find((x) => x.ID == id);
            if (order == null)
                throw new Exception(); //fix this
            try {



            } catch (Exception)
            {
                //fix this
                throw;
            }
            return ;
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
