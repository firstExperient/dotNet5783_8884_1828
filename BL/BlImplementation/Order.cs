using BlApi;
using Dal;
using DalApi;
using DO;
using System.Collections.Generic;

namespace BlImplementation
{
    internal class Order:IOrder
    {
        private DalApi.IDal Dal = new DalList();

        #region GET

        public IEnumerable<OrderForList> GetAll()
        {
            List<DO.Order> dalOrders = (List<DO.Order>)Dal.Order.GetAll();
            List<BO.OrderForList> blOrders = new List<BO.OrderForList>();
            double totalPrice = 0; // fix this

            foreach (DO.Order item in dalOrders)
            {
                blOrders.Add(new BO.OrderForList()
                {
                    ID = item.ID,
                    CustomerName = item.CustomerName,
                    Status = (BO.OrderStatus),//fix this I don't know how to continue
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

                    order = new BO.Order()
                    {
                        ID = dalOrder.ID,
                        CustomerName = dalOrder.CustomerName,
                        CustomerEmail = dalOrder.CustomerEmail,
                        CustomerAdress = dalOrder.CustomerAdress,
                        OrderDate = dalOrder.OrderDate,
                        Status = (BO.OrderStatus),//fix this I don't know how to continue
                        ShipDate = dalOrder.ShipDate,
                        DeliveryDate = dalOrder.DeliveryDate,
                        // Items = 
                        // TotalPrice =  הם רוצים שנבחר לבד?
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
                    throw new Exception();//fix this
                }

                dalOrder.DeliveryDate = DateTime.Now;

                BO.Order order = new BO.Order()
                {
                    ID = dalOrder.ID,
                    CustomerName = dalOrder.CustomerName,
                    CustomerEmail = dalOrder.CustomerEmail,
                    CustomerAdress = dalOrder.CustomerAdress,
                    OrderDate = dalOrder.OrderDate,
                    Status = (BO.OrderStatus),//fix this I don't know how to continue
                    ShipDate = DateTime.Now,
                    DeliveryDate = dalOrder.DeliveryDate,
                    // Items = 
                    // TotalPrice =  הם רוצים שנבחר לבד?
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
                    throw new Exception();//fix this
                }

                dalOrder.DeliveryDate = DateTime.Now;

                BO.Order order = new BO.Order()
                {
                    ID = dalOrder.ID,
                    CustomerName = dalOrder.CustomerName,
                    CustomerEmail = dalOrder.CustomerEmail,
                    CustomerAdress = dalOrder.CustomerAdress,
                    OrderDate = dalOrder.OrderDate,
                    Status = (BO.OrderStatus),//fix this I don't know how to continue
                    ShipDate = dalOrder.ShipDate,
                    DeliveryDate = DateTime.Now,
                    // Items = 
                    // TotalPrice =  הם רוצים שנבחר לבד?
                };

                return order;
            }
            catch (DO.NotFoundException e)
            {
                throw new BO.NotFoundException("Order not found", e);
            }
        }

        // bonus
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