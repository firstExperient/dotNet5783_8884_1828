using BlApi;
using BO;
using Dal;
using DalApi;
using DO;

namespace BlImplementation
{
    internal class Order:IOrder //fix this
    {
        private DalApi.IDal Dal = new DalList();

        #region GET

        public IEnumerable<BO.OrderForList> GetAll()
        {
            List<DO.Order> dalOrders = (List<DO.Order>)Dal.Order.GetAll();
            List<BO.OrderForList> blOrders = new List<BO.OrderForList>();

            // TODO: for each to calculate the total price
            double totalPrice = 0;


            foreach (DO.Order item in dalOrders)
            {
                blOrders.Add(new BO.OrderForList()
                {
                    ID = item.ID,
                    CustomerName = item.CustomerName,
                    Status = (BO.OrderStatus),//fix this
                    AmountOfItems = dalOrders.Count,
                    TotalPrice = totalPrice
                });
            }
            return blOrders;
        }

        public BO.Order Get(int id)
        {
            if (id > 0) throw new Exception();//fix this
            {
                BO.Order order;

                try
                {
                    DO.Order dalOrder = Dal.Order.Get(id);

                    // TODO: for each to calculate the total price
                    double totalPrice = 0;



                    order = new BO.Order()
                    {
                        ID = dalOrder.ID,
                        CustomerName = dalOrder.CustomerName,
                        CustomerEmail = dalOrder.CustomerEmail,
                        CustomerAdress = dalOrder.CustomerAdress,
                        OrderDate = dalOrder.OrderDate,
                        Status = (BO.OrderStatus), //fix this
                        ShipDate = dalOrder.ShipDate,
                        DeliveryDate = dalOrder.DeliveryDate,
                        Items = , // fix this
                        TotalPrice = dalOrder.Items.count //fix this
                    };
            

                    return order;
                }
                catch (DO.NotFoundException e)
                {
                    throw new BO.NotFoundException("Order not found", e);
                }
            }
            return new BO.Order();
        }
        #endregion

        #region UPDATE

        public BO.Order ShipOrder(int id)
        {
            try
            {
                DO.Order dalOrder = Dal.Order.Get(id);

                if (DateTime.Now > dalOrder.ShipDate)
                {
                    throw new BO.NotYetShippedException("Order hasn't shipped yet");
                }

                // TODO: for each to calculate the total price
                double totalPrice = 0;


                dalOrder.DeliveryDate = DateTime.Now;

                BO.Order order = new BO.Order()
                {
                    ID = dalOrder.ID,
                    CustomerName = dalOrder.CustomerName,
                    CustomerEmail = dalOrder.CustomerEmail,
                    CustomerAdress = dalOrder.CustomerAdress,
                    OrderDate = dalOrder.OrderDate,
                    Status = (BO.OrderStatus),//fix this
                    ShipDate = DateTime.Now,
                    DeliveryDate = dalOrder.DeliveryDate,
                    Items = //fix this
                    TotalPrice //fix this
                };

                return order;
            }
            catch (DO.NotFoundException e)
            {
                throw new BO.NotFoundException("Order not found", e);
            }
        }

        public BO.Order DeliverOrder(int id)
        {
            try
            {
                DO.Order dalOrder = Dal.Order.Get(id);

                if (dalOrder.ShipDate > dalOrder.DeliveryDate)
                {
                    throw new BO.NotYetDeliveredException("Order hasn't delivered yet");
                }

                // TODO: for each to calculate the total price
                double totalPrice = 0;


                dalOrder.DeliveryDate = DateTime.Now;

                BO.Order order = new BO.Order()
                {
                    ID = dalOrder.ID,
                    CustomerName = dalOrder.CustomerName,
                    CustomerEmail = dalOrder.CustomerEmail,
                    CustomerAdress = dalOrder.CustomerAdress,
                    OrderDate = dalOrder.OrderDate,
                    Status = (BO.OrderStatus),//fix this
                    ShipDate = dalOrder.ShipDate,
                    DeliveryDate = DateTime.Now,
                    Items = //fix this
                    TotalPrice //fix this
                };

                return order;
            }
            catch (DO.NotFoundException e)
            {
                throw new BO.NotFoundException("Order not found", e);
            }
        }

        public void UpdateOrder(Order order)
        {

        }

        #endregion

        #region TRACK

        /// <summary>
        /// a function for the maneger to manage orders
        /// </summary>
        /// <param name="id">id of order</param>
        /// <returns>order-tracking of the asked order (by id)</returns>
        public OrderTracking TrackOrder(int id)
        {
            BO.OrderTracking orderTracking;
            try
            {
                DO.Order dalOrder = Dal.Order.Get(id);

                orderTracking = new BO.OrderTracking()
                {
                    ID = dalOrder.ID,
                    Status =  (BO.OrderStatus)//fix this I don't know how to continue,

                };
                return orderTracking;
            }
            catch (DO.NotFoundException e)
            {
                throw new BO.NotFoundException("order not found", e);
            }
        }
        #endregion
    }
}